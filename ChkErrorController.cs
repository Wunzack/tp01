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

namespace ecsfc
{
    public class ChkErrorController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        SystemMailMessage smm = new SystemMailMessage();

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
            DateTime cd = Convert.ToDateTime((string)jd["chk_date"]);
            string cdStr = cd.ToString("yyyy-MM-dd");

            string content = string.Format("機號：{0},手順書：{1},點檢點：{2},點檢日：{3},異常訊息：{4}", (string)jd["sno"], (string)jd["hand_order"], (string)jd["coordsID"], cdStr, (string)jd["errDesc"]);
            smm.Mail_Send("vinson@mail.or.com.tw", "m2427@mail.or.com.tw,m3962@mail.or.com.tw,vinson@mail.or.com.tw", (string)jd["sno"] + " 點檢異常", content, true);
            return "";
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