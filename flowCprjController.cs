using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ecsfc
{
    public class flowCprjController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public DataTable Get([FromBody]object value)
        {

            //return cc1.QueryDataTable("select cprj,line,nopr,emno,sdate from ecsfc933_mdt where emno='" + id + "' and cooperation='1'");
            return cc1.QueryDataTable("select cprj,line,nopr,emno,sdate from ecsfc933_mdt where emno='' and cooperation='1'");
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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