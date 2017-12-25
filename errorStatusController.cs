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

namespace ecsfc
{
    public class errorStatusController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public string Post([FromBody]object value)
        {
            var jd = JObject.Parse(value.ToString());            
            int status = Convert.ToInt32((string)jd["status"]);
            string cprj = (string)jd["cprj"];
            string line = (string)jd["line"];
            string nopr = (string)jd["nopr"];
            string errno = (string)jd["errno"];
            //dt、workstop
            switch (status)
            {
                //不停工異常
                case 4:
                    cc1.connectionofc008_modify(string.Format("insert into ecsfc930_pud(cprj,line,nopr,usdtim,errno,WorkStop) values('{0}','{1}','{2}','{3}','{4}','0')", cprj, line, nopr, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), errno));
                    cc1.connectionofc008_modify(string.Format("update ecsfc929_memb set statuscode='4' where cprj='{0}' and line= '{1}' and nopr='{2}'", cprj, line, nopr));
                    break;
                //停工異常(進行中)
                case 3:
                    cc1.connectionofc008_modify(string.Format("insert into ecsfc930_pud(cprj,line,nopr,usdtim,errno,WorkStop) values('{0}','{1}','{2}','{3}','{4}','1')", cprj, line, nopr, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), errno));
                    string msfid = cc1.connectionofc008_select(string.Format("select top 1 msfid from ecsfc933_mdt where cprj='{0}' and line='{1}' and nopr='{2}' order by msfid desc", cprj, line, nopr), "msfid");
                    this.cc1.connectionofc008_modify(string.Format(@"update ecsfc933_mdt set edate='{0}',edtim='{1}' where msfid='{2}'", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HHmm"), msfid));
                    int countofgroup933 = Convert.ToInt32(cc1.connectionofc008_select(string.Format("select count(*) as cot from ecsfc933_mdt where cprj='{0}' and line='{1}' and nopr='{2}'", cprj, line, nopr), "cot"));
                    int totalm = connectionofc008_selectarray(string.Format("select sdate,stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as sdt,edate,stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':') as edt from ecsfc933_mdt where cprj='{0}' and line='{1}' and nopr='{2}'", cprj, line, nopr), countofgroup933);
                    this.cc1.connectionofc008_modify(string.Format("update ecsfc929_memb set autm='" + totalm + "',statuscode='3' where cprj='{0}' and line='{1}' and nopr='{2}'", cprj, line, nopr));
                    break;
                //停工異常(休停中)
                default:
                    cc1.connectionofc008_modify(string.Format("insert into ecsfc930_pud(cprj,line,nopr,usdtim,errno,WorkStop) values('{0}','{1}','{2}','{3}','{4}','1')", cprj, line, nopr, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), errno));
                    this.cc1.connectionofc008_modify(string.Format("update ecsfc929_memb set statuscode='3' where cprj='{0}' and line='{1}' and nopr='{2}'", cprj, line, nopr));
                    break;
            }

            return "";
        }
        public static int connectionofc008_selectarray(string sql, int countofgroup933)
        {
            string rstr = string.Empty;
            int totalm = 0;
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["c008ConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                int countnum = 0;
                cn.Open();
                SqlDataReader sr = new SqlCommand(sql, cn).ExecuteReader();
                string[] sdatearray = new string[countofgroup933];
                string[] sdtimarray = new string[countofgroup933];
                string[] edatearray = new string[countofgroup933];
                string[] edtimarray = new string[countofgroup933];
                while (sr.Read())
                {
                    sdatearray[countnum] = sr["sdate"].ToString() == "" ? DateTime.Now.ToString("yyyy/MM/dd") : sr["sdate"].ToString();
                    sdtimarray[countnum] = sr["sdt"].ToString() == "" ? DateTime.Now.ToString("HH:mm") : sr["sdt"].ToString();
                    edatearray[countnum] = sr["edate"].ToString() == "" ? (countnum < countofgroup933 - 1 ? sdatearray[countnum] : DateTime.Now.ToString("yyyy/MM/dd")) : sr["edate"].ToString();
                    edtimarray[countnum] = sr["edt"].ToString() == "" ? (countnum < countofgroup933 - 1 ? sdtimarray[countnum] : DateTime.Now.ToString("HH:mm")) : sr["edt"].ToString();
                    countnum++;
                }
                sr.Close();
                TimeSpan ts;
                string dted = string.Empty;
                string dtsd = string.Empty;
                for (int i = 0; i < countofgroup933; i++)
                {
                    if (sdatearray[i] != "")
                    {
                        dted = Convert.ToDateTime(edatearray[i]).ToString("yyyy-MM-dd");
                        dtsd = Convert.ToDateTime(sdatearray[i]).ToString("yyyy-MM-dd");
                        ts = Convert.ToDateTime(dted + " " + edtimarray[i]) - Convert.ToDateTime(dtsd + " " + sdtimarray[i]);
                        int tz = Convert.ToInt32(ts.TotalMinutes);
                        totalm += tz;
                    }
                }
                return totalm;
                //return Tuple.Create(sdatearray, sdtimarray, edatearray, edtimarray);
            }
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