using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ecsfc
{
    /// <summary>
    /// Handler1 的摘要描述
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        comment_class1 cc1 = new comment_class1();
        public void ProcessRequest(HttpContext context)
        {
            int test;
            #region 抓取目標tb欄位資料
            DataTable dt = new DataTable();
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "empno";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name";
            dt.Columns.Add(column);
            string[] earray = cc1.connectionofc008_selectasarray("select empl from ecsfc000_emplrole", "empl", Convert.ToInt32(cc1.connectionofc008_select("select count(*) as cte from ecsfc000_emplrole", "cte")));
            string[] col_array = { "empl", "t_name" };
            int emplct = Convert.ToInt32(cc1.connectionofc008_select("select count(*) as ct from ecsfc000_emplrole", "ct"));
            var enarray = cc1.mstrarray("select empl,t_name from ecsfc000_emplrole as t1 inner join ecpsl010 as t2 on t1.empl=t2.t_empl", emplct, col_array);
                for (int k = 0; k < emplct;k++ )
                {
                    row = dt.NewRow();
                    row["empno"] = Convert.ToInt32(enarray.Item1[0, k]);
                    row["name"] = enarray.Item1[1, k].ToString();
                    dt.Rows.Add(row);
                }
                //for (int i = 0; i < earray.Length; i++)
                //{
                //    row = dt.NewRow();
                //    row["empno"] = i;
                //    row["name"] = earray[i];
                //    dt.Rows.Add(row);
                //}
            #endregion
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}