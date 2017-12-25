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
    public class lineIDController : ApiController
    {
        comment_class1 cc1 = new comment_class1();
        SystemMailMessage smm = new SystemMailMessage();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public DataTable Get(string id)
        {
//            if (id != "N112")
//            {
//                return cc1.QueryDataTable(@"select RTRIM(t1.cprj)as cprj,RTRIM(t1.line)as line,
//(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是'),
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='1'))as w1,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='2'))as w2,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='3'))as w3,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='4'))as w4,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='5'))as w5,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='6'))as w6,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='7'))as w7,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='8'))as w8 
// from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) 
// full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr 
// where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and  t1.line ='" + id
//    + "'  group by t1.cprj,t1.line order by max(t2.nopr) desc");
//            }
//            else
//            {
//                return cc1.QueryDataTable(@"select RTRIM(t1.cprj)as cprj,RTRIM(t1.line)as line,
//(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是'),
//(select top 1 case when t004.t_dsca is not null then t004.t_dsca else t915.t_dsca end as t_dsca from rp3440.baan.baan.tecsfc915230 as t915 full join ecsfc004_sinData as t004 on t915.t_cprj=t004.cprj and t915.t_line=t004.line where t_cprj=t1.cprj and t_line=t1.line order by t_mm desc) as t_dsca,(select distinct t929.mkind from ecsfc929_memb as t929 where t929.cprj=t1.cprj and t929.line=t1.line) as mkind,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='1'))as w1,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='2'))as w2,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='3'))as w3,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='4'))as w4,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='5'))as w5,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='6'))as w6,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='7'))as w7,
//RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='8'))as w8 
// from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) 
// full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr 
// where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and  t1.line ='" + id
//    + "'  group by t1.cprj,t1.line order by max(t2.nopr) desc");
//            }
            switch (id)
            {
                case "N112":
                    return cc1.QueryDataTable(string.Format(@"select RTRIM(t1.cprj)as cprj,RTRIM(t1.line)as line,
(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是'),
(select top 1 case when t004.t_dsca is not null then t004.t_dsca else t915.t_dsca end as t_dsca from rp3440.baan.baan.tecsfc915230 as t915 full join ecsfc004_sinData as t004 on t915.t_cprj=t004.cprj and t915.t_line=t004.line where t_cprj=t1.cprj and t_line=t1.line order by t_mm desc) as t_dsca,(select distinct t929.mkind from ecsfc929_memb as t929 where t929.cprj=t1.cprj and t929.line=t1.line) as mkind,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='1'))as w1,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='2'))as w2,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='3'))as w3,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='4'))as w4,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='5'))as w5,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='6'))as w6,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='7'))as w7,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='8'))as w8 
 from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) 
 full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr 
 where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and  t1.line ='{0}'
group by t1.cprj,t1.line order by max(t2.nopr) desc", id));                    
                case "N131":
                    return cc1.QueryDataTable(string.Format(@"select RTRIM(t1.cprj)as cprj,RTRIM(t1.line)as line,
(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是'),
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='1'))as w1,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='2'))as w2,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='3'))as w3,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='4'))as w4,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='5'))as w5,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='6'))as w6,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='7'))as w7,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='8'))as w8 
 from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) 
 full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr 
 where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and  t1.line ='{0}'
group by t1.cprj,t1.line order by max(t2.nopr) desc", id));                    
                default:
                    return cc1.QueryDataTable(string.Format(@"select RTRIM(t1.cprj)as cprj,RTRIM(t1.line)as line,
(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是'),
t914.t_cdat,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='1'))as w1,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='2'))as w2,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='3'))as w3,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='4'))as w4,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='5'))as w5,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='6'))as w6,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='7'))as w7,
RTRIM((select convert(varchar(1),statuscode)+'_'+t_empl+'_'+t_name as stc from ecsfc929_memb as et1 left join ecpsl010 as ep on et1.emno=ep.t_empl where et1.cprj=t1.cprj and et1.line=t1.line and et1.nopr='8'))as w8 
 from ((ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) 
 full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr) left join rp3440.baan.baan.tecsfc914230 as t914 on t1.cprj=t914.t_cprj and t1.line=t914.t_line
 where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and  t1.line ='{0}'
  group by t1.cprj,t1.line,t_no,t_cdat order by t_no asc,t_cdat asc", id));                    
            }
        }

        // POST api/<controller>
        //value from body意思是從request body中取得data資料
        [HttpPost]
        public string Post([FromBody]object value)
        {
            //須注意來源格式的宣告要正確
            //此處因來源為使用json 而此處定義為object(如此才能收到資料)
            int kd = 5682;
            var jd = JObject.Parse(value.ToString());
            string wt = (string)jd["time"]??DateTime.Now.ToString();
            DateTime dt = Convert.ToDateTime(wt);
            //var jData = (JObject)JsonConvert.DeserializeObject(value.ToString());
            int ti = Convert.ToInt32((string)jd["status"]);
            string cprj = (string)jd["cprj"];
            string line = (string)jd["line"];
            string nopr = (string)jd["nopr"];
            string empl = (string)jd["empl"];
            //mode用以判斷是否為協作
            //mode=1為協作專案
            string mode = (string)jd["mode"];
            int autmTotal = 0;
            switch (ti)
            {
                case 1://未開  此處status數字代表意義與前端不同
                       //此處為欲變更為之狀態代號
                    this.cc1.connectionofc008_modify(@"insert into ecsfc933_mdt(cprj,line,nopr,sdate,sdtim,emno) values('" + cprj + "','" + line + "','" + nopr + "','" + dt.ToString("yyyy-MM-dd") + "','" + dt.ToString("HHmm") + "','" + empl + "')");
                    this.cc1.connectionofc008_modify(@"update ecsfc929_memb set sdate='" + dt.ToString("yyyy-MM-dd") + "',sdtim='" + dt.ToString("HHmm") + "',code='*',statuscode='1' where cprj='" + cprj + "'and line='" + line + "' and nopr='" + nopr + "'");
                    try
                    {
                        if (line.Trim() == "P722")
                        {
                            //Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc914230 set " + "t_adat_" + nopr + "='" + dt.ToString("yyyy-MM-dd") + "' where t_cprj='" + cprj + "' and t_line='" + line + "'"));
                            cc1.connectionofc008_modify("update rp3440.baan.baan.tecsfc914230 set " + "t_adat_" + nopr + "='" + dt.ToString("yyyy-MM-dd") + "' where t_cprj='" + cprj + "' and t_line='" + line + "'");
                        }
                        else if (nopr.Trim() == "1")
                        {
                            //Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc915230 set t_apdt='" + dt.ToString("yyyy-MM-dd") + "' where t_cprj='" + cprj + "' and t_line='" + line + "'"));
                            cc1.connectionofc008_modify("update rp3440.baan.baan.tecsfc915230 set t_apdt='" + dt.ToString("yyyy-MM-dd") + "' where t_cprj='" + cprj + "' and t_line='" + line + "'");
                        }                        
                    }
                    catch (Exception ex)
                    {
                        smm.Mail_Send("vinson@mail.or.com.tw", "vinson@mail.or.com.tw", "ecsfc914、915更新失敗", "'" + cprj + "','" + nopr + "'更新失敗'" + ex.ToString() + "'!!!", true);
                    }
                    return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                case 2://轉至休停
                    //autmTotal = autmCal(cprj, line, nopr);
                    DateTime dk = Convert.ToDateTime(getLatestSdate(cprj, line, nopr));
                    if ( dk <= dt)
                    {
                        if (mode == "1")
                        {
                            cc1.connectionofc008_modify(@"update ecsfc933_mdt set edate='" + dt.ToString("yyyy-MM-dd") + "',edtim='" + dt.ToString("HHmm") + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                                + "' and cooperation='1' and emno='" + empl + "' order by msfid desc);"
                                + "insert into ecsfc933_mdt (cprj,line,nopr,emno,cooperation) values('" + cprj + "','" + line + "','" + nopr + "','" + empl + "','1')"
                                );
                        }
                        else
                        {
                            cc1.connectionofc008_modify(@"update ecsfc933_mdt set edate='" + dt.ToString("yyyy-MM-dd") + "',edtim='" + dt.ToString("HHmm") + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                                + "' order by msfid desc);"
                                + "update ecsfc929_memb set statuscode='2',autm='" + autmCal(cprj, line, nopr) + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'"
                                );
                        }
                        return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    }
                    else
                        return "1";
                    
                case 0://轉至開始
                    if (mode == "1")
                    {
                        cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='" + dt.ToString("yyyy-MM-dd") + "',sdtim='" + dt.ToString("HHmm") + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                            + "' and cooperation='1' and emno='" + empl + "' order by msfid desc);"
                            );
                    }
                    else
                    {
                        cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='" + dt.ToString("yyyy-MM-dd") + "',sdtim='" + dt.ToString("HHmm") + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                            + "' order by msfid desc);"
                            + "update ecsfc929_memb set statuscode='1',code='*' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'"
                            );
                    }
                    return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    
                case 3://紅異
                    return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    
                case 4://黃異
                    return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    
                case 5://待復工 尚未完成
                    if (mode == "1")
                    {
                        cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='',sdtim='' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and cooperation='1' and emno='" + empl + "' order by msfid desc)");
                    }
                    else
                    {
                        cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='',sdtim='' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' order by msfid desc)");
                        this.cc1.connectionofc008_modify(@"update ecsfc929_memb set code='*',statuscode='1' where cprj='" + cprj + "'and line='" + line + "' and nopr='" + nopr + "'");

                    }
                    return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    
                case 7://進行中完工 autm計算
                    if (Convert.ToDateTime(getLatestSdate(cprj, line, nopr)) <= dt)
                    {
                        if (mode == "1")
                        {
                            cc1.connectionofc008_modify(@"update ecsfc933_mdt set edate='" + dt.ToString("yyyy-MM-dd") + "',edtim='" + dt.ToString("HHmm") + "' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                                + "' and cooperation='1' and emno='" + empl + "' order by msfid desc);"
                                + "delete from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and (sdate is null and sdtim is null);"
                                );
                        }
                        else
                        {
                            autmTotal = autmCal(cprj, line, nopr);
                            cc1.connectionofc008_modify("update ecsfc929_memb set edate='" + dt.ToString("yyyy-MM-dd") + "',edtim='" + dt.ToString("HHmm") + "',autm='" + autmTotal + "',pfc='是',statuscode='6' where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'");
                        }
                        return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                    }
                    else
                        return "1";
                case 8://休停中完工
                    DateTime glt = Convert.ToDateTime(getLatestSdate(cprj, line, nopr, 0));                    
                        if (Convert.ToDateTime(getLatestSdate(cprj, line, nopr, 0)) <= dt)
                        {
                            
                            if (mode == "1")
                            {
                                cc1.connectionofc008_modify("delete from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and (sdate is null and sdtim is null)");
                            }
                            else
                            {                                
                                    string sdt = glt.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd") ? "750" : glt.ToString("HHmm");                                
                                cc1.connectionofc008_modify(@"
                                    update ecsfc933_mdt set sdate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',sdtim='" + sdt + "',edate='',edtim='' where msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' order by msfid desc)"
                                    +"update ecsfc929_memb set edate='" + dt.ToString("yyyy-MM-dd") + "',edtim='" + dt.ToString("HHmm") + "',pfc='是',statuscode='6',msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and (sdate is not null and edate is not null) order by msfid desc) where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr
                                    + "';"
                                    + "delete from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and (sdate is null and sdtim is null)"
                                    );
                            }
                            if (glt.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                                return "";
                            else 
                                return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;
                        }
                        else
                        return "1";
                default:
                        return "test return: " + kd + ":" + wt + "=>" + ti + ":" + cprj;                    
            }

        }
        public string getLatestSdate(string cprj, string line, string nopr,int type=1)
        {
            if (type == 1)
            {
                string sdate = cc1.connectionofc008_select("select convert(char(10),sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':'),108) as sdt from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and  msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' order by msfid desc)", "sdt");
                return sdate == "" ? DateTime.Now.AddMinutes(-2).ToString() : sdate;
            }
            else
            {
                string edate = cc1.connectionofc008_select("select convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as sdt from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and  msfid=(select top 1 msfid from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and sdate is not null and edate is not null order by msfid desc)", "sdt");
                return edate;
            }
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
        public int autmCal(string cprj, string line, string nopr)
        {
            int countofgroup933 = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as cot from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'", "cot"));
            int totalm = connectionofc008_selectarray("select sdate,stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as sdt,edate,stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':') as edt from ecsfc933_mdt where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'", countofgroup933);
            return totalm;
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