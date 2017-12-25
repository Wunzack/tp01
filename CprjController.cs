using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ecsfc
{
    public class CprjController : ApiController
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
            return cc1.QueryDataTable(@"select distinct rtrim(SUBSTRING(nloc,3,1)) as nloc
,SUBSTRING(convert(nvarchar(10),apdt),6,2) as am
,SUBSTRING(convert(nvarchar(10),apdt),9,2) as ad
,case when SUBSTRING(convert(nvarchar(10),t2.sdate),6,2) is null 
then SUBSTRING(convert(nvarchar(10),t1.sdate),6,2) 
else SUBSTRING(convert(nvarchar(10),t2.sdate),6,2) end as sm
,case when SUBSTRING(convert(nvarchar(10),t2.sdate),9,2) is null 
then SUBSTRING(convert(nvarchar(10),t1.sdate),9,2) 
else SUBSTRING(convert(nvarchar(10),t2.sdate),9,2) end as sd 
from ecsfc929_memb as t1 left join ecsfc933_mdt as t2 on t1.msfid=t2.msfid where t1.cprj='" + id + "'");
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