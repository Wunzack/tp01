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
    public static class JsonHelper
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                (token.Type == JTokenType.Array && !token.HasValues) ||
                (token.Type == JTokenType.Object && !token.HasValues) ||
                (token.Type == JTokenType.String && (token.ToString() == string.Empty || string.IsNullOrWhiteSpace(token.ToString()))) ||
                (token.Type == JTokenType.Null);
        }

    }

    public class mcsChkListDataController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        public class chkDtModel
        {
            public DataTable dt1 { get; set; }
            public DataTable dt2 { get; set; }
            public DataTable dt3 { get; set; }
        }
        class CustomDataSetConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(DataSet));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataSet x = (DataSet)value;
                JObject jObject = new JObject();
                DataTable a = x.Tables["A"];                
                JArray ajArray = new JArray();
                JObject pjo = new JObject();
                foreach (DataRow row in a.Rows)//result json header內容
                {
                    JObject jo = new JObject();
                    foreach (DataColumn col in a.Columns)
                    {
                        jo.Add(col.Caption.ToLower(), row[col].ToString());
                    }
                    ajArray.Add(jo);                    
                    pjo.Add(jo["coordsid"].ToString(), new JObject());
                    JObject InJo = new JObject();
                    InJo.Add("items", new JObject());
                    InJo.Add("coords", new JObject());
                    string[] coords = jo["coords"].ToString().Split(',');
                    InJo["coords"]["x"] = coords[0];
                    InJo["coords"]["y"] = coords[1];
                    InJo.Add("subImgName", new JObject());
                    InJo["subImgName"]["s1"] = jo["subimg"].ToString();
                    pjo[jo["coordsid"].ToString()] = InJo;
                }
                jObject.Add("header", ajArray);
                //foreach (DataColumn col in a.Columns)
                //{
                //    jObject.Add(col.Caption.ToLower(), a.Rows[0][col].ToString());
                //}
                //JArray jArray = new JArray();
                DataTable b = x.Tables["B"];
                foreach (DataRow row in b.Rows)//result json details內容
                {
                    JObject jo = new JObject();
                    string kd = row["coordsid"].ToString();                    
                    foreach (DataColumn col in b.Columns)
                    {
                        jo.Add(col.Caption.ToLower(), row[col].ToString());
                    }
                    //jArray.Add(jo);
                    //JToken jt=pjo[kd][jo["6s_type"].ToString().Trim()];
                    pjo[kd]["items"][jo["6s_type"].ToString().Trim()] = pjo[kd]["items"][jo["6s_type"].ToString().Trim()].IsNullOrEmpty() == true ? new JObject() : pjo[kd]["items"][jo["6s_type"].ToString().Trim()];
                    pjo[kd]["items"][jo["6s_type"].ToString().Trim()][string.Format("{0}-{1}-{2}", kd, jo["6s_type"].ToString().Trim(), jo["sortno"])] = jo;
                    //pjo[kd] = jArray;
                    //jArray.Clear();
                }
                DataTable c = x.Tables["C"];
                string mi = c.Rows[0]["mach_map"].ToString();
                jObject.Add("mach_map", mi);
                jObject.Add("details", pjo);
                jObject.WriteTo(writer);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string id)
        {
            //chkDtModel cDModel = new chkDtModel();
            //DataTable headDt = new DataTable();
            //cDModel.dt1 = cc1.QueryDataTable(string.Format("select * from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where cl.chk_date=convert([date],GETDATE(),0) and cl.sno='{0}';", id));
            //cDModel.dt1 = headDt;
            //DataTable listDt = new DataTable();
            //cDModel.dt2 = cc1.QueryDataTable(string.Format("select distinct cl.sno,cl.coordsID from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where cl.chk_date=convert([date],GETDATE(),0) and cl.sno='{0}';", id));
            //cDModel.dt2 = listDt;
            List<string> ls = new List<string>();
            //ls.Add(string.Format("select distinct cl.sno,cl.coordsID,SubImg,coords from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where cl.chk_date=convert([date],GETDATE(),0) and cl.sno='{0}';", id));
            //ls.Add(string.Format("select * from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where cl.chk_date=convert([date],GETDATE(),0) and cl.sno='{0}';", id));
            //ls.Add(string.Format("select distinct mach_map from chkList as cl inner join ChkMachInfo as cio on cl.sno=cio.sno where cl.chk_date=convert([date],GETDATE(),0) and cl.sno='{0}'", id));
            ls.Add(string.Format("select distinct cl.sno,cl.coordsID,SubImg,coords from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where  cl.sno='{0}';", id));
            ls.Add(string.Format("select * from (chkList as cl inner join ChkItem as ci on cl.sno=ci.sno and cl.hand_order=ci.hand_order and cl.coordsID=ci.coordsID) inner join ChkPointCoords as cc on cl.sno=cc.sno and cl.coordsID=cc.coordsID where cl.sno='{0}';", id));
            ls.Add(string.Format("select distinct mach_map from chkList as cl inner join ChkMachInfo as cio on cl.sno=cio.sno where  cl.sno='{0}'", id));

            string[] dsName = { "A", "B", "C" };
            DataSet x = cc1.InsertSDA(ls.ToArray(), dsName);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomDataSetConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(x, settings);
            Console.WriteLine(json);

            return json;
        }

        // POST api/<controller>
        public string Post([FromBody]object value)
        {
            var jd = JObject.Parse(value.ToString());
            string result = string.Empty;
            string nt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            DateTime cd = Convert.ToDateTime((string)jd["chk_date"]);
            string cdStr = cd.ToString("yyyy-MM-dd");
            try
            {
                if ((string)jd["updMode"] == "s")//開始
                {
                    //5
                    cc1.connectionofc008_modify(string.Format(@"  update chkList set 
starttime='{0}' where sno='{1}' and hand_order='{2}' and coordsID='{3}' and chk_date='{4}'
", nt, (string)jd["sno"], (string)jd["hand_order"], (string)jd["coordsID"], cdStr));
                }
                else//完成
                {
                    //7
                    cc1.connectionofc008_modify(string.Format(@"  update chkList set
endtime='{0}',totaltime='{1}',comstatus='{2}' 
where sno='{3}' and hand_order='{4}' and coordsID='{5}' and chk_date='{6}'
", nt, (string)jd["totaltime"], "1", (string)jd["sno"], (string)jd["hand_order"], (string)jd["coordsID"], cdStr));
                }

            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}