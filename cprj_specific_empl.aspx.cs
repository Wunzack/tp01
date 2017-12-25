using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ecsfc
{
    public partial class cprj_specific_empl : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        private void GVBind()
        {
            comment_class1 cc1 = new comment_class1();
            GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,t1.emno,t2.t_name,t1.apdt from (ecsfc929_memb as t1 left join ecpsl010 as t2 on t1.emno=t2.t_empl) left join ecsfc000_emplrole as t3 on t1.emno=t3.empl where  t1.code is" + Session["mod"] + " null and pfc!='是' and t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Global.gline + "%' and t1.nloc like '%" + Global.gnopr + "%' " + Global.d1 + " order by t1.cprj,t1.nopr asc");
            GridView1.DataBind();
        }
        public class Global
        {
            public static string gcprj;
            public static string gline;
            public static string gnopr;
            public static string d1;
            public static string tempvar;
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
            Session["nowtrigger"] = "";
            string serverIP;
            serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Session["mod"] = "";
                Session["tempe"] = Session["emplnet"];
                //------------------抓取emplnet session-----------------sn
                if (serverIP == "http://localhost:61434/pman_hour_collect.aspx")
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
                        //lbempl.Text = Session["emplnet"].ToString();
                    }
                    catch (Exception ex)
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('尚未登入，請由系統首頁登入。'" + ex.ToString() + "'');window.location.href='/default.aspx';", true);
                    }
                    //if (Session["chktimeout"] == null)
                    //{
                    //    Session.Timeout = 15;
                    //    Session["chktimeout"] = "15";
                    //}
                }
                //lbempl.Text = Session["emplnet"].ToString();
                lbname.Text = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"] + "'", "t_name");
                if (cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + Session["emplnet"] + "'", "line") != "")
                    Global.gline = cc1.connectionofc008_select("select line from ecsfc000_emplrole where empl='" + Session["emplnet"] + "'", "line");
                //DBInit();
                GVBind();
            }
            //else
            //{
            //    if (Session["chktimeout"].ToString() == "")
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('閒置時間過長，請重新登入。');", true);
            //    //DBInit();
            //}
        }
        protected string emnostr(string cprj,string line,string nopr)
        {
            string ct=cc1.connectionofc008_select("select emno from ecsfc929_memb where cprj='"+cprj+"' and line='"+line+"' and nopr='"+nopr+"'", "emno");
            Session["temno"] = ct;
            return ct;
        }
        protected string emnoname(string emno)
        {
            return cc1.connectionofc008_select("select t2.t_name from ecsfc000_emplrole as t1 inner join ecpsl010 as t2 on t1.empl=t2.t_empl", "t_name");
        }

        //protected void backtomainpage_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/cprj_specific_empl.aspx?empl=" + Session["emplnet"].ToString())
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
        //    if (serverIP == "http://localhost:61434/cprj_specific_empl.aspx?empl=" + Session["emplnet"].ToString())
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

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string[] date_colarray = Request.Form["emno"].Split(',');
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[rowindex];
            int cprjidx = cc1.getcolumnindexbynameint(row, "cprj");
            int lineidx = cc1.getcolumnindexbynameint(row, "line");
            int cindex = cc1.getcolumnindexbynameint(row, "nopr");
            string pristr1 = cc1.getcolumnindexbyname(row, "cprj");
            string pristr2 = cc1.getcolumnindexbyname(row, "line");
            string pristr3 = cc1.getcolumnindexbyname(row, "nopr");
            string[] emno_colarray = Request.Form["emno"].Split(',');
            string specific_rowemno = emno_colarray[rowindex];
            Button ab = (Button)e.CommandSource;
            Button ab2=new Button();
            GridViewRow myrow = (GridViewRow)ab.NamingContainer;
            if (e.CommandName == "updcomd")
                ab2 = (Button)GridView1.Rows[myrow.DataItemIndex].FindControl("updbtn");
            else if (e.CommandName == "addempl")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "getSpecificEmpl();", true);
            }
            try
            {
            if(ab2.ID=="updbtn")
            {
                this.cc1.connectionofc008_modify("update ecsfc929_memb set emno='"+specific_rowemno+"' where cprj='"+pristr1+"' and line='"+pristr2+"' and nopr='"+pristr3+"'");
            }
            }
            catch(Exception ex)
            {

            }
            GVBind();
        }
        protected void filter_btn_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                Global.tempvar = TextBox3.Text;
                if (Global.tempvar == "")
                    Global.tempvar = DateTime.Now.ToString("yyyy-MM-dd");
                Global.d1 = " and t1.apdt>='" + TextBox2.Text + "' " + "and t1.apdt<='" + Global.tempvar + "'";
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
            GVBind();
        }

        protected void changeMod_Click(object sender, EventArgs e)
        {
            Session["mod"] = " not";
            GVBind();
        }

        protected void changeMod2_Click(object sender, EventArgs e)
        {
            Session["mod"] = "";
            GVBind();
        }
    }
}