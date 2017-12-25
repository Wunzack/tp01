using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Threading;

namespace ecsfc
{    
    public partial class abnormal_maintain : System.Web.UI.Page
    {
        public class Global
        {
         public static string query_cprj;
         public static string query_line;
         public static string query_nopr;
         public static string gcprj;
         public static string gline;
         public static string gnopr;
         public static string gcstatus;
         public static string wherevar;
         public static string d1;
         public static string tempvar;
        }
     
        comment_class1 cc1 = new comment_class1();
        private void GVBind()
        {
            switch (Session["gline"].ToString().Trim())
            {
                case "N112":            
            if(Global.query_cprj!=null)
            {
                GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,RTRIM(tec.t_dsca) as position,t1.nloc,emno,t2.usdtim,t4.typedesc,t3.errdsca,case convert(nchar(1),t2.[workstop]) when '1' then '是' else '否' end as workstop,t2.uclose,t2.errno,t2.puid,t2.pdsca,t5.t_name from (ecsfc929_memb as t1 inner join (ecsfc930_pud as t2 inner join (ecsfc932_ud as t3 inner join ecsfc931_ud as t4 on t3.[type]=t4.[type]) on t2.errno=t3.errno) on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr)inner join ecpsl010 as t5 on t1.emno=t5.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where t1.cprj='" + Global.query_cprj + "' and t1.line='" + Global.query_line + "' and t1.nopr='" + Global.query_nopr + "'");
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,RTRIM(tec.t_dsca) as position,t1.nloc,emno,t2.usdtim,t4.typedesc,t3.errdsca,case convert(nchar(1),t2.[workstop]) when '1' then '是' else '否' end as workstop,t2.uclose,t2.errno,t2.puid,t2.pdsca,t5.t_name from (ecsfc929_memb as t1 inner join (ecsfc930_pud as t2 inner join (ecsfc932_ud as t3 inner join ecsfc931_ud as t4 on t3.[type]=t4.[type]) on t2.errno=t3.errno) on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr)inner join ecpsl010 as t5 on t1.emno=t5.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Global.gline + "%' and t1.nloc like '%" + Global.gnopr + "%' and uclose like '%" + Global.gcstatus + "%' " + Global.d1 + "");
                GridView1.DataBind();
            }
            break;
                case"N131":
                case"P722":
                case "P711":
                case "P723":
                case "P712":
            if (Global.query_cprj != null)
            {
                GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.position,t1.nloc,emno,t2.usdtim,t4.typedesc,t3.errdsca,case convert(nchar(1),t2.[workstop]) when '1' then '是' else '否' end as workstop,t2.uclose,t2.errno,t2.puid,t2.pdsca,t5.t_name from (ecsfc929_memb as t1 inner join (ecsfc930_pud as t2 inner join (ecsfc932_ud as t3 inner join ecsfc931_ud as t4 on t3.[type]=t4.[type]) on t2.errno=t3.errno) on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr)inner join ecpsl010 as t5 on t1.emno=t5.t_empl where t1.cprj='" + Global.query_cprj + "' and t1.line='" + Global.query_line + "' and t1.nopr='" + Global.query_nopr + "'");
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.position,t1.nloc,emno,t2.usdtim,t4.typedesc,t3.errdsca,case convert(nchar(1),t2.[workstop]) when '1' then '是' else '否' end as workstop,t2.uclose,t2.errno,t2.puid,t2.pdsca,t5.t_name from (ecsfc929_memb as t1 inner join (ecsfc930_pud as t2 inner join (ecsfc932_ud as t3 inner join ecsfc931_ud as t4 on t3.[type]=t4.[type]) on t2.errno=t3.errno) on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr)inner join ecpsl010 as t5 on t1.emno=t5.t_empl where t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Global.gline + "%' and t1.nloc like '%" + Global.gnopr + "%' and uclose like '%" + Global.gcstatus + "%' " + Global.d1 + "");
                GridView1.DataBind();
            }
            break;
            }
        }
        public string tbpdsca_readonly(string uclose)
        {
            if (uclose == "是")
                return "false";
            else
                return "true";
        }
        public string pdsca_label(string pdsca)
        {
            if (pdsca != "")
                return pdsca;
            else
                return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string serverIP;
            serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Global.gcstatus = "否";
                //------------------抓取emplnet session-----------------sn
                if (serverIP == "http://localhost:61434/abnormal_maintain.aspx")
                {
                    //Session["emplnet"] = "5682";
                    //Session.Timeout = 15;
                    //Session["chktimeout"] = "15";
                }
                if (Session["emplnet"] == null)
                {
                    try
                    {
                        Session["emplnet"] = Request.QueryString["empl"];
                    }
                    catch (Exception ex)
                    {
                        Response.Write(@"<hr/> <script language=javascript>alert('尚未登入，請由系統首頁登入。');
                                   window.location.href='/logout.asp'</script>" + ex.ToString());
                    }
                    //if (Session["chktimeout"] == null)
                    //{
                    //    Session.Timeout = 15;
                    //    Session["chktimeout"] = "15";
                    //}
                }
                //DBInit();
                Global.query_cprj = Request.QueryString["cprj"];
                Global.query_line = Request.QueryString["line"];
                Global.query_nopr = Request.QueryString["nopr"];
                //Session["gline"] = Request.QueryString["line"];
                //if (cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + Session["emplnet"] + "'", "line") != "")
                //{
                //    Global.gline = cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + Session["emplnet"] + "'", "line");
                //    Session["gline"] = Global.gline;
                //}
                GVBind();
                //DataBind();
            }
            ////Label4.Text = Request.QueryString["cprj"];
            ////Label5.Text = serverIP;
            //else
            //{
            //    if (Session["chktimeout"].ToString() == "" || Session["chktimeout"] == null)
            //        Response.Write(@"<script language=javascript>alert('閒置時間過長，請重新登入。');");
            //    //DBInit();
            //}
            //try
            //{
            //table9292.SelectCommand = "SELECT [cprj], [line], [nopr], [nloc], [apdt], [rutm], convert(nvarchar(11),edate)+' '+convert(nvarchar(6),edtim) as tb2, convert(nvarchar(11),sdate)+' '+convert(nvarchar(6),sdtim) as tb, [autm], [error], [code], [mkind], [msfid], [pfc] FROM [ecsfc929_memb] WHERE ([emno] = '" + Session["emplnet"] + "' and [pfc] !='是')";
            //}
            //catch (Exception ex) 
            //{
            //    Response.Write("<hr />error"+ex.ToString());
            //}
            lbname.Text = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"] + "'", "t_name");
        }
        protected string visible_status(object uclose,object ky)
        {
            string vsstr = string.Empty;
            if (uclose.ToString() == "否")
            {
                if (Convert.ToInt32(ky) == 1)
                    vsstr = "true";
                else
                    vsstr = "false";
            }
            else
            {
                if (Convert.ToInt32(ky) == 1)
                    vsstr = "false";
                else
                    vsstr = "true";
            }
            return vsstr;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            string ddj = Convert.ToDateTime(cc1.time_now).ToString("yyyy-MM-dd HH:mm");
            string pristr1 = cc1.getcolumnindexbyname(row, "cprj");
            string pristr2 = cc1.getcolumnindexbyname(row, "line");
            string pristr3 = cc1.getcolumnindexbyname(row, "nopr");
            string pristr4 = cc1.getcolumnindexbyname(row, "puid");
            string stop_status = cc1.getcolumnindexbyname(row, "workstop");
            string position = cc1.getcolumnindexbyname(row, "position");
            string usdtimstr = Convert.ToDateTime(cc1.connectionofc008_select("select usdtim from ecsfc930_pud where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' and puid='" + pristr4 + "'", "usdtim")).ToString("yyy-MM-dd HH:mm");
            
            Button ssb1=(Button)e.CommandSource;
            GridViewRow myrow=(GridViewRow)ssb1.NamingContainer;
            TextBox tb = (TextBox)GridView1.Rows[myrow.RowIndex].Cells[9].FindControl("tbpdsca");
            Button upd = (Button)GridView1.Rows[myrow.RowIndex].Cells[10].FindControl("End_exception");
            if (e.CommandName == "End_exception")
            {
                //if (tb.Text != "")
                //{
                    string ssStatus = "1";//1進行中 0休停中
                    if (stop_status == "是")
                    {
                        cc1.connectionofc008_modify("update ecsfc929_memb set code=NULL,statuscode='5' where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                        ssStatus = "0";
                    }
                    else
                    {
                        cc1.connectionofc008_modify("update ecsfc929_memb set statuscode='1' where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                    }
                    cc1.connectionofc008_modify(@"update ecsfc930_pud set [workstop]='0',uclose='是',uedtim='" + ddj + "',eutm='" + cc1.calculate_time(usdtimstr, ddj) + "',pdsca='" + tb.Text + "' where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' and puid='" + pristr4 + "';");
                    string uCt = cc1.connectionofc008_select("  select count(*) as uCt from ecsfc930_pud where line='" + pristr2 + "' and uclose='否'", "uCt");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendMsgtoKanban", "ssStatus = '" + ssStatus + "';position='" + position + "';eClose='" + uCt + "';$('#msgBtn').click();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "layer_close222", "parent.location.reload();", true);                    
                //}
                //else
                //{
                //    //upd.Attributes.Add("onclick", "nulltip2();");
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "nulltip2", "nulltip2();", true);
                //    //System.Threading.Thread.Sleep(5000);
                //    //Response.Redirect(Request.Url.ToString());
                //}
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text.CompareTo("是") == 0)
                    e.Row.BackColor = Color.FromName("orange");
                else if (e.Row.Cells[8].Text.CompareTo("否") == 0)
                {
                    e.Row.BackColor = Color.FromName("#CCFFFF");
                }
                //e.Row.Attributes.Add("mouseover", "this.style.backgroundColor='orange'");
                //e.Row.Attributes["onClick"] = "javascript:c=this.style.backgroundColor;this.style.background='#00BBFF';";
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Button1.Attributes.Add("onclick", "nulltip();");
        //    System.Threading.Thread.Sleep(3000);
        //    Response.Redirect(Request.Url.ToString());
        //}

        protected void filter_btn_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                Global.tempvar = TextBox3.Text;
                if (Global.tempvar == "")
                    Global.tempvar = DateTime.Now.ToString("yyyy-MM-dd");
                Global.d1 = " and convert(char(10),usdtim,120)>='" + TextBox2.Text + "' " + "and convert(char(10),usdtim,120)<='" + Global.tempvar + "'";
            }
            else
                Global.d1 = "";
            if (DropDownList1.SelectedValue == "全產線!!")
                Global.gline = "";
            else
                Global.gline = DropDownList1.SelectedValue; 
            Global.gcprj = Request.Form["cprj_input"];
            if (DropDownList3.SelectedValue == "全工程")
                Global.gnopr = "";
            else
                Global.gnopr = DropDownList3.SelectedValue;
            Global.gcstatus = DropDownList2.SelectedValue;
            GVBind();
        }

        //protected void backtomainpage_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/abnormal_maintain.aspx?empl=" + Session["emplnet"].ToString())
        //    {
        //        Response.Redirect("~/default.aspx?empl=" + Session["emplnet"].ToString() + "");
        //    }
        //    else
        //    {
        //        Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/default.aspx?empl=" + Session["emplnet"].ToString());
        //    }
        //}
        //protected void logout_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/abnormal_maintain.aspx?empl=" + Session["emplnet"].ToString())
        //    {
        //        Session.RemoveAll();
        //        Response.Redirect("~/default.aspx");
        //    }
        //    else
        //    {
        //        Session.RemoveAll();
        //        Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/");
        //    }
        //}

    }
}