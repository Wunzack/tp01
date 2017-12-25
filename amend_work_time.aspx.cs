using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace ecsfc
{
    public partial class amend_work_time : System.Web.UI.Page
    {
        comment_class1 cc1 = new comment_class1();
        public class Global
        {
            public static string dpstr;
            public static string gcprj;
            public static string gline;
            public static string gnopr;
            public static string d1;
            public static string tempvar;
            public static int change_mode;
            public static int tempint1;
            public static string tempvar2;
        }
        private void GVBind()
        {
            switch (Session["gline"].ToString().Trim())
            {
                case "N112":
            if(Convert.ToInt32(Session["change_mode"])==1)//change_mode 補完工
            {
                if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,tec.t_dsca as position,t1.mkind,t1.emno,t2.t_name,convert(char(10),sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as tb2 from ecsfc929_memb as t1 inner join ecpsl010 as t2 on t1.emno=t2.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where t1.code='*' and t1.pfc='否' and t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Session["gline2"] + "%' and t1.nloc like '%" + Global.gnopr + "%' " + Global.d1 + "");
                else
                    GridView1.DataSource = cc1.QueryDataTable("select distinct t1.cprj,t1.line,t1.nopr,t1.nloc,tec.t_dsca as position,t1.mkind,t1.emno,t4.t_name,convert(char(10),t1.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),t1.edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.edtim as nvarchar),4),3,0,':'),108) as tb2 from ((ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 on t1.msfid=t2.msfid) left join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr) inner join ecpsl010 as t4 on t1.emno=t4.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where pfc='否' and code='*' and ((t3.WorkStop ='1' and t3.uclose='是')or(t3.WorkStop ='0')or(t3.WorkStop is null)) and t2.sdate is not null and t1.line='"+Session["gline"]+"'");
            }
            else
            {
                if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,tec.t_dsca as position,t1.mkind,t1.emno,t2.t_name,convert(char(10),sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as tb2 from ecsfc929_memb as t1 inner join ecpsl010 as t2 on t1.emno=t2.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where t1.code is null and t1.pfc='否' and t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Session["gline2"] + "%' and t1.nloc like '%" + Global.gnopr + "%'");
                else
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,tec.t_dsca as position,t1.mkind,t1.emno,t4.t_name,convert(char(10),t1.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),t1.edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.edtim as nvarchar),4),3,0,':'),108) as tb2 from ((ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 on t1.msfid=t2.msfid) left join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr) inner join ecpsl010 as t4 on t1.emno=t4.t_empl inner join rp3440.baan.baan.tecsfc915230 as tec on t1.cprj=tec.t_cprj and t1.line=tec.t_line where pfc='否' and code='*' and ((t3.WorkStop ='1' and t3.uclose='是')or(t3.WorkStop ='0')or(t3.WorkStop is null)) and t2.sdate is null and t1.line='" + Session["gline"] + "'");
            }
                    break;
                case "N131":
                case "P722":                                
            if(Convert.ToInt32(Session["change_mode"])==1)//change_mode 補完工
            {
                if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,t1.position,t1.mkind,t1.emno,t2.t_name,convert(char(10),sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as tb2 from ecsfc929_memb as t1 inner join ecpsl010 as t2 on t1.emno=t2.t_empl where t1.code='*' and t1.pfc='否' and t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Session["gline2"] + "%' and t1.nloc like '%" + Global.gnopr + "%' " + Global.d1 + "");
                else
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,t1.position,t1.mkind,t1.emno,t4.t_name,convert(char(10),t1.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),t1.edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.edtim as nvarchar),4),3,0,':'),108) as tb2 from ((ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 on t1.msfid=t2.msfid) left join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr) inner join ecpsl010 as t4 on t1.emno=t4.t_empl where pfc='否' and code='*' and ((t3.WorkStop ='1' and t3.uclose='是')or(t3.WorkStop ='0')or(t3.WorkStop is null)) and t2.sdate is not null and t1.line='" + Session["gline"] + "'");
            }
            else
            {
                if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,t1.position,t1.mkind,t1.emno,t2.t_name,convert(char(10),sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as tb2 from ecsfc929_memb as t1 inner join ecpsl010 as t2 on t1.emno=t2.t_empl where t1.code is null and t1.pfc='否' and t1.cprj like '%" + Global.gcprj + "%' and t1.line like '%" + Session["gline2"] + "%' and t1.nloc like '%" + Global.gnopr + "%'");
                else
                    GridView1.DataSource = cc1.QueryDataTable("select t1.cprj,t1.line,t1.nopr,t1.nloc,t1.position,t1.mkind,t1.emno,t4.t_name,convert(char(10),t1.sdate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.sdtim as nvarchar),4),3,0,':'),108) as tb,convert(char(10),t1.edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(t1.edtim as nvarchar),4),3,0,':'),108) as tb2 from ((ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 on t1.msfid=t2.msfid) left join ecsfc930_pud as t3 on t1.cprj=t3.cprj and t1.line=t3.line and t1.nopr=t3.nopr) inner join ecpsl010 as t4 on t1.emno=t4.t_empl where pfc='否' and code='*' and ((t3.WorkStop ='1' and t3.uclose='是')or(t3.WorkStop ='0')or(t3.WorkStop is null)) and t2.sdate is null and t1.line='" + Session["gline"] + "'");
            }
            break;
            }

            GridView1.DataBind();
        }
        protected string datetime_value(object cprj, object line, object nopr,object trigger)
        {
            string dvstr = string.Empty;
            DateTime dt;
            dvstr = cc1.connectionofc008_select("select convert(char(10),edate,20)+' '+convert(varchar(5),stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':'),108) as tb from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'", "tb");
                string tristr = cc1.connectionofc008_select("select cprj+line+convert(nvarchar,nopr) as comb from ecsfc929_memb where cprj='"+cprj+"' and line='"+line+"' and nopr='"+nopr+"'", "comb");
                if (trigger.ToString() != "" && trigger.ToString() == tristr)
                {
                    dvstr = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                }
                else if (dvstr != "")
                {
                    dt = Convert.ToDateTime(dvstr);
                    dvstr = dt.ToString("yyyy-MM-ddTHH:mm:ss");
                }
                else
                    dvstr = "";
            return dvstr;
        }
        public string amwork_start_time_gd(string change_mode)
        {
            if (Convert.ToInt32(change_mode) == 1)
                return "補完工時";
            else
                return "補開工時";
        }
        protected string amend_btn_visible(object cprj, object line, object nopr)
        {
            string btn_visible = string.Empty;
            int stp_count = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as stp_count from ecsfc930_pud where cprj='"+cprj+"' and line='"+line+"' and nopr='"+nopr+"' and ([workstop]='1')", "stp_count"));
            if (stp_count > 0)
                btn_visible = "false";
            else
                btn_visible = "true";
            return btn_visible;
        }
        protected string manage_end_time_status(string cprj,string line,string nopr)
        {
            int sint = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as sint from ecsfc930_pud as t1 inner join ecsfc929_memb as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + cprj + "' and t1.line='" + line + "' and t1.nopr='" + nopr + "' and (t1.[workstop]='1')", "sint"));
            if (sint > 0)
                return "異常休停";
            else
                return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["nowtrigger"] = "";
            string serverIP;
            serverIP = Request.Url.ToString();
            if (!Page.IsPostBack)
            {
                Session["SF_SPTrigger"] = "0";
                Session["tempe"] = Session["emplnet"];
                //------------------抓取emplnet session-----------------sn
                if (serverIP == "http://localhost:61434/amend_work_time.aspx")
                {
                    Session["emplnet"] = "5682";
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
                Session["emplnet2"] = Session["emplnet"];
                Session["gline2"] = Session["gline"];
                //lbempl.Text = Session["emplnet"].ToString();
                lbname.Text = cc1.connectionofc008_select("select t_name from ecpsl010 where t_empl='" + Session["emplnet"] + "'", "t_name");
                //DBInit();
                Session["change_mode"] = 1;
                Label4.Text = "補完工";
                GVBind();
            }
            //else
            //{
            //    if (Session["chktimeout"].ToString() == "")
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('閒置時間過長，請重新登入。');", true);
            //    //DBInit();
            //}
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
        //protected void logout_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/amend_work_time.aspx?empl=" + Session["emplnet"].ToString())
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //--------var---------sn
            string control_str = string.Empty;
            Button ab = new Button();
            Button ab2 = new Button();
            Button ab3 = new Button();
            //--------var---------en
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            int cprjidx = cc1.getcolumnindexbynameint(row, "cprj");
            int lineidx = cc1.getcolumnindexbynameint(row, "line");
            int cindex = cc1.getcolumnindexbynameint(row, "nopr");
            int findex = cc1.getcolumnindexbynameint(row, "完工日期");
            string pristr1 = cc1.getcolumnindexbyname(row, "cprj");
            string pristr2 = cc1.getcolumnindexbyname(row, "line");
            string pristr3 = cc1.getcolumnindexbyname(row, "nopr");
            string nloc = cc1.getcolumnindexbyname(row, "nloc");
            string t_name = cc1.getcolumnindexbyname(row, "t_name");
            string mkind = cc1.getcolumnindexbyname(row, "mkind");
            string position = cc1.getcolumnindexbyname(row, "position");
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            string sdtim = DateTime.Now.ToString("HH:mm");
            string sdtim2 = DateTime.Now.ToString("HHmm");

            //0826修改出get templatefield的columindex的方法
            //以往只能用bindfield 的datafield屬性來抓
            //但抓出key後若用其抓取控制項的text值則此控制項必須為asp.net之控制項(否則只會抓出"")
            //(若為html之控制項則加上屬性runat="server"即可使用，
            //但若html控制項被包在(目前已知:ajax)標籤內且此控制項的type若有特定
            //屬性(如此處使用type="datetime-local")則因其不屬updatepanel標籤類別之下
            //會發生錯誤
            //string fintime = cc1.getcolumnindexbyname(row, "完工日期");

            //-----------request.form抓取form中name為datel的欄位全值(預設使用','分隔)--------sn
            string[] date_colarray = Request.Form["datel"].Split(',');
            string specific_rowdate = date_colarray[index];
            //-----------request.form--------------------------------------------------------en
            //
            string[] emno_colarray = Request.Form["emno"].Split(',');
            string specific_rowemno = emno_colarray[index];
            //

            ab = (Button)e.CommandSource;
            GridViewRow myrow = (GridViewRow)ab.NamingContainer;
            if (e.CommandName == "afs")
                ab2 = (Button)GridView1.Rows[myrow.DataItemIndex].FindControl("amend_finish_status");
            else if(e.CommandName=="getnowtime")
                ab3 = (Button)GridView1.Rows[myrow.DataItemIndex].FindControl("getnowtime");
            
            control_str = ab2.Text.ToString();
            if (ab2.ID == "amend_finish_status")
            {
                if (specific_rowdate != "")
                {
                    DateTime convtodt = Convert.ToDateTime(specific_rowdate);
                    string datestr = convtodt.ToString("yyyy-MM-dd");
                    string timestr = convtodt.ToString("HHmm");
                    if (Convert.ToInt32(Session["change_mode"]) == 1 && specific_rowdate != "")
                    {
                        int countofgroup933 = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as cot from ecsfc933_mdt where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'", "cot"));
                        int countisnull = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as nullq from ecsfc929_memb as t1  inner join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t1.cprj='" + pristr1 + "' and t1.line='" + pristr2 + "' and t1.nopr='" + pristr3 + "' and (t2.sdate is NULL and t2.edate is NULL)", "nullq"));
                        string msfid = this.cc1.connectionofc008_select(@"select msfid from ecsfc929_memb where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'", "msfid");
                        if (countisnull > 0)//休停中
                        {
                            if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                            {
                                string seconddata = this.cc1.connectionofc008_select(@"select top 1 t1 from (select top 2 msfid as t1,cprj 
from ecsfc933_mdt where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' order by t1 desc) ecsfc933_mdt order by t1 asc", "t1");
                                this.cc1.connectionofc008_modify(@"update ecsfc929_memb set edate=t2.edate ,edtim=t2.edtim ,msfid=t2.msfid ,pfc='是',statuscode='6' 
                                from ecsfc929_memb as t1 inner join ecsfc933_mdt as t2 
                                on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr where t2.msfid='" + seconddata + "'");
                                this.cc1.connectionofc008_modify(@"delete from ecsfc933_mdt where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' and sdate is NULL and edate is NULL");
                                int totalt = Convert.ToInt32(cc1.connectionofc008_select("select autm from ecsfc929_memb where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'", "autm"));
                                Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc929230 set t_edat='" + sdate + "',t_etim='" + sdtim2 + "',t_autm='" + totalt + "' where t_cprj='" + pristr1 + "' and t_line='" + pristr2 + "' and t_nopr='" + pristr3 + "'"));
                                //休停中完工 
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';mkind='" + mkind + "';$('#FinishBtn').click();", true);
                            }
                            else
                            {
                                this.cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='" + sdate + "',sdtim='" + sdtim2 + "',memno='" + Session["emplnet2"] + "'  where msfid=(select top 1 msfid from ecsfc933_mdt as t2 where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' order by msfid desc)");
                                this.cc1.connectionofc008_modify(@"update ecsfc929_memb set statuscode='1'  where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                                //補開始??
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';$('#StartBtn').click();", true);
                            }                            
                        }
                        else//進行中
                        {
                            if (Session["SF_SPTrigger"].ToString().Trim() == "0")
                            {
                                int totalm = connectionofc008_selectarray("select sdate,stuff(right('0000'+cast(sdtim as nvarchar),4),3,0,':') as sdt,edate,stuff(right('0000'+cast(edtim as nvarchar),4),3,0,':') as edt from ecsfc933_mdt where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'", countofgroup933);
                                this.cc1.connectionofc008_modify(@"update ecsfc929_memb set edate='" + sdate + "',edtim='" + sdtim2 + "',pfc='是',autm='" + totalm + "',statuscode='6'  where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                                this.cc1.connectionofc008_modify("update ecsfc933_mdt set memno='" + Session["emplnet2"] + "' from ecsfc929_memb as t1 left join ecsfc933_mdt as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr  where t1.cprj='" + pristr1 + " ' and t1.line='" + pristr2 + "' and t1.nopr='" + pristr3 + "' and t1.msfid=t2.msfid");
                                Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc929230 set t_edat='" + sdate + "',t_etim='" + sdtim2 + "',t_autm='" + totalm + "' where t_cprj='" + pristr1 + "' and t_line='" + pristr2 + "' and t_nopr='" + pristr3 + "'"));
                                //進行中完工 ok
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';mkind='" + mkind + "';$('#FinishBtn').click();", true);
                            }
                            else
                            {
                                this.cc1.connectionofc008_modify("update ecsfc933_mdt set edate='" + sdate + "',edtim='" + sdtim2 + "',memno='" + Session["emplnet2"] + "' where msfid=(select top 1 msfid from ecsfc933_mdt as t2 where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' order by msfid desc)");
                                this.cc1.connectionofc008_modify(@"update ecsfc929_memb set statuscode='2'  where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                                //補休停 ok
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';mkind='" + mkind + "';$('#PauseBtn').click();", true);
                            }
                        }
                    }
                    else if (Session["SF_SPTrigger"].ToString().Trim() != "1")//開工
                    {
                        if (pristr3 != "1")
                        {
                            Global.tempint1 = Convert.ToInt32(pristr3) - 1;
                            Global.tempvar2 = cc1.connectionofc008_select("select pfc from ecsfc929_memb where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + Global.tempint1 + "'", "pfc");
                        }
                        //if (Global.tempvar2 == "是" || pristr3 == "1")
                        //{
                        this.cc1.connectionofc008_modify(@"insert into ecsfc933_mdt(cprj,line,nopr,sdate,sdtim,emno,memno) values('" + pristr1 + "','" + pristr2 + "','" + pristr3 + "','" + datestr + "','" + timestr + "','" + specific_rowemno + "','" + Session["emplnet"].ToString() + "')");
                        this.cc1.connectionofc008_modify(@"update ecsfc929_memb set sdate='" + datestr + "',sdtim='" + timestr + "',code='*',statuscode='1' where cprj='" + pristr1 + "'and line='" + pristr2 + "' and nopr='" + pristr3 + "'");
                        Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc929230 set t_sdat='" + datestr + "',t_stim='" + timestr + "',t_code='*'  where t_cprj='" + pristr1 + "' and t_line='" + pristr2 + "' and t_nopr='" + pristr3 + "'"));
                        if (pristr2.Trim() == "P722")
                            Task.Run(() => cc1.RunCommandAsynchronously("update rp3440.baan.baan.tecsfc914230 set " + "t_adat_" + pristr3 + "='" + datestr + "' where t_cprj='" + pristr1 + "' and t_line='" + pristr2 + "'"));
                        if (pristr3 == "1" && cc1.connectionofc008_select("select distinct (case when(select t2.sdate from ecsfc929_memb as t2 where t2.cprj=t1.cprj and t2.nopr='1')<=t914.t_pdat_1 then 1 else 0 end) as InTime from ecsfc929_memb as t1 left join rp3440.baan.baan.tecsfc914230 as t914 on t1.cprj=t914.t_cprj and t1.line=t914.t_line where t1.cprj='" + pristr1 + "' and t1.line='" + pristr2 + "'", "InTime") == "0")
                            cc1.ReCalPreDate(pristr1, pristr2);
                        Session["change_mode"] = 1;
                        //補開工
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';mkind='" + mkind + "';$('#StartBtn').click();", true);
                        //}
                        //else
                        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('前一工程未完工不可補開目標工程!!');", true);
                    }
                    else
                    {
                        //補開始
                        this.cc1.connectionofc008_modify("update ecsfc933_mdt set sdate='" + sdate + "',sdtim='" + sdtim2 + "',memno='" + Session["emplnet2"] + "' where msfid=(select top 1 msfid from ecsfc933_mdt as t2 where cprj='" + pristr1 + "' and line='" + pristr2 + "' and nopr='" + pristr3 + "' order by msfid desc)");
                        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('目前為補開工模式!!');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendSignals", "cprj='" + pristr1 + "';line='" + pristr2 + "';nloc='" + nloc + "';posi='" + position + "';t_name='" + t_name + "';mkind='" + mkind + "';$('#SNlocBtn').click();", true);
                    }
                    GVBind();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "nulltip2", "nulltip2();", true);
            }
            else if (ab3.ID == "getnowtime")
            {
                Session["nowtrigger"] = pristr1 + pristr2 + pristr3;
                GVBind();
            }
        }
        protected string InputValue { get; set; }

        protected string emnostr(string cprj, string line, string nopr)
        {
            string ct = cc1.connectionofc008_select("select emno from ecsfc929_memb where cprj='" + cprj + "' and line='" + line + "' and nopr='" + nopr + "'", "emno");
            Session["temno"] = ct;
            return ct;
        }

        protected void filter_btn_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                Global.tempvar = TextBox3.Text;
                if (Global.tempvar == "")
                    Global.tempvar = DateTime.Now.ToString("yyyy-MM-dd");
                Global.d1 = " and sdate>='" + TextBox2.Text + "' " + "and sdate<='" + Global.tempvar + "'";
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

        protected void change_mode_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(Session["change_mode"]) == 1)
            //{
            //    Session["change_mode"] = 0;
            //    this.GridView1.Columns[6].HeaderText = "開工日期";
            //    this.GridView1.Columns[7].HeaderText = "補開工";
            //    change_mode.Text = "轉換補開工模式";
            //    Label4.Text = "補開工";
            //}                
            //else
            //{
            //    Session["change_mode"] = 1;
            //    this.GridView1.Columns[6].HeaderText = "完工日期";
            //    this.GridView1.Columns[7].HeaderText = "補完工";
            //    change_mode.Text = "轉換補完工模式";
            //    Label4.Text = "補完工";
            //}
            switch (Session["change_mode"].ToString().Trim())
            {
                case "0":
                Session["change_mode"] = 1;
                this.GridView1.Columns[6].HeaderText = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "完工日期" : "休停日期";
                this.GridView1.Columns[7].HeaderText = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "補完工" : "補休停";
                change_mode.Text = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "轉換補完工模式" : "轉換補休停模式";
                Label4.Text = this.GridView1.Columns[7].HeaderText;
                    break;
                default:
                Session["change_mode"] = 0;
                this.GridView1.Columns[6].HeaderText = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "開工日期" : "開始日期";
                this.GridView1.Columns[7].HeaderText = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "補開工" : "補開始";
                change_mode.Text = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "轉換補開工模式" : "轉換補開始模式";
                Label4.Text = this.GridView1.Columns[7].HeaderText;
                    break;
            }
            GVBind();
        }

        protected void ChangeToSSmode_Click(object sender, EventArgs e)
        {
            Session["SF_SPTrigger"] = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "1" : "0";
            ChangeToSSmode.Text = Session["SF_SPTrigger"].ToString().Trim() == "0" ? "開啟補開始休停" : "關閉補開始休停";
            GVBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["gline"] = DropDownList1.SelectedItem;
            Session["gline2"] = DropDownList1.SelectedItem;
            GVBind();
        }

        //protected void backtomainpage_Click(object sender, EventArgs e)
        //{
        //    string serverIP = Request.Url.ToString();
        //    if (serverIP == "http://localhost:61434/amend_work_time.aspx?empl=" + Session["emplnet"].ToString())
        //    {
        //        Response.Redirect("~/default.aspx?empl=" + Session["emplnet"].ToString() + "");
        //    }
        //    else
        //    {
        //        Response.Redirect("http://192.168.101.33/WebTest/5682/ecsfc/default.aspx?empl=" + Session["emplnet"].ToString());
        //    }
        //}
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Button getnow_btn = (Button)sender;
        //    GridViewRow myrow = (GridViewRow)getnow_btn.Parent.Parent;
        //    int nowindex = getcolumnindexbyname(myrow, "完工日期");
        //    getnow_btn = (Button)GridView1.Rows[myrow.RowIndex].Cells[nowindex].FindControl("完工日期");
        //    this.InputValue = "2015-08-27T08:30:00";
        //}
    }
}