using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
//*********************************自己加寫（宣告）的NameSpace

namespace ecsfc
{
    public partial class cprj_sechedule_kanban : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        public void GV1Bind()
        {

            int tt = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ct from [ecsfc929_memb] WHERE ([emno] = '" + Session["emplnet"] + "' and [pfc] !='是')", "ct"));

            GridView1.DataSource = cc1.QueryDataTable("select  t1.cprj,t1.line,(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr where  t1.line='" + Session["line"] + "' and (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') group by t1.cprj,t1.line order by max(t2.nopr) desc ");

            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string[] rrrr = Global.holiday;
                Session["line"] = Request.QueryString["line"];
                if (Global.holiday == null)
                    Global.holiday = cc1.GetHoliday(Session["line"].ToString());
                GV1Bind();
            }
        }
        public class Global
        {
            public static int prenloc;
            public static string nloc_str;
            public static string now_nloc;
            public static string pre_nloc;
            public static string next_new_nopr;
            public static string last_cprj_line = "";
            public static int late_days;
            public static string spnloc;
            public static string spnloc2;
            public static DateTime weekend;
            public static List<int> wd_list = new List<int>();
            public static string[] holiday;
        }
        protected string scheduled_project(string cprj, string line, int source_type)
        {
            Global.spnloc = cc1.connectionofc008_select("select top 1 nloc from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)<=getdate() order by apdt desc,apti desc", "nloc");
            string pre_nopr = cc1.connectionofc008_select("select top 1 nopr,nloc,code from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)<=getdate() order by apdt desc,apti desc", "nopr");
            string now_nopr = cc1.connectionofc008_select("select top 1 nopr from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and sdate is not null order by nopr desc", "nopr");
            if (pre_nopr == "")
                pre_nopr = "0";
            if (now_nopr == "")
                now_nopr = "0";
            if (source_type == 1)
            {

                if (Global.spnloc == "" && Convert.ToInt32(now_nopr) > Convert.ToInt32(pre_nopr))
                    return "超前";
                else
                {
                    //預計的進度是否已完工
                    string[] preFinDtCol = { "apdtime", "rutm" };
                    var preFinDtCols = cc1.strarray("select top 1 convert(nvarchar(11),apdt)+' '+stuff(right('0000'+cast(apti as nvarchar),4),3,0,':') as apdtime,rutm from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' order by apdtime desc", 2, preFinDtCol);
                    DateTime apdtime = Convert.ToDateTime(preFinDtCols.Item1[0]).AddHours(Convert.ToInt32(preFinDtCols.Item1[1]));
                    int k = 0;
                    if (apdtime <= DateTime.Now)
                        k = 1;
                    if (k == 1)
                        return "應完工";
                    else
                        return Global.spnloc;
                }
                    
            }
            else
            {
                Global.spnloc2 = cc1.connectionofc008_select("select code from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nloc like'%" + Global.spnloc + "%'", "code");
                //判斷預計目前是否已完工
                string[] preFinDtCol = { "apdtime", "rutm" };
                var preFinDtCols = cc1.strarray("select top 1 convert(nvarchar(11),apdt)+' '+stuff(right('0000'+cast(apti as nvarchar),4),3,0,':') as apdtime,rutm from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' order by apdtime desc", 2, preFinDtCol);
                DateTime apdtime = Convert.ToDateTime(preFinDtCols.Item1[0]).AddHours(Convert.ToInt32(preFinDtCols.Item1[1]));
                int k = 0;
                if (apdtime <= DateTime.Now)
                    k = 1;
                //if (Global.spnloc2 == "*")
                //    return "#5BFF24";//green
                //else
                //    return "#FFBB66";//orange
                if (Convert.ToInt32(now_nopr) > Convert.ToInt32(pre_nopr))
                    return "#00BBFF";//indigo靛青 超前
                else if (Convert.ToInt32(now_nopr) == Convert.ToInt32(pre_nopr) && k == 0)
                    return "#5BFF24";//green 正常
                else
                    return "#FFBB66";//orange 落後
            }

        }
        public string sechedule_nopr(string cprj, string line, int sortkey, string wdate)
        {
            string[] reqarray = { "nloc","tp","pfc","nopr"};
            var v_now_cprj = cc1.strarray("select top 1 *,(select count(*) from ecsfc929_memb as t3 full join ecsfc933_mdt as t4 on t3.cprj=t4.cprj and t3.line=t4.line and t3.nopr=t4.nopr where t3.cprj=t1.cprj and t3.line=t1.line and t3.nopr=t1.nopr and (t4.sdate is not null and t4.edate is null) and pfc!='是')as tp from ecsfc929_memb as t1 full join ecsfc930_pud as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='"+cprj+"' and t1.line='"+line+"' order by concat(t1.sdate,' ',convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108)) desc", 4,reqarray);
            Global.now_nloc = cc1.connectionofc008_select("select count(*) as tp from ecsfc929_memb as t3 full join ecsfc933_mdt as t4 on t3.cprj=t4.cprj and t3.line=t4.line and t3.nopr=t4.nopr where t3.cprj='" + cprj + "' and t3.line='" + line + "'  and (t4.sdate is not null and t4.edate is null) and pfc!='是'", "tp");
            //status key 
            string[] reqvardata = { "sdate", "edate", "s", "pfc", "code", "nopr" };
            var v_now_status = cc1.strarray("select top 1 t2.sdate,t2.edate,WorkStop as s,t1.pfc,t1.code,t1.nopr from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr and t1.msfid=t2.msfid)full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr where t1.cprj='" + cprj + "' and t1.line='" + line + "'order by convert(char(10),t1.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108) desc,convert(char(10),t1.edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t2.edtim as nvarchar),4),3,0,':'),108) desc,t1.nopr asc,WorkStop desc", 6, reqvardata);
            Global.prenloc = 0;
            if (sortkey == 4)
            {
                Global.last_cprj_line = cprj;
                //Global.nloc_str = cc1.connectionofc008_select("select top 1 t1.cprj as tc,t1.nloc as tn,t2.sdate,t2.sdtim,concat(t2.sdate,' ',convert(varchar(5),stuff(right('0000'+cast(t2.sdtim as nvarchar),4),3,0,':'),108)) as cc from ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj + "' t2.sdate is not null order by cc desc", "tn");
                if (v_now_status.Item1[0] == "" && v_now_status.Item1[1] == "" && v_now_status.Item1[3] == "否"&&(v_now_status.Item1[2]=="否"||v_now_status.Item1[2]=="")&&v_now_status.Item1[4]=="*")
                    Session["wc"] = "#FFFF00";//休停
                else if (v_now_status.Item1[2] == "是" && v_now_status.Item1[3] == "否")
                    Session["wc"] = "red";//異常停工
                else if (v_now_status.Item1[0] != "" && v_now_status.Item1[1] == "" && v_now_status.Item1[3] == "否")
                    Session["wc"] = "#5BFF24";//進行中
                else
                {
                    Session["wc"] = "#E38EFF";//預備中purple
                    if (v_now_status.Item1[5] == "1")
                        Global.prenloc = 1;
                    else
                        Global.prenloc = Convert.ToInt32(v_now_status.Item1[5]) + 1;                    
                }                    
                if(Global.prenloc==0)
                    return v_now_cprj.Item1[0];
                else
                {
                    string pnloc = cc1.connectionofc008_select("select distinct top 1 nloc from ecsfc929_memb where nopr='"+Global.prenloc+"' and line='"+Session["line"]+"' order by nloc asc", "nloc");
                    return pnloc;
                }
                
            }
            else if(sortkey<4)
            {
                string fin_nloc = cc1.connectionofc008_select("select top 1 t1.nloc from ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj + "' and t1.line='" + line + "' and (t2.sdate='" + wdate + "' or t2.edate='" + wdate + "') order by (convert(char(10),t2.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t2.sdtim as nvarchar),4),3,0,':'),108)) desc", "nloc");
                if (fin_nloc != "")
                    return fin_nloc;
                else
                    return "";
            }
            else
            {
                string result_nloc = "";
                TimeSpan ts2;
                int tt = Convert.ToInt32(v_now_cprj.Item1[3]) + 1;
                DateTime ed = Convert.ToDateTime("1922-01-01");
                DateTime apdt = Convert.ToDateTime("1922-01-01");
                if (Global.last_cprj_line == cprj && sortkey == 5)
                {
                    if (cc1.connectionofc008_select("select apdt from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nopr='" + v_now_cprj.Item1[3] + "'", "apdt") == cc1.connectionofc008_select("select apdt from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nopr='" + tt.ToString() + "'", "apdt"))
                        tt = Convert.ToInt32(v_now_cprj.Item1[3]) + 2;
                    if (cc1.connectionofc008_select("select apdt from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and nopr='" + tt.ToString() + "'", "apdt")!="")
                        apdt = Convert.ToDateTime(cc1.connectionofc008_select("select apdt from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and nopr='" + tt.ToString() + "'", "apdt"));
                    
                    ts2 = DateTime.Today - apdt;
                    Global.late_days = Convert.ToInt32(ts2.Days) + 1;
                }
                int pre_date=(int)Convert.ToDateTime(wdate).DayOfWeek;
                ed = Convert.ToDateTime(wdate).AddDays(-Global.late_days);
                result_nloc = cc1.connectionofc008_select("select top 1 nloc,(convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)) as ctt from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and apdt='" + ed.ToString("yyyy-MM-dd") + "' and(code!='*' or pfc!='是') order by ctt asc", "nloc");
                //if (pre_date == 6 || pre_date == 0)
                //    Global.late_days += 1;
                Global.last_cprj_line = cprj;
                    if (result_nloc == "")
                        return "";
                    else
                    {
                        if(pre_date == 6 || pre_date == 0)
                        {
                            Global.late_days += 1;
                            ed = Convert.ToDateTime(wdate).AddDays(-Global.late_days);
                            result_nloc = cc1.connectionofc008_select("select top 1 nloc,(convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)) as ctt from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and apdt='" + ed.ToString("yyyy-MM-dd") + "' and(code!='*' or pfc!='是') order by ctt asc", "nloc");
                            //1204暫時寫死只要是六日都不排
                            result_nloc = "";
                        }
                        return result_nloc;
                    }
                        
            }
                
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<int> holiday = (List<int>)Session["wd_list"];
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                foreach (int HolidayList in holiday)
                {
                    e.Row.Cells[HolidayList].BackColor = System.Drawing.Color.FromName("#5ED9C0");
                }
                //e.Row.Cells[Global.cell_int].BackColor = System.Drawing.Color.FromName("#FF9999");
                //e.Row.Cells[Global.cell_int2].BackColor = System.Drawing.Color.FromName("#FF9999");
            }
        }

        protected void back_pre_Click(object sender, EventArgs e)
        {
            if (Request.Url.ToString() == "http://localhost:61434/cprj_sechedule_kanban.aspx?line="+Session["line"])
                Response.Redirect("~/default.aspx");
            else
                Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/default.aspx");            
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            GV1Bind();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int k = -3;
                int test;
                List<int> wd_list = new List<int>();
                for (int i = 2; i < 11; i++)
                {
                    string temp_str = DateTime.Now.AddDays(k).ToString("MM/dd");
                    e.Row.Cells[i].Text = temp_str;
                    test = e.Row.RowIndex;
                    int ifid = ((int)DateTime.Parse(temp_str).DayOfWeek);
                    if (ifid == 6 || ifid == 0)
                    {
                        wd_list.Add(i);
                        Global.weekend = Convert.ToDateTime(temp_str);
                    }
                    int ttttt = (int)DateTime.Parse(temp_str).DayOfWeek;

                    k++;
                }
                Session["wd_list"] = wd_list;
                
                //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell HeaderCell = new TableCell();
                //for (int i = 0; i < 11; i++)
                //{
                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = i == 5 ? "預計進度 實際進度" : "";
                //    HeaderCell.ColumnSpan = 1;
                //    HeaderGridRow.Cells.Add(HeaderCell);
                //}
                //GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);                   
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            for (int i = 0; i < 11; i++)
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = i == 5 ? "預計進度 實際進度" : "";
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);
            }
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
}