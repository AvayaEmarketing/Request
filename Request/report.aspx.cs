using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


public partial class report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        public string solicit_id { get; set; }
        public string S_key_name {get;set;}
        public string solicitante {get;set;}
        public string f_solicitante {get;set;}
        public string estado {get;set;}
		public string S_register_date2 {get;set;}
        public string T_Fecha_Estimada2 {get;set;}
        public string S_Fecha_modificacion { get; set; }
        public string InCharge { get; set; }
        public string f_responsable { get; set; }
        public string S_original_language { get; set; }
        public string S_translate_language { get; set; }
        public string responsable_id { get; set; }
        public string solicitante_id { get; set; }
        public string duration { get; set; }
    }

    public static string DataTableToJSON(DataTable table)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }

    
    
    [WebMethod]
    public static string getDatosReg()
    {
        string result = "";

        if (validaSession() == "fail")
        {
            result = "fail";
        }
        else
        {
            
            string resultado = getDatosRequest();
            var serializer = new JavaScriptSerializer();
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);
            string jsonarmado = "[";
            if (values != null)
            {
                foreach (var root in values)
                {
                    jsonarmado += "{\"solicit_id\": \"" + root.solicit_id + "\", \"S_key_name\": \"" + root.S_key_name + "\",\"solicitante\": \"<p style=\'position:relative;text-align:center;width:100%;\'><a href=\'#\' onClick=\'filterBySolicitante(" + root.solicitante_id + ")\'><img  width=\'50\' height=\'50\' title=\'" + root.solicitante + "\' src=\'images/" + root.f_solicitante + "\'/></a></p>\",\"original\": \"" + root.S_original_language + "\",\"translate\": \"" + root.S_translate_language + "\",\"estado\": \"" + root.estado + "\",\"S_register_date2\": \"" + root.S_register_date2 + "\",\"T_Fecha_Estimada2\": \"" + root.T_Fecha_Estimada2  + "\",\"InCharge\": \"<p style=\'position:relative;text-align:center;width:100%;\'><a href=\'#\' onClick=\'filterByResponsable(" + root.responsable_id + ")\'><img style=\'text-align:center;\' width=\'50\' height=\'50\' title=\'" + root.InCharge + "\' src=\'images/" + root.f_responsable + "\'/></a></p>\",\"duration\": \"" + root.duration + "\"},";
                }
            }

            jsonarmado = jsonarmado.Substring(0, jsonarmado.Length - 1);
            if (jsonarmado == "")
            {
                result = "[]";
            }
            else
            {
                result = jsonarmado + "]";
            }
        }
        return result;
    }

    [WebMethod]
    public static string getDatosReg2(int user,string rol)
    {
        string result = "";

        if (validaSession() == "fail")
        {
            result = "fail";
        }
        else
        {

            string resultado = getDatosRequest2(user,rol);
            var serializer = new JavaScriptSerializer();
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);
            string jsonarmado = "[";
            if (values != null)
            {
                foreach (var root in values)
                {
                    jsonarmado += "{\"solicit_id\": \"" + root.solicit_id + "\", \"S_key_name\": \"" + root.S_key_name + "\",\"solicitante\": \"<p style=\'position:relative;text-align:center;width:100%;\'><a href=\'#\' onClick=\'filterBySolicitante(" + root.solicitante_id + ")\'><img  width=\'50\' height=\'50\' title=\'" + root.solicitante + "\' src=\'images/" + root.f_solicitante + "\'/></a></p>\",\"original\": \"" + root.S_original_language + "\",\"translate\": \"" + root.S_translate_language + "\",\"estado\": \"" + root.estado + "\",\"S_register_date2\": \"" + root.S_register_date2 + "\",\"T_Fecha_Estimada2\": \"" + root.T_Fecha_Estimada2 + "\",\"InCharge\": \"<p style=\'position:relative;text-align:center;width:100%;\'><a href=\'#\' onClick=\'filterByResponsable(" + root.responsable_id + ")\'><img style=\'text-align:center;\' width=\'50\' height=\'50\' title=\'" + root.InCharge + "\' src=\'images/" + root.f_responsable + "\'/></a></p>\",\"duration\": \"" + root.duration + "\"},";
                }
            }

            jsonarmado = jsonarmado.Substring(0, jsonarmado.Length - 1);
            if (jsonarmado == "")
            {
                result = "[]";
            }
            else
            {
                result = jsonarmado + "]";
            }
        }
        return result;
    }


    [WebMethod]
    public static string Convertir(int user,string rol)
    {
        string resultado = "error0";
        string path = "";
        try
        {
            path = HttpContext.Current.Server.MapPath("~");
            resultado = toExcel(path,user,rol);
            HttpContext.Current.Response.SetCookie(new HttpCookie("fileDownload", "true") { Path = "/" });
        }
        catch (Exception e)
        {
            
            resultado = e.ToString();
            WriteError(resultado, "solicitante.aspx", "Convertir");
        }
        return resultado;

    }

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string strSQL = "SELECT solicit_id,S_key_name,solicitante,solicitante_id,S_original_language,S_translate_language,f_solicitante,estado,convert(varchar,S_register_date2,110) as S_register_date2,convert(varchar,T_Fecha_Estimada2,110) as T_Fecha_Estimada2,CONVERT(varchar, S_Fecha_modificacion, 110) as S_Fecha_modificacion,InCharge,f_responsable,responsable_id,datediff(day,S_register_date2,T_Fecha_Estimada2) as duration FROM t_requests";
        //string strSQL = "SELECT solicit_id,S_key_name,solicitante,solicitante_id,S_original_language,S_translate_language,f_solicitante,estado,S_register_date2,T_Fecha_Estimada2,S_Fecha_modificacion,InCharge,f_responsable,responsable_id FROM t_requests";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        

        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);
            //result = JsonConvert.SerializeObject(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            result = "fail";
            string resultado = ex.ToString();
            WriteError(resultado, "report.aspx", "getDatosRequest");
        }
        finally
        {
            con.Close();
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest2(int user,string rol)
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string strSQL = "";
        if (rol == "solicitante") {
            strSQL = "SELECT solicit_id,S_key_name,solicitante,solicitante_id,S_original_language,S_translate_language,f_solicitante,estado,convert(varchar,S_register_date2,110) as S_register_date2,convert(varchar,T_Fecha_Estimada2,110) as T_Fecha_Estimada2,CONVERT(varchar, S_Fecha_modificacion, 110) as S_Fecha_modificacion,InCharge,f_responsable,responsable_id,datediff(day,S_register_date2,T_Fecha_Estimada2) as duration FROM t_requests where solicitante_id =@user";
        } else {
            strSQL = "SELECT solicit_id,S_key_name,solicitante,solicitante_id,S_original_language,S_translate_language,f_solicitante,estado,convert(varchar,S_register_date2,110) as S_register_date2,convert(varchar,T_Fecha_Estimada2,110) as T_Fecha_Estimada2,CONVERT(varchar, S_Fecha_modificacion, 110) as S_Fecha_modificacion,InCharge,f_responsable,responsable_id,datediff(day,S_register_date2,T_Fecha_Estimada2) as duration FROM t_requests where responsable_id = @user";
        }
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@user", SqlDbType.Int);
        cmd.Parameters["@user"].Value = user;

        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);
            //result = JsonConvert.SerializeObject(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            result = "fail";
            string resultado = ex.ToString();
            WriteError(resultado, "report.aspx", "getDatosRequest");
        }
        finally
        {
            con.Close();
        }
        return result;
    }

    
		
	
    	public static string getContenido(int user,string rol)
        {
            string result = "";
            string resultado = "";
            if (rol == "all")
            {
                resultado = getDatosRequest();
            }
            else {
                resultado = getDatosRequest2(user,rol);
            }
            string tabla = "";
			var serializer = new JavaScriptSerializer();
			
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

            foreach (var root in values)
            {
                 
                    tabla += "<tr>";
                    
                    tabla += "<td>" + root.solicit_id + "</td>";
                    tabla += "<td>" + root.S_key_name + "</td>";
                    tabla += "<td>" + root.solicitante + "</td>";
                    tabla += "<td>" + root.estado + "</td>";
					tabla += "<td>" + root.S_register_date2 + "</td>";
                    tabla += "<td>" + root.T_Fecha_Estimada2 + "</td>";
                    tabla += "<td>" + root.S_Fecha_modificacion + "</td>";
                    tabla += "<td>" + root.InCharge + "</td>";
                    tabla += "<td>" + root.S_original_language + "</td>";
                    tabla += "<td>" + root.S_translate_language + "</td>";
                    tabla += "<td>" + root.duration + "</td>";
                    tabla += "</tr>";
                }
                
			result = tabla;
            return result;
        }
		
		public static string toExcel(string path1,int user,string rol)
        {
            string respuesta = "";
            string path = path1 + "\\ExcelFiles\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + "ExcelFiles.xls";
            string fullPath = path1 + "\\ExcelFiles\\" + nombre;
            string contenido = getContenido(user,rol);
            string data = "<tr><th width=\"10%\">ID</th><th width=\"10%\">Name</th><th width=\"10%\">Requestor</th><th width=\"10%\">State</th><th width=\"10%\">Register Date</th><th width=\"10%\">Estimated Date</th><th width=\"10%\">Modification Date</th><th width=\"10%\">In Charge</th><th width=\"10%\">Original Language</th><th width=\"10%\">Translate Language</th><th width=\"10%\">Duration</th></tr>";
            contenido = data + contenido;
            contenido = "<table border = '1' style=" + '"' + "font-family: Verdana,Arial,sans-serif; font-size: 12px;" + '"' + ">" + contenido + "</table></body></html>";

            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
                
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(contenido);
                }
            }
            catch (Exception e)
            {
                respuesta = "fail";
                string resultado = e.Message;
                WriteError(resultado, "report.aspx", "toExcel");
            }
            finally
            {
                respuesta = nombre;
            }
            return respuesta;
        }

        //Metodo para escribir el error en un archivo log  *** Parametros error, archivo donde ocurrio el error, metodo donde ocurrio el error.
        public static string WriteError(string error,string file, string metodo)
        {
            string path1 = HttpContext.Current.Server.MapPath("~");
            string respuesta = "";
            DateTime datt = DateTime.Now;
            string fecha = datt.ToString();
            string path = path1 + "\\Errores\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string nombre = "Error.log";

            string fullPath = path1 + "\\Errores\\" + nombre;
            string contenido = "Date: " + ' ' + fecha + "  File: " + ' ' + file + "  Method:" + ' '+ metodo + "  Error :" + ' ' + error;

            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(contenido);
                }
            }
            catch
            {
                respuesta = "fail";
            }
            finally
            {
                respuesta = nombre;
            }
            return respuesta;
        }

       
        [WebMethod(EnableSession = true)]
        public static string validarIngresoAdmin(string name, string pass, string app)
        {
            
            string result = validarIngreso(name, pass, app);
            if (result != "fail")
            {
                var sessionUsuario = HttpContext.Current.Session;
                sessionUsuario["ID"] = name;
            }
            return result;
        }

        public static string validarIngreso(string name, string pass, string app)
        {
            
            string resultado = "";
            string usuario;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

            string strSQL = "SELECT distinct (usuario + ',' + rol) as data from UserData where usuario = @name and password = @pass and application = @app";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@pass", SqlDbType.VarChar, 300); 
            cmd.Parameters.Add("@app", SqlDbType.VarChar, 100);

            cmd.Parameters["@name"].Value = name;
            cmd.Parameters["@pass"].Value = pass;
            cmd.Parameters["@app"].Value = app;

            try
            {
                con.Open();
                usuario = (String)cmd.ExecuteScalar();
                con.Close();
                var test = usuario.Split(',');
                string nombre = test[0];
                string rol = test[1];

                resultado = (name == nombre) ? rol : "fail";
                
            }
            catch (Exception ex)
            {
                resultado = "fail";
                string resultado2 = ex.ToString();
                WriteError(resultado2, "solicitante.aspx", "SendMail");

            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

         

        [WebMethod(EnableSession = true)]
        public static string validaSession()
        {
            string result = "";
            var sessionUsuario = HttpContext.Current.Session;
            result = (sessionUsuario["id"] == null) ? "fail": sessionUsuario["id"].ToString();
            return result;
        }

        //Función que permite obtener el nombre del usuario solicitante
        [WebMethod(EnableSession = true)]
        public static string obtenerDatosUsuario ( )
        {
            string result = "";
            var sessionUsuario = HttpContext.Current.Session;
            string Datos = sessionUsuario["name"].ToString() + "," + sessionUsuario["foto"].ToString();
            result = (sessionUsuario["id"] == null) ? "fail" : Datos;
            return result;
        }

        
        [WebMethod(EnableSession = true)]
        public static string cerrarSession()
        {
            var sessionUsuario = HttpContext.Current.Session;
            var resultado = "";
            try
            {
                sessionUsuario.Clear();
                sessionUsuario.Abandon();
                resultado = "ok";
            }
            catch (Exception)
            {
                resultado = "fail";
            }
            return resultado;
        }

}

