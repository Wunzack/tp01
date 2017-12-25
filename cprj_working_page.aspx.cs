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
    public partial class cprj_working_page : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        public class Global
        {
            public static string cprj;
            public static string line;
            public static string nopr;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string serverIP;
            serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Global.cprj = Request.QueryString["cprj"];
                Global.line = Request.QueryString["line"];
                Global.nopr = Request.QueryString["nopr"];
                //------------------抓取emplnet session-----------------sn
                if (serverIP == "http://localhost:61434/cprj_working_page.aspx")
                {
                    //Session["emplnet"] = "5682";
                    //Session.Timeout = 15;
                    //Session["chktimeout"] = "15";
                    Global.cprj = "UC4533";
                    Global.line = "N131";
                    Global.nopr = "3";
                }
                
                //DBInit();
                //DataBind();
            }
        }
    }
}