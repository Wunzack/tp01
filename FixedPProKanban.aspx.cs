using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*要引用以下命名空間*/
using System.Data;
using System.Data.SqlClient;
/*Json.NET相關的命名空間*/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ecsfc
{
    public partial class FixedPProKanban : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        public class Global
        {
            public static DateTime t1st;
        }
        public TimeSpan timeTrans(DateTime targTime)
        {
            DateTime d1 = new DateTime(1970, 1, 1);            
            TimeSpan ts = new TimeSpan(targTime.Ticks - d1.Ticks);
            return ts;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //HttpCookie pagePart = new HttpCookie("pagePart");
                string pp = Request.QueryString["pagePart"]??"0";
                Response.Cookies["PagePart"].Value = pp;
                Response.Cookies["PagePart"].Expires = DateTime.Now.AddDays(10);
                //tp：當天TT啟動時間
                string tp = cc1.connectionofc008_select("select top 1 convert(nvarchar(23),sdate,20)+' '+stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as sdt from ecsfc933_mdt where line='N112' and sdate=convert(nvarchar(23),getdate(),23)", "sdt");
                switch (pp)
                {
                    case "0":
                        if (tp == "")
                            break;
                        else
                        {
                            TimeSpan tts = DateTime.Now - Convert.ToDateTime(tp);
                            Session["sTtime"] = tts.TotalSeconds;                            
                        }
                        break;
                    case "1":
                        string fetime = cc1.connectionofc008_select("select top 1 SUBSTRING(convert(nvarchar(23),usdtim,20),1,20)as udt from ecsfc930_pud where line='N112' and WorkStop='1' and uclose='否' order by usdtim asc", "udt");
                        if (fetime != "")
                        {//有停工異常存在
                            //異常發生日期
                            DateTime tfetime = Convert.ToDateTime(fetime);
                            if (tfetime.Date != DateTime.Now.Date)
                            {//判斷最早異常是否非當天發生
                                
                                //累積異常起算時間，使用當天TT開始時間
                                if (tp == "")//開啟看板時待當天作業開始才啟動計算累積時間
                                    break;
                                DateTime tdt = Convert.ToDateTime(tp);
                                //TimeSpan ets = DateTime.Now - tdt;
                                Session["accumulate"] = tdt.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                Session["accumulate"] = tfetime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        else
                            Session["accumulate"] = "";
                        break;
                    case "2":
                        DateTime kd1 = new DateTime(1970, 1, 1);
                        DateTime kd2 = DateTime.Now.ToUniversalTime();
                        TimeSpan kts = new TimeSpan(kd2.Ticks - kd1.Ticks);
                        ViewState["serverTime"] = kts.TotalMilliseconds;
                        break;
                }
                //pagePart.Expires = DateTime.Now.AddMinutes(2);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SetClientID", "DoCookieSetup(\"pagePart\",\"" + pp + "\")", true);
            }
        }
        //把DataTable轉成JSON字串
        //protected void btn_DataTableToJSONstr_Click(object sender, EventArgs e)
        //{
        //    //得到一個DataTable物件
        //    DataTable dt = cc1.QueryDataTable("select * from v_N112WrokStatus");
        //    //將DataTable轉成JSON字串
        //    string str_json = JsonConvert.SerializeObject(dt, Formatting.Indented);

        //    //JSON字串顯示在畫面上
        //    li_showData.Text = str_json;

        //}

        ////把JSON字串轉成DataTable或Newtonsoft.Json.Linq.JArray
        //protected void btn_JSONstrToDataTable_Click(object sender, EventArgs e)
        //{

        //    //Newtonsoft.Json.Linq.JArray jArray = 
        //    //    JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(li_showData.Text.Trim());
        //    //或
        //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(li_showData.Text.Trim());

        //    //GridView1顯示DataTable的資料
        //    //GridView1.DataSource = jArray; GridView1.DataBind();
        //    GridView1.DataSource = dt; GridView1.DataBind();
        //}
    }
}