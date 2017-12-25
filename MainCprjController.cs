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
    public class MainCprjController : ApiController
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
            string line = cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + id + "'", "line");
            if (line.Trim() != "N112")
            {
                return cc1.QueryDataTable(string.Format(@"SELECT distinct 
            [cprj], [line], [nopr],
            [nloc], convert(char(10),[apdt],20) as apdt, 
            [rutm], convert(nvarchar(11),edate)+' '+stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':') as tb2,
            convert(char(10),sdate,20)+' '+stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as tb,
            [autm], [code], [mkind], [msfid], [pfc], t1.position ,statuscode,'' as cooperation
            FROM [ecsfc929_memb] as t1 
            WHERE ([emno] = '{0}' and [pfc] !='是')
union all
select 
            t933.[cprj], t933.[line], t933.[nopr], 
            [nloc], convert(char(10),[apdt],20) as apdt, [rutm], 
            convert(nvarchar(11),t929.edate)+' '+stuff(right('0000'+cast(t929.edtim as nvarchar),4),3,0,':') as tb2,
            convert(char(10),t929.sdate,20)+' '+stuff(right('0000'+cast(t929.sdtim as nvarchar),4),3,0,':') as tb, 
            [autm], [code], [mkind], t933.[msfid], [pfc], 
            t929.position ,statuscode,cooperation
            from ecsfc933_mdt as t933 left join ecsfc929_memb as t929 on t933.cprj=t929.cprj and t933.line=t929.line and t933.nopr=t929.nopr
            where cooperation='1' and t933.emno='{0}' 
            and t933.msfid in(select ftb.mid from(select t1.cprj,t1.line,t1.nopr,(select top 1 t2.msfid from ecsfc933_mdt as t2 where t2.cprj=t1.cprj and t2.line=t1.line and t2.nopr=t1.nopr group by t2.msfid order by t2.msfid desc) as mid from ecsfc933_mdt as t1 where t1.emno='{0}' and cooperation='1' group by t1.cprj,t1.line,t1.nopr )as ftb)", id));
            }
            else
            {
                return cc1.QueryDataTable(string.Format(@"SELECT distinct
            [cprj], [line], [nopr], 
            [nloc], convert(char(10),[apdt],20) as apdt, [rutm], 
            convert(nvarchar(11),edate)+' '+stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':') as tb2,
            convert(char(10),sdate,20)+' '+stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as tb, 
            [autm], [code], [mkind], [msfid], [pfc], 
            RTRIM(t915.t_dsca) as position ,statuscode,''as cooperation
            FROM [ecsfc929_memb] as t1 inner join rp3440.baan.baan.tecsfc915230 as t915 
            on t1.cprj=t915.t_cprj
            WHERE ([emno] = '{0}' and [pfc] !='是') 
union all
select 
            t933.[cprj], t933.[line], t933.[nopr], 
            [nloc], convert(char(10),[apdt],20) as apdt, [rutm], 
            convert(nvarchar(11),t929.edate)+' '+stuff(right('0000'+cast(t929.edtim as nvarchar),4),3,0,':') as tb2,
            convert(char(10),t929.sdate,20)+' '+stuff(right('0000'+cast(t929.sdtim as nvarchar),4),3,0,':') as tb, 
            [autm], [code], [mkind], t933.[msfid], [pfc], 
            RTRIM(t915.t_dsca) as position ,statuscode,cooperation
            from ecsfc933_mdt as t933 left join ecsfc929_memb as t929 on t933.cprj=t929.cprj and t933.line=t929.line and t933.nopr=t929.nopr  inner join rp3440.baan.baan.tecsfc915230 as t915 
            on t933.cprj=t915.t_cprj and t933.line=t915.t_line 
            where cooperation='1' and t933.emno='{0}' 
            and t933.msfid in
            (select ftb.mid from(select t1.cprj,t1.line,t1.nopr,(select top 1 t2.msfid from ecsfc933_mdt as t2 where t2.cprj=t1.cprj and t2.line=t1.line and t2.nopr=t1.nopr group by t2.msfid order by t2.msfid desc) as mid from ecsfc933_mdt as t1 where t1.emno='{0}' and cooperation='1' group by t1.cprj,t1.line,t1.nopr )as ftb)", id));
            }
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