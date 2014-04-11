using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    /// <summary>
    /// Descripción breve de DescargarArchivo
    /// </summary>
    public class DescargarArchivo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string filename = context.Request.QueryString["File"];
            string stringParam = (string)context.Request["carpeta"];
            //Validate the file name and make sure it is one that the user may access
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            context.Response.ContentType = "application/octet-stream";
            int fileExtPos = filename.LastIndexOf(".");
            
            string path = HttpContext.Current.Server.MapPath("~");
            path = path + "\\" + stringParam + "\\";
            context.Response.WriteFile(path + filename);
            
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
