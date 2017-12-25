using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecsfc
{
    public partial class management_page : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["switch_emno"] = "";
            string serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Session["textbox_text"] = "";
                if (serverIP == "http://localhost:54899/retest.aspx")
                {
                    Session["emplnet"] = "5682";
                    //Session.Timeout = 15;
                    //Session["chktimeout"] = "15";
                }
                //未登入
                if (Session["emplnet"] == null)
                {
                    Session["emplnet"] = "";
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
                else
                {
                    if (Session["userauth"] != null)
                    {
                        if (Session["userauth"].ToString() == "管理者")
                        {
                            Response.Redirect("./default.aspx");
                            //if (Request.Url.ToString() == "http://localhost:61434/management_page.aspx")
                            //    Response.Redirect("~/default.aspx");
                            //else
                            //    Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/default.aspx");
                        }
                        else
                            Response.Redirect("./pman_hour_collect.aspx?empl=" + Session["emplnet"] + "");
                    }
                    //else
                    //{
                    //    Session.RemoveAll();
                    //    Response.Redirect("./management_page.aspx");
                    //    //if (Request.Url.ToString() == "http://localhost:61434/management_page.aspx")
                    //    //    Response.Redirect("~/management_page.aspx");
                    //    //else
                    //    //    Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/management_page.aspx");
                    //}
                }
            }
            TextBox1.Focus();
        }

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    Label2.DataBind();
        //}
        protected string emno_status(string tbt)
        {
            string roleauthority = string.Empty;
            string emno_name = string.Empty;
            if (tbt.Length == 4)
            {
                emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + tbt + "'", "t_name");
                if (emno_name == "")
                    return "查無此工號";
                else
                {
                    roleauthority = cc1.connectionofc008_select("select substring(t2.[role],2,1) as rolea from (ecpsl010 as t1 inner join ecsfc000_emplrole as t2 on t1.t_empl=t2.empl) inner join ecsfc001_role as t3 on t2.role=t3.role where t1.t_empl='" + TextBox1.Text.ToString() + "'", "rolea");
                    if (roleauthority.ToString() == "")
                        return emno_name + "" + "您好!" + "<br/>" + "此工號無權限進行作業!!請聯絡權限管理人";
                    else
                        return emno_name + "" + "您好!";
                }
            }
            else if (tbt.Length == 0)
            {
                if (Session["emplnet"] == null)
                    return "您好 請輸入工號!";
                else
                {
                    if (Request.QueryString["empl"] != null)
                        Session["emplnet"] = Request.QueryString["empl"];
                    if (Session["emplnet"] == null)
                        Session["emplnet"] = "";

                    emno_name = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"].ToString() + "'", "t_name");
                    return emno_name + "";
                }
            }
            else
                return "工號格式錯誤!!";
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Session["emplnet"] = TextBox1.Text;
            string me = cc1.connectionofc008_select(string.Format(@"select top 1 line from (
select distinct top 1 line,'2' as sortID from ecsfc929_memb where emno='{0}'
union
select line,'1' as sortID from ecsfc000_emplrole where empl='{1}') as ut1 where line is not null order by sortID asc", Session["emplnet"], Session["emplnet"]), "line");
            if (me == "")
            {//呼叫前端layer
                //通知此帳號目前並未設定產線
                //請其選取並確認是否更新
                string [] lines = cc1.connectionofc008_selectasarray("select distinct line from ecsfc929_memb", "line");
                string strLines = string.Join(",", lines);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lineComfirmTri", "lineComfirmTri('" + strLines + "');", true);
                
            }
            else
            {
                Session["gline"] = me;
                loginEvent();
            }

        }

        public void loginEvent()
        {
            string roleauthority = string.Empty;
            try
            {
                roleauthority = cc1.connectionofc008_select("select substring(t2.[role],2,1) as rolea from (ecpsl010 as t1 inner join ecsfc000_emplrole as t2 on t1.t_empl=t2.empl) inner join ecsfc001_role as t3 on t2.role=t3.role where t1.t_empl='" + TextBox1.Text.ToString() + "'", "rolea");
            }
            catch (Exception ex)
            {
                Response.Write(@"<hr/> <script language=javascript>alert('尚未登入，請由系統首頁登入。');
                                   window.location.href='/logout.asp'</script>" + ex.ToString());
            }
            if (Convert.ToInt32(roleauthority) >= 4)
            {
                Session["emplnet"] = TextBox1.Text;
                Session["userauth"] = "管理者";
                Response.Redirect("./default.aspx");
            }
            else
            {
                Session["userauth"] = "報工人員";
                Session["emplnet"] = TextBox1.Text;
                //if (Session["gline"].ToString().Trim() == "P722")
                //    Response.Redirect("./pmhc_TempVer.aspx?empl=" + TextBox1.Text + "");
                //else
                //    Response.Redirect("./pman_hour_collect.aspx?empl=" + TextBox1.Text + "");
                string gline = Session["gline"].ToString().Trim();
                switch (gline)
                {
                    case "P722":
                    case "P711":
                    case "P712":
                    case "P723":
                        Response.Redirect("./pmhc_TempVer.aspx?empl=" + TextBox1.Text + "");
                        break;
                    default:
                        Response.Redirect("./pman_hour_collect.aspx?empl=" + TextBox1.Text + "");
                        break;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //確定選取產線並更新ecsfc000_role
            string sl = lineTempStor.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "testReceive", "testReceive('" + sl + "');", true);
            //依選取產線登入
            Session["gline"] = sl;
            loginEvent();
        }
    }
}