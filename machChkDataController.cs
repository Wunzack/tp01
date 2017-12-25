using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;
namespace ecsfc
{
    public class machChkDataController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public DataTable Get(string id)
        {
            return cc1.QueryDataTable(string.Format("select distinct sno from ChkItem where nextchk_date='{0}'", id));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public string Put(int id, [FromBody]string value)
        {
            var originalData = JObject.Parse(value.ToString());
            //抓取所有originalData["machs"]下的key name
            var o2 = JObject.Parse(originalData["machs"].ToString());
            IList<string> keys = o2.Properties().Select(p => p.Name).ToList();
            string tstr = "";
            Array d = keys.ToArray();
            try {
            for (int di = 0; di < d.Length; di++)//機台單位
            {
                int totalClean = 0; int totalChk = 0; int totalOil = 0;
                string i = keys[di];
                //insert 機台主資料
                string machID = (string)i;
                string machName = (string)o2[i]["imgName"];
                string insertMain = string.Format("insert into ChkMachInfo(sno,mach_id,mach_name,mach_map)values('{0}','{1}','{2}','{3}');"
                    ,machID
                    ,machID
                    ,machName
                    ,machName);
                string insertCoords = "insert into ChkPointCoords(sno,coordsID,coords,SubImg) values";
                StringBuilder typeCleanInStr = new StringBuilder("insert into ChkItem(sno,[6s_type],coordsID ,hand_order, part, standard ,method ,tool ,time , cycle, nextchk_date, sortNo) values ");
                StringBuilder typeChkInStr = new StringBuilder("insert into ChkItem(sno,[6s_type],coordsID ,hand_order, part, position, standard, method, process, time, cycle, nextchk_date, sortNo) values ");
                StringBuilder typeOilInStr = new StringBuilder("insert into ChkItem(sno,[6s_type],coordsID ,hand_order, position, standard, method, oil, tool, time, cycle, nextchk_date, sortNo) values ");
                StringBuilder insertChkList = new StringBuilder("insert into chkList(sno,hand_order,coordsID,chk_date) values");
                //string insertChkItem = "insert into chkItem(sno,handorder,part,standard,['6s_type'],cycle,nextchk_date) values ";
                
                StringBuilder inCICombine = new StringBuilder();
                int coordsCt=0;
                tstr += o2[i]["imgName"];
                //抓取所有originalData["machs"][mid]下的point key name                
                IList<string> locas = JObject.Parse(o2[i]["chkData"].ToString()).Properties().Select(p => p.Name).ToList();
                int t = 0;
                Array locasAry = locas.ToArray();
                for (int locA = 0; locA < locasAry.Length; locA++)//點檢位置單位
                {
                    string locaObj = locas[locA];
                    string dot = "";
                    //insert coord datas
                    string coordX = o2[i]["chkData"][locaObj]["coords"]["x"].ToString();
                    string coordY = o2[i]["chkData"][locaObj]["coords"]["y"].ToString();
                    dot = coordsCt != 0 ? "," : "";
                    insertCoords += dot + "('" + machID + "','" + locas[locA] + "','" + coordX + "," + coordY + "','" + o2[i]["chkData"][locaObj]["subImgName"]["s1"].ToString() + "')";
                    coordsCt++;
                    //chkitem 各個欄位
                    //get items key name
                    IList<string> items = JObject.Parse(o2[i]["chkData"][locaObj]["items"].ToString()).Properties().Select(p => p.Name).ToList();
                    var dkj = JObject.Parse(o2[i]["chkData"][locaObj]["items"].ToString());
                    string dot2 = "";
                    Array itemsAry = items.ToArray();
                    for (int iat = 0; iat < itemsAry.Length; iat++)//依點檢、給油、清掃資料格式區分
                    {
                        StringBuilder insertChkItem = new StringBuilder();
                        IList<string> typeItems = JObject.Parse(o2[i]["chkData"][locaObj]["items"][items[iat]].ToString()).Properties().Select(p => p.Name).ToList();
                        Array typeItems_ary = typeItems.ToArray();
                        for (int tya = 0; tya < typeItems_ary.Length;tya++ )//跑該分類之所有資料
                        {

                            StringBuilder icol = new StringBuilder();
                            IList<string> ItemsCol = JObject.Parse(o2[i]["chkData"][locaObj]["items"][items[iat]][typeItems[tya]].ToString()).Properties().Select(p => p.Name).ToList();
                            Array icl = ItemsCol.ToArray();
                            icol.Append("'" + machID + "'," + "'" + items[iat] + "','" + locas[locA] + "'");
                            StringBuilder InChkD = new StringBuilder("('" + machID + "','" + o2[i]["chkData"][locaObj]["items"][items[iat]][typeItems[tya]]["hand_order"].ToString() + "','" + locas[locA] + "','" + o2[i]["chkData"][locaObj]["items"][items[iat]][typeItems[tya]]["nextchk_date"].ToString() + "'),");
                            insertChkList.Append(InChkD);
                            for (int ict = 0; ict < icl.Length; ict++)//該資料列下的資料欄位(依分類不同所含欄位也相異)
                            {
                                //string iidf = ItemsCol.ToArray()[ict].ToString();
                                //string dot3 = ict != 0 ? "," : "";
                                string dot3 = ",";
                                icol.Append(dot3 + "'" + o2[i]["chkData"][locaObj]["items"][items[iat]][typeItems[tya]][ItemsCol[ict]].ToString() + "'");                                
                            }
                            string dot4 = tya != 0 ? "," : "";
                            insertChkItem.Append(dot4 + "(" + icol + ")");

                        }
                        t++;                        
                        switch (items[iat])
                        {
                            case "clean":
                                string dot5 = totalClean != 0 ? "," : "";
                                typeCleanInStr.Append(dot5 + insertChkItem);//此處insertChkItem為該點下對應分類所有行資料，此處動作為整併所有點chkPoint中對應分類的資料彙總。e.g. p0_clean+p1_clean...
                                totalClean++;
                                break;
                            case "chk":
                                string dot51 = totalChk != 0 ? "," : "";
                                typeChkInStr.Append(dot51 + insertChkItem);
                                totalChk++;
                                break;
                            default:
                                string dot52 = totalOil != 0 ? "," : "";
                                typeOilInStr.Append(dot52 + insertChkItem);
                                totalOil++;
                                break;
                        }
                        //inCICombine.Append(insertChkItem.Append(";"));
                    }                    
                }
                inCICombine.Append(typeCleanInStr.ToString() + ";" + typeChkInStr.ToString() + ";" + typeOilInStr + ";");
                insertCoords += ";";
                insertChkList = insertChkList.Remove(insertChkList.Length-1,1).Append(";");
                tstr = insertMain + insertCoords + inCICombine + insertChkList;
                //insert chkItems
                cc1.connectionofc008_modify(tstr);
            }
            return tstr;
        }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}