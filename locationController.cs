using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ecsfc
{
    public class locationController : ApiController
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
            return cc1.QueryDataTable(string.Format(@"select distinct t_cprj,line,case when t004.t_dsca is not null then t004.t_dsca else t915.t_dsca end as t_dsca
from rp3440.baan.baan.tecsfc915230 as t915 full join ecsfc004_sinData as t004 on t915.t_cprj=t004.cprj and t915.t_line=t004.line
where (select count(*) from ecsfc929_memb where cprj=t915.t_cprj and pfc='否')!='0'  and t_line='N112' order by t_dsca asc"));
        }

        // POST api/<controller>
        public string Post([FromBody]object value)
        {            
            StringBuilder upStr = new StringBuilder();
            var updatas = JObject.Parse(value.ToString());
            foreach (var n in updatas["upds"])
            {
                //upStr.Append(@"update rp3440.baan.baan.tecsfc915230 set t_dsca='" + (string)n["t_dsca"] + "' where t_cprj='" + (string)n["t_cprj"] + "';");
                upStr.Append(string.Format(@"
IF NOT EXISTS (SELECT * FROM ecsfc004_sinData 
                   WHERE cprj='{0}' and line='{2}')
       INSERT INTO ecsfc004_sinData (cprj,line,t_dsca)
       VALUES ('{0}','{2}', '{1}');
   else
       update ecsfc004_sinData set t_dsca='{1}' where cprj='{0}' and line='{2}';", (string)n["t_cprj"], (string)n["t_dsca"], updatas["line"].ToString()));
            }
            cc1.connectionofc008_modify(upStr.ToString());
            return upStr.ToString();
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