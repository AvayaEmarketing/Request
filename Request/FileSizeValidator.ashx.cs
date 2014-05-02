using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Request
{
    /// <summary>
    /// Descripción breve de FileSizeValidator
    /// </summary>
    public class FileSizeValidator : IHttpHandler
    {

        public void ProcessRequest ( HttpContext context )
        {
           /* try
            {
                string Attachment = "";
                context.Response.ContentType = "text/plain";
                //context.Response.Expires = -1;
                string fileName = string.Empty;

                foreach (string f in context.Request.Files.AllKeys)
                {
                    HttpPostedFile file = context.Request.Files[f];
                    if (!String.IsNullOrEmpty(file.FileName))
                    {
                        Attachment = file.FileName;
                        long fileSize = file.ContentLength;
                        context.Response.Write(fileSize);
                        //context.Response.End();
                    }

                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
                var x = "";
            }*/

            //Uploaded File Deletion
            if (context.Request.QueryString.Count > 0)
            {
                string filePath = HttpContext.Current.Server.MapPath("DownloadedFiles") + "//" + context.Request.QueryString[0].ToString();
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            //File Upload
            else
            {
                var ext = System.IO.Path.GetExtension(context.Request.Files[0].FileName);
                var fileName = Path.GetFileName(context.Request.Files[0].FileName);
                HttpPostedFile file = context.Request.Files[0];
                if (context.Request.Files[0].FileName.LastIndexOf("\\") != -1)
                {
                    fileName = context.Request.Files[0].FileName.Remove(0, context.Request.Files[0].FileName.LastIndexOf("\\")).ToLower();
                }


                long fileSize = file.ContentLength;
               // fileName = GetUniqueFileName(fileName, HttpContext.Current.Server.MapPath("DownloadedFiles/"), ext).ToLower();



                string location = HttpContext.Current.Server.MapPath("DownloadedFiles/") + fileName + ext;
                //context.Request.Files[0].SaveAs(location);
                context.Response.Write(fileName + ext + fileSize);
                context.Response.End();
            }
      
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