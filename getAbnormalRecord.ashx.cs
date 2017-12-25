using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecsfc
{
    /// <summary>
    /// getAbnormalRecord 的摘要描述
    /// </summary>
    public class getAbnormalRecord : IHttpHandler
    {
        comment_class1 cc1 = new comment_class1();
        public void ProcessRequest(HttpContext context)
        {
            #region 抓取專案異常紀錄
            //須找尋 泛型處理常式 透過參數執行方法的的方法
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            #endregion
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