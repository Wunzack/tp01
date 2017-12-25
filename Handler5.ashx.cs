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
    /// Handler2 的摘要描述
    /// </summary>
    public class Handler5 : IHttpHandler
    {
        comment_class1 cc1 = new comment_class1();
        public void ProcessRequest(HttpContext context)
        {
            #region 抓取cprj資料
            DataTable dt = new DataTable();
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cprj";
            dt.Columns.Add(column);
            string[] earray = cc1.connectionofc008_selectasarray("select distinct cprj from ecsfc929_memb where code!='*'", "cprj", Convert.ToInt32(cc1.connectionofc008_select("select count(distinct cprj) as cte from ecsfc929_memb where code!='*'", "cte")));
            for (int i = 0; i < earray.Length;i++ )
            {
                row = dt.NewRow();
                row["cprj"] = earray[i];
                dt.Rows.Add(row);
            }
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