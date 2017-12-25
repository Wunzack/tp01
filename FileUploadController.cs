using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.IO;

namespace ecsfc
{
    public class FileUploadController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public string Post()
        {
            try {  
            var files=HttpContext.Current.Request.Files;
            int kd = 0;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                //var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                //if (httpPostedFile != null)
                //{
                //    // Validate the uploaded image(optional)

                //    // Get the complete file path
                //    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

                //    // Save the uploaded file to "UploadedFiles" folder
                //    httpPostedFile.SaveAs(fileSavePath);
                //}
                for (int i = 0; i < files.AllKeys.Length; i++)
                {
                    kd++;
                    // Get the complete file path
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), files[i].FileName);

                    // Save the uploaded file to "UploadedFiles" folder
                    files[i].SaveAs(fileSavePath);
                }
            }
            return "upd time is "+kd;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        // PUT api/<controller>/5
        public string Put()
        {
            var files = HttpContext.Current.Request.Files;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                //var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                //if (httpPostedFile != null)
                //{
                //    // Validate the uploaded image(optional)

                //    // Get the complete file path
                //    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

                //    // Save the uploaded file to "UploadedFiles" folder
                //    httpPostedFile.SaveAs(fileSavePath);
                //}
                for (int i = 0; i < files.AllKeys.Length; i++)
                {
                    // Get the complete file path
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), files[i].FileName);

                    // Save the uploaded file to "UploadedFiles" folder
                    files[i].SaveAs(fileSavePath);
                }
            }
            return "back yooooooooo";

        }
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}