using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ecsfc
{
    public partial class error_kind_maintain : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        private void GVBind()
        {
            //GridView2.DataSource = cc1.QueryDataTable("select t1.errno,t1.errdsca,t1.esdate,t2.[type],t2.typedesc from ecsfc932_ud as t1 inner join ecsfc931_ud as t2 on t1.[type]=t2.[type]");
            //GridView2.DataBind();
        }
        public class Global
        {
            public static string unistr;
        }
        protected int getcolumnindexbyname(GridViewRow row, string columnname)
        {
            int columnindex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                //boundfield分辨(按鈕類:選取停工開工開始等.都不是;sqlcommand抓取的欄位就算)
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnname))
                        break;
                }
                else//if (cell.ContainingField is templatefield)
                {
                    if (((TemplateField)cell.ContainingField).HeaderText.Equals(columnname))
                        break;
                } columnindex++;
            }
            return columnindex;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InputValue = "ture";
            string serverIP;
            serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
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
                GVBind();
            }
            //else
            //{
            //    if (Session["chktimeout"].ToString() == "" || Session["chktimeout"] == null)
            //        Response.Write(@"<script language=javascript>alert('閒置時間過長，請重新登入。');");
            //}
            lbname.Text = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"] + "'", "t_name");
        }

        //protected void backtomainpage_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/error_kind_maintain.aspx?empl=" + Session["emplnet"].ToString())
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
        //    if (serverIP == "http://localhost:61434/error_kind_maintain.aspx?empl=" + Session["emplnet"].ToString())
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
        protected string InputValue { get; set; }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            GridView2.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GridView2.DataSourceID = "SqlDataSource1";
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList zupa = (DropDownList)((DropDownList)sender).FindControl("DropDownList2");
            Label llltest = (Label)zupa.FindControl("Label_errno");
            string svdata = zupa.SelectedValue;
            string ctn = cc1.connectionofc008_select("select (count(*)+1)as type_ct from ecsfc932_ud where [type] like'" + svdata + "%'", "type_ct");
            Global.unistr = svdata + ctn;
            llltest.Text = Global.unistr;
        }

        protected void DetailsView1_DataBound(object sender, EventArgs e)
        {
            DetailsView dv = (DetailsView)sender;
        }

        protected void InsertBtn_Click(object sender, EventArgs e)
        {
            Button ibtn = (Button)sender;
            DetailsView d2 = (DetailsView)ibtn.FindControl("DetailsView1");
            Label llll = (Label)d2.FindControl("Label_errno");
            TextBox tbx1 = (TextBox)d2.FindControl("TextBox1");
            DropDownList ddlist2 = (DropDownList)d2.FindControl("DropDownList2");
            //IconNum:6 positive、5 negative 
                try
                {
                    if (llll.Text == "" || tbx1.Text == "")
                        throw new ArgumentNullException();
                    cc1.connectionofc008_modify("insert into ecsfc932_ud(errno,errdsca,type) values('" + llll.Text + "','" + tbx1.Text + "','" + ddlist2.SelectedValue + "')");
                    GridView2.DataSourceID = "SqlDataSource1";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "nulltip2", "nulltip2('新增成功!!',6)", true);
                }
                catch (ArgumentNullException ex )
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "nulltip2", "nulltip2('欄位不可有空!!',5)", true);
                }
                catch(SqlException sqlexcp)
                {
                    //用replace替換掉exception陳述式中出現的換行符號\r\n
                    //以避免javascript因判斷不出字元而導致function出錯
                    string SqlExcpStr = sqlexcp.Message.Replace(Environment.NewLine, "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "nulltip2", "nulltip2('"+SqlExcpStr+"',5)", true);
                }
        }

        protected void InsertBtn_Click1(object sender, EventArgs e)
        {
            GridView2.DataSourceID = null;
        }

    }
}