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
    public class employeeController : ApiController
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
            return cc1.QueryDataTable("select empl,t_name,[role] from ecsfc000_emplrole as t000 inner join ecpsl010 as t010 on t000.empl=t010.t_empl");
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