using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//*********************************自己加寫（宣告）的NameSpace
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace ecsfc
{
    public partial class _default : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        public class Global
        {
            public static string gcprj;
            public static string gline;
            public static string gcstatus;
            public static string wherevar;
            public static string a1;
            public static string a2;
            public static string spnloc;
            public static string spnloc2;
            public static int errorct;
            public static int total_work_time;
            public static int hour_temp;
            public static int min_temp;
            public static string[] nloc;
        }
        protected string nopr_status_show(object cprj,object line,object nopr)
        {
            int cprjint = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ct from ecsfc929_memb where cprj='" + cprj.ToString() + "' and line='" + line.ToString() + "'", "ct"));
            if (Convert.ToInt32(nopr) <= cprjint)
            {
                string nsstr = string.Empty;
                string[] tranarray = { "code", "pfc", "sti", "emno" };
                int recovery_ct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as rect from ecsfc929_memb where cprj='"+cprj.ToString()+"' and line='"+line.ToString()+"' and nopr='"+nopr+"' and(code is null and sdate is not null)", "rect"));
                string pausect = cc1.connectionofc008_select("select distinct t1.cprj,t1.line,t1.nopr,(select count(*) from ecsfc929_memb as t3 inner join ecsfc933_mdt as t4 on t3.cprj=t4.cprj and t3.line=t4.line and t3.nopr=t4.nopr where t3.cprj=t1.cprj and t3.line=t1.line and t3.nopr=t1.nopr and(t4.sdate is not null and t4.edate is null) and pfc!='是')as pausect from ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj.ToString() + "' and t1.line='" + line.ToString() + "' and t1.nopr='" + nopr.ToString() + "'", "pausect");
                var resulta = cc1.strarray("select distinct t1.code,t1.pfc,(select count(*) from ecsfc930_pud where cprj='" + cprj.ToString() + "' and line='" + line.ToString() + "' and nopr='" + nopr.ToString() + "' and uclose='否') as sti, t1.emno from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) full join ecsfc930_pud as t3 on t1.cprj=t2.cprj and t1.line=t3.line and t1.nopr=t3.nopr where t1.cprj='" + cprj.ToString() + "' and t1.line='" + line.ToString() + "' and t1.nopr='" + nopr.ToString() + "';", 4, tranarray);
                nsstr = resulta.Item1[0];
                string ename = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + resulta.Item1[3] + "'", "t_name");
                if (resulta.Item1[0] == "*" && Convert.ToInt32(resulta.Item1[2]) == 0 && resulta.Item1[1] == "否")
                {
                    //if (pausect == "0")
                    //    return "休停";
                    //else
                        return ename;
                }
                else if (resulta.Item1[1] == "是")
                {
                    Global.errorct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ect from ecsfc930_pud where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and uclose='否'", "ect"));
                    if (Global.errorct > 0)
                        return "";
                    else
                        return "已完工";                   
                }
                else if (Convert.ToInt32(resulta.Item1[2]) > 0)
                    return "";
                else if (recovery_ct > 0)
                    return "已復工";
                else
                    return "未開工";
            }
            else
                return "------";
        }
        public void GV1Bind()
        {
            
            int tt = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ct from [ecsfc929_memb] WHERE ([emno] = '" + Session["emplnet"] + "' and [pfc] !='是')", "ct"));

            //GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') from (ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr) full join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr where  (select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(cprj) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and pfc='是') and t1.cprj like '%" + Session["gcprj"] + "%' and t1.line like '%" + Session["gline"] + "%' " + Session["gcstatus"] + " group by t1.cprj,t1.line order by max(t2.nopr) desc");

            //GridView1.DataBind();
        }
        protected string error_btn_visible(string cprj, string line, int nopr)
        {
            int errorct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ect from ecsfc930_pud where cprj='"+cprj+"' and line='"+line+"' and nopr='"+nopr+"' and uclose='否'", "ect"));
            if (errorct > 0)
                return "true";
            else
                return "false";
        }
        //預計進度對比欄
        protected string scheduled_project(string cprj,string line,int source_type)
        {
            //超前
            string pre_nopr = cc1.connectionofc008_select("select top 1 nopr,nloc,code from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)<=getdate() order by apdt desc,apti desc", "nopr");
            string now_nopr = cc1.connectionofc008_select("select top 1 nopr from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and sdate is not null order by nopr desc", "nopr");
            if (now_nopr == "")
                now_nopr = "0";
            if (pre_nopr == "")
                pre_nopr = "0";
            //預計的進度是否已完工
            string[] preFinDtCol = { "apdtime", "rutm" };
            Session["spnloc"] = cc1.connectionofc008_select("select top 1 nloc from ecsfc929_memb as t1 where cprj='" + cprj + "' and line='" + line + "' and convert(char(10),apdt,20)+' '+convert(varchar(5),stuff(right('0000'+cast(apti as nvarchar),4),3,0,':'),108)<=getdate() order by apdt desc,apti desc", "nloc");
            var preFinDtCols = cc1.strarray("select top 1 convert(nvarchar(11),apdt)+' '+stuff(right('0000'+cast(apti as nvarchar),4),3,0,':') as apdtime,rutm from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' order by apdtime desc", 2, preFinDtCol);
            DateTime apdtime = Convert.ToDateTime(preFinDtCols.Item1[0]).AddHours(Convert.ToInt32(preFinDtCols.Item1[1]));
            int k = 0;
            if (apdtime <= DateTime.Now)            
                k = 1;            
            //
            if (source_type == 1)
            {
                if (k == 1)
                    return "應完工";
                else
                    return Session["spnloc"].ToString();                
            }
            else
            {
                Session["spnloc2"] = cc1.connectionofc008_select("select code from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nloc like'%" + Session["spnloc"] + "%'", "code");
                if (Convert.ToInt32(now_nopr) > Convert.ToInt32(pre_nopr))
                    return "#00BBFF";//indigo靛青 超前
                else if (Session["spnloc2"].ToString() == "*" && k == 0)
                    return "#5BFF24";//green正常
                else if (pre_nopr == "0")
                    return "#FFFFFF";
                else
                    return "#FFBB66";//orange落後
            }

        }
        protected string summary_total_worktime(string cprj,string line)
        {
            int temp_totalwtime;
            int cprjstartct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as csct from ecsfc929_memb where cprj='"+cprj+"' and line='"+line+"' and code='*'", "csct"));
            string[] sarray = cc1.connectionofc008_selectasarray("select autm from ecsfc929_memb where cprj='"+cprj+"' and line='"+line+"' and code='*'", "autm",cprjstartct);
            for(int i=0;i<cprjstartct;i++)
            {
                Global.total_work_time = Global.total_work_time + Convert.ToInt32(sarray[i]);
            }
            temp_totalwtime = Global.total_work_time;
            Global.total_work_time = 0;
            Global.hour_temp=Convert.ToInt32(temp_totalwtime)/60;
            return Global.hour_temp.ToString("00") + ":" + (Convert.ToInt32(temp_totalwtime) - (Global.hour_temp * 60)).ToString("00");
        }
        protected string emno_status()
        {
            string roleauthority=string.Empty;
            string emno_name = string.Empty;
            emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"].ToString() + "'", "t_name");
            return emno_name + "";
            //if(tbt.Length==4)
            //{
            //    emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='"+tbt+"'", "t_name");
            //    if (emno_name == "")
            //        return "查無此工號";
            //    else
            //    {
            //        roleauthority = cc1.connectionofc008_select("select substring(t2.[role],2,1) as rolea from (ecpsl010 as t1 inner join ecsfc000_emplrole as t2 on t1.t_empl=t2.empl) inner join ecsfc001_role as t3 on t2.role=t3.role where t1.t_empl='" + TextBox1.Text.ToString() + "'", "rolea");
            //        if (roleauthority.ToString() == "")
            //            return emno_name + "" + "您好!" + "<br/>" + "此工號無權限進行作業!!請聯絡權限管理人";
            //        else
            //            return emno_name + "" + "您好!";
            //    }
            //}
            //else if(tbt.Length==0)
            //{
            //    if(Session["emplnet"]==null)
            //        return "您好 請輸入工號!";
            //    else
            //    {
            //        if (Request.QueryString["empl"]!=null)
            //            Session["emplnet"] = Request.QueryString["empl"];
            //        if(Session["emplnet"]==null)
            //            Session["emplnet"] = "";
                    
            //        emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"].ToString() + "'", "t_name");
            //        return emno_name + "";
            //    }
            //}
            //else
            //    return "工號格式錯誤!!";
        }
        protected string cprj_status_color(string cprj,string line,int nopr)
        {
            string[] col = { "sdate", "code", "pfc", "sct", "ect" };
            var result = cc1.strarray("select distinct t1.sdate,t1.code,t1.pfc,(select count(*) from ecsfc930_pud where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and ([workstop]='1'))as sct,(select count(*) from ecsfc930_pud where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and ([workstop]!='1') and uclose='否')as ect from ecsfc929_memb as t1 full join ecsfc930_pud as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj + "' and t1.line='" + line + "' and t1.nopr='" + nopr + "'", 5, col);
            string pausect = cc1.connectionofc008_select("select distinct t1.cprj,t1.line,t1.nopr,(select count(*) from ecsfc929_memb as t3 inner join ecsfc933_mdt as t4 on t3.cprj=t4.cprj and t3.line=t4.line and t3.nopr=t4.nopr where t3.cprj=t1.cprj and t3.line=t1.line and t3.nopr=t1.nopr and(t4.sdate is not null and t4.edate is null) and pfc!='是')as pausect from ecsfc929_memb as t1 full join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj.ToString() + "' and t1.line='" + line.ToString() + "' and t1.nopr='" + nopr.ToString() + "'", "pausect");
            if (result.Item1[0] != "" && result.Item1[1] == "*" && result.Item1[2] == "否" && (result.Item1[3] == "0" && result.Item1[4] == "0"))
            {
                if (pausect == "0")
                    return "#FFFF00";//yellow 暫停中
                else
                    return "#5BFF24";//green 進行中
            }
            else if (result.Item1[0] != "" && result.Item1[1] == "*" && result.Item1[2] == "否" && (result.Item1[3] == "0" && result.Item1[4] != "0"))
                return "#FFFF00";//yellow 普通異常
            else if (result.Item1[0] != "" && result.Item1[1] == "*" && result.Item1[2] == "否" && result.Item1[3] != "0")
                return "#FF0000";//red 停工異常
            else if (result.Item1[0] != "" && result.Item1[1] != "*" && (result.Item1[0] != null && result.Item1[3] != null))
                return "#FF8800";//orange 已復工
            else if (result.Item1[2] == "是")
            {
                Global.errorct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ect from ecsfc930_pud where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "' and uclose='否'", "ect"));
                if (Global.errorct > 0)
                    return "#00BBFF";
                else
                    return "#00FFFF";//sky blue 已完工                
            }                
            else
                return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["switch_emno"] = "";            
            if (Session["emplnet"] == null)
            {
                //Session["emplnet"] = "";
                Response.Redirect("./management_page.aspx");
            }
            else if (Session["userauth"].ToString() == "報工人員")
            {
                string gline = Session["gline"].ToString().Trim();
                switch (gline)
                {
                    case "P722":
                    case "P711":
                    case "P712":
                    case "P723":
                        Response.Redirect("./pmhc_TempVer.aspx?empl=" + Session["emplnet"].ToString() + "");
                        break;
                    default:
                        Response.Redirect("./pman_hour_collect.aspx?empl=" + Session["emplnet"].ToString() + "");
                        break;
                }

            }                        
            string serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Session["spnloc"] = "";
                Session["spnloc2"] = "";
                Session["serverIP"] = serverIP;
                //Timer2.Enabled = true;
                //Timer1.Enabled = true;
                Session["gcprj"] = "";
                Session["textbox_text"] = "";

                string emno_name = string.Empty;
                emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"].ToString() + "'", "t_name");
                Label18.Text = emno_name;

                //------------------抓取emplnet session-----------------sn
                //Session["gline"] = cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + Session["emplnet"] + "'", "line");
                //DBInit();
                //DropDownList1.SelectedValue = Session["gline"].ToString();
                //string dfksfj = Session["gline"].ToString();
                //string P722 = "";
                //if (Session["gline"].ToString().Trim() == "N131")                
                //    P722 = " and RTRIM(nloc) like '___' ";
                //Global.nloc = cc1.connectionofc008_selectasarray("select distinct nloc,nopr from ecsfc929_memb where line='" + Session["gline"] + "' " + P722 + " order by nopr asc", "nloc", 0);
                //GV1Bind();
                //DataBind();
            }
            //else
            //{
            //    if (Session["chktimeout"].ToString() == "" || Session["chktimeout"] == null)
            //        Response.Write(@"<script language=javascript>alert('閒置時間過長，請重新登入。');");
            //    //DBInit();
            //}            
        }
        protected string nopr_status(object cprj,object line,object nopr)
        {
            string nos = string.Empty;
            //cc1.connectionofc008_select("", "");
            return nos;
        }

        protected void timer_tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(500);
            //Label2.Text = "panel refresh at :　" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //GV1Bind();
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            string serverIP = Request.Url.ToString();
            Session.RemoveAll();
            Response.Redirect("./management_page.aspx");
            //if (serverIP == "http://localhost:61434/default.aspx")
            //{
            //    Session.RemoveAll();
            //    Response.Redirect("~/management_page.aspx");
            //}
            //else
            //{
            //    Session.RemoveAll();
            //    Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/management_page.aspx");
            //}
        }
        protected void Timer2_Tick(object sender, EventArgs e)
        {
            Label18.DataBind();
        }

        protected void filter_btn_Click(object sender, EventArgs e)
        {
            //if (DropDownList1.SelectedValue == "全產線!!")
            //    Session["gline"] = "";
            //else
            //    Session["gline"] = DropDownList1.SelectedValue;            
            //Session["gcprj"] = Request.Form["cprj_input"];
            //Session["gcstatus"] = DropDownList2.SelectedValue;
            //if (Session["gcstatus"].ToString() == "ing")
            //    Session["gcstatus"] = " and (select count(*) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)!=(select count(*) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and code is null)";
            //else if (Session["gcstatus"].ToString() == "unstart")
            //    Session["gcstatus"] = " and (select count(*) from ecsfc929_memb where cprj=t1.cprj and line=t1.line)=(select count(*) from ecsfc929_memb where cprj=t1.cprj and line=t1.line and code is null)";
            //else if (Session["gcstatus"].ToString() == "esta")
            //    Session["gcstatus"] = " and (select count(*) from ecsfc930_pud where cprj=t1.cprj and line=t1.line and uclose!='是')>0";
            //else
            //    Session["gcstatus"] = "";
            //GV1Bind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/cprj_sechedule_kanban.aspx?line=" + Session["gline"] + "");
        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {               
        //        for (int i = 0; i < Global.nloc.Length; i++)
        //        {
        //            e.Row.Cells[i + 3].Text = Global.nloc[i];
        //        }
        //    }
        //}

        protected void Button2_Click(object sender, EventArgs e)
        {
            string test = Session["serverIP"].ToString();
            string layerUrl = Session["serverIP"].ToString() == "http://localhost:61434/default.aspx" ? "http://localhost:61434/amend_work_time.aspx" : "http://192.168.101.33/WebTest/5682/ecsfc/amend_work_time.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "LayerOpen", "LayerOpen('" + layerUrl + "');", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string layerUrl = Session["serverIP"].ToString() == "http://localhost:61434/default.aspx" ? "http://localhost:61434/abnormal_maintain.aspx" : "http://192.168.101.33/WebTest/5682/ecsfc/abnormal_maintain.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "LayerOpen", "LayerOpen('" + layerUrl + "');", true);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string layerUrl = "./error_maintain.aspx?line=" + Session["gline"].ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "LayerOpen", "LayerOpen('" + layerUrl + "');", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string layerUrl = Session["serverIP"].ToString() == "http://localhost:61434/default.aspx" ? "http://localhost:61434/cprj_specific_empl.aspx" : "http://192.168.101.33/WebTest/5682/ecsfc/cprj_specific_empl.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "LayerOpen", "LayerOpen('" + layerUrl + "');", true);
        }

        //protected void gvbindBtn_Click(object sender, EventArgs e)
        //{
        //    //GV1Bind();
        //}

        //protected void Timer2_Tick(object sender, EventArgs e)
        //{
        //    System.Threading.Thread.Sleep(4000);
        //    Label8.Text = "panel refresh at :　" + DateTime.Now.Ticks.ToString();
        //}
    }
}