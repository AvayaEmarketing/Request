<%@ WebHandler Language="C#" Class="AjaxFileUploader4" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

    /// <summary>
    /// Descripción breve de AjaxFileuploader
    /// </summary>
    public class AjaxFileUploader4 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
		{
			if (context.Request.Files.Count > 0)
			{
				string error = "";
				string resultado = "";
				string path = context.Server.MapPath("~");
				path = context.Server.MapPath("~") + "\\Reviews";
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				var file = context.Request.Files[0];

				string fileName;
				string hora = string.Format("{0:HH_mm_ss}", System.DateTime.Now);

				fileName = System.IO.Path.GetFileName(file.FileName);
			    fileName = fileName.Replace(" ", "_");
				string fullPath = HttpContext.Current.Server.MapPath("~") + "\\Reviews\\" + hora + "-" + fileName;
				string strFileName = fileName;
				try {
					file.SaveAs(fullPath);
					resultado = "ok";
				} 
				catch (Exception ex) {
					resultado = "fail";
					error = ex.Message;
					Console.WriteLine(ex.Message);
				}
				
				
				if (resultado == "ok") {
					string stringParam = (string)context.Request["solicit_id"];
					int i = Convert.ToInt32(stringParam);
					resultado = actualizar(i,hora + "-" + fileName);
				}
				
				string msg = "{";
				msg += string.Format("error:'{0}',\n", error);
				msg += string.Format("msg:'{0}'\n", hora + "-" + fileName);
				msg += "}";
				context.Response.Write(msg);

			}
		}
		
		public static string actualizar(int id, string nombre)
		{
			
			string resultado = "";
			
			SqlConnection con = new SqlConnection();
			con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

			string strSQL = "update Translate_Solicits set RT_review = @nombre where solicit_id = @id";
			SqlCommand cmd = new SqlCommand(strSQL, con);
			cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 150);
			cmd.Parameters.Add("@id", SqlDbType.Int); 
			
			
			cmd.Parameters["@nombre"].Value = nombre;
			cmd.Parameters["@id"].Value = id;

			try
			{
				con.Open();
				cmd.ExecuteNonQuery();
				resultado = "ok";
				
			}
			catch (Exception ex)
			{
				resultado = "fail";
				Console.WriteLine(ex.Message);
			}
			finally
			{
				con.Close();
			}

			return resultado;
		}
		
		
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
    }
