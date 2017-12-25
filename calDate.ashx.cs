using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ecsfc
{
    /// <summary>
    /// calDate 的摘要描述
    /// </summary>
    public class calDate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int col = 0;
            
            string[] DtArray = new string[12];
            List<string> DateList = new List<string>();
            #region 產生日期序列
            for (var i = 6; i > -6; i--) {
                DateTime dt = DateTime.Now;
                dt = dt.AddDays(-(i));
                string cdt = dt.Month.ToString() + "/" + dt.Date.ToString("dd");
                DateList.Add(cdt);
                //DtArray[col] = dt.Month.ToString() + "/" + dt.Date.ToString();
                //$('#B1dTable tr:eq(0) td:eq(' + col + ')').html((dt.getMonth() + 1).toString().padLeft(2) + '/' + dt.getDate().toString().padLeft(2));
                //$('#B1dTable tr:eq(1) td:eq(' + col + ')').attr('ID_date', '1' + (dt.getMonth() + 1).toString().padLeft(2) + '_' + dt.getDate().toString().padLeft(2));
                //$('#B1dTable tr:eq(2) td:eq(' + col + ')').attr('ID_date', '2' + (dt.getMonth() + 1).toString().padLeft(2) + '_' + dt.getDate().toString().padLeft(2));
                //console.log('date is ' + $('td[ID_date*=1' + ((dt.getMonth() + 1) + '_' + dt.getDate()).toString() + ']').attr('ID_date'));
                //col += 1;
            }

            //用Linq直接組
            var result = new
            {
                DateList = from s in DateList
                                   select s

            };
            string str_json = JsonConvert.SerializeObject(result, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Write(str_json);
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