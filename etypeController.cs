using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ecsfc
{
    public class etypeController : ApiController
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
            string[] key = id.Split(',');
            if (key[1] == "1")
            {
                return cc1.QueryDataTable(@"select t931.typedesc,t931.[type],t931.groupID "
                    + "from ecsfc931_ud as t931 left join ecsfc003_errorGrp as t003 on t931.groupID=t003.groupID" +
                    " where t003.line='" + key[0] + "'");
            }
            else
            {
                return cc1.QueryDataTable(@"select t932.errno,t932.errdsca,t932.[type] 
                from (ecsfc932_ud as t932 inner join ecsfc931_ud as t931 on t932.[type]=t931.[type] )inner join ecsfc003_errorGrp as t003 on t931.groupID=t003.groupID 
                where t003.line='" + key[0] + "'");
            }
        }

        // POST api/<controller>
        public string Post([FromBody]object value)
        {
            string upStr = "";
            var updatas = JObject.Parse(value.ToString());
            foreach (var n in updatas["upds"])
            {
                upStr += "update ecsfc932_ud set errdsca='" + (string)n["errdsca"] + "',type='" + (string)n["kindval"] + "',lostno='" + (string)n["lostno"] + "' where errno='" + (string)n["errno"] + "';";
            }
            cc1.connectionofc008_modify(upStr);
            return upStr;
        }

        //public class Item
        //{
        //    public string a = string.Empty;
        //    public string b = string.Empty;
        //    public string c = string.Empty;
        //}

        // PUT api/<controller>/5
        public string Put([FromBody]object value)
        {
            string d1 = string.Empty;
            string d2 = string.Empty;
            int t = 0;
            string dot = "";
            string uptStr = "insert into ecsfc932_ud(errdsca,[type],lostno) values ";
            var originalData = JObject.Parse(value.ToString());
            //List<Item> items = new List<Item>();
            foreach (var a in originalData["etypes"])
            {
                dot = t != 0 ? "," : "";
                uptStr += dot + "('" + (string)a["etypeName"] + "','" + (string)a["kindval"] + "','" + (string)a["lostno"] + "')";
                t++;
            }
            cc1.connectionofc008_modify(uptStr);
            return uptStr;
        }

        // DELETE api/<controller>/5
        public string Delete([FromBody]object value)
        {
            int t = 0;
            string dot = "";
            string jsonA = string.Empty;            
            var originalData = JObject.Parse(value.ToString());
            string dta = originalData["dt"].ToString();            
            foreach (var a in originalData["dt"])
            {
                dot = t != 0 ? "," : "";
                jsonA += dot + (string)a["d1"];
                t++;
            }
            //try
            //{
                string delStr = "delete from ecsfc932_ud where errno in(" + jsonA + ")";
                cc1.connectionofc008_modify(delStr);
                return "success" + "  " + delStr;
            //}
            //catch (SqlException ex)
            //{
            //    string errorResult = DisplaySqlErrors(ex);
            //    return "1";
            //}            
        }

        private static string DisplaySqlErrors(SqlException exception)
        {
            string exStr = string.Empty;
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                exStr += "Index #" + i + "\n" + "Error: " + exception.Errors[i].ToString() + "\n";
            }
            return exStr;
        }
    }
}