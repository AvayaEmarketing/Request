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


public partial class revisor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        public string solicit_id { get; set; }
        public string S_key_name {get;set;}
        public string nombre {get;set;}
        public string S_original_language {get;set;}
        public string S_translate_language {get;set;}
		public string S_register_date {get;set;}
        public string S_desired_date {get;set;}
        public string T_Fecha_Estimada { get; set; }
        public string S_solicit_priority { get; set; }
        
    }

    [WebMethod]
    public static string getCountries()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string strSQL = "SELECT idCountry,Country from C_Country order by Country";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);
            //result = new JavaScriptSerializer().Serialize(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            result = "fail";
            WriteError(ex.Message, "revisor.aspx", "getCountries");
        }
        finally
        {
            con.Close();
        }
        return result;

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
    public static string Convertir()
    {
        string resultado = "error0";
        try
        {
            string path = HttpContext.Current.Server.MapPath("~");
            resultado = toExcel(path);
            HttpContext.Current.Response.SetCookie(new HttpCookie("fileDownload", "true") { Path = "/" });
        }
        catch (Exception e)
        {
            resultado = e.ToString();
            WriteError(e.Message, "revisor.aspx", "Convertir");
        }
        return resultado;

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
            foreach (var root in values)
            {
                jsonarmado += "{\"S_key_name\": \"" + root.S_key_name + "\", \"nombre\": \"" + root.nombre + "\",\"S_original_language\": \"" + root.S_original_language + "\",\"S_translate_language\": \"" + root.S_translate_language + "\",\"S_register_date\": \"" + root.S_register_date + "\",\"S_desired_date\": \"" + root.S_desired_date + "\",\"T_Fecha_Estimada\": \"" + root.T_Fecha_Estimada + "\",\"S_solicit_priority\": \"" + root.S_solicit_priority + "\",\"Edit\": \"<a href=\'#\' onClick=\'detailsRev(" + root.solicit_id + ")\'><img title=\'Details\' src=\'images/edit.png\'/></a>\"},";
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

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        var sessionUsuario = HttpContext.Current.Session;
        string responsable2 = sessionUsuario["id"].ToString();
        int responsable = Convert.ToInt32(responsable2);
        //string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date, sol.S_solicit_priority from Translate_Solicits sol, Translate_State sta  where sol.responsable = @responsable and sol.estado = sta.id and S_register_date2 = (select max(S_register_date2) as fecha_registro from Translate_Solicits where responsable = @responsable)";
        string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date, sol.T_Fecha_Estimada, sol.S_solicit_priority from Translate_Solicits sol, Translate_State sta  where sol.responsable = @responsable and sol.estado = sta.id and S_visible = 'YES'";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@responsable", SqlDbType.Int);
        cmd.Parameters["@responsable"].Value = responsable;

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
            result = "";
            WriteError(ex.Message, "revisor.aspx", "getDatosRequest");
        }
        finally
        {
            con.Close();
        }
        return result;

    }

    public static int getTraductor(string original_language, string translate_language) {
        int traductor = 0;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string stmt = "select id from UserData where idioma1 = @original and idioma2 = @translate";

        SqlCommand cmd2 = new SqlCommand(stmt, con);

        cmd2.Parameters.Add("@original", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@translate", SqlDbType.VarChar, 50);

        cmd2.Parameters["@original"].Value = original_language;
        cmd2.Parameters["@translate"].Value = translate_language;

        try
        {
            con.Open();
            traductor = (Int32)cmd2.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            WriteError(ex.Message, "revisor.aspx", "getTraductor");
        }
        finally
        {
            con.Close();
        }

        return traductor;
    }

    
	
	    public static string sendMails(string nombre, string observacion, string correo)
        {
            string result = "";
            string title = "Avaya ATF"; 
            try
            {
                //string contenido = getContenidoMail(nombre, observacion);
                string plantilla = getPlantilla();
                string rta_mail = SendMail(correo, "e-marketing@avaya.com", title, plantilla);

                result = "ok";
            }
            catch (Exception ex)
            {
                result = "false" + ex;
                WriteError(ex.Message, "revisor.aspx", "sendMails");
            }
            return result;
        }

        public static string SendMail(string to, string from, string subject, string contenido)
        {
            string respuesta = "";

            MailAddress sendfrom = new MailAddress(from);
            MailAddress sendto = new MailAddress(to);
            MailMessage message = new MailMessage();

            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            string body = HttpUtility.HtmlDecode(contenido);
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            message.AlternateViews.Add(alternate);

            message.From = new MailAddress(from, "Avaya Translation Requests");
            message.To.Add(to);
            message.Subject = subject;

            SmtpClient client = new SmtpClient("localhost");

            try
            {
                client.Send(message);
                respuesta = "ok";

            }
            catch (SmtpException e)
            {
                respuesta = "fail";
                throw new SmtpException(e.Message);
                WriteError(e.Message, "revisor.aspx", "SendMail");

            }
            return respuesta;
        }

        public static string getContenidoMail(string nombre, string observacion)
        {
            string plantilla = getPlantilla();

            Dictionary<string, string> dataIndex = new Dictionary<string, string>();
            dataIndex.Add("{nombre}", nombre);
            dataIndex.Add("{evento}", observacion);
            
            string buscar = "";
            string reemplazar = "";
            string index = "";
            //Recorrer la plantilla del index para reemplazar el contenido
            foreach (var datos in dataIndex)
            {
                buscar = datos.Key;
                reemplazar = datos.Value;
                index = plantilla.Replace(buscar, reemplazar);
                plantilla = index;
            }

            return plantilla;
        }

        public static string getPlantilla()
        {
            string fullPath = HttpContext.Current.Server.MapPath("~");

			string html = "";
            //html = File.ReadAllText(fullPath + "sf_mail_conf_register1_html.html");
            html = File.ReadAllText(fullPath + "\\usa\\events\\ATF-2014_test\\ATF-ty-reg.html");
			return html;
        }


        public static string getContenido()
        {
            string result = "";
            string resultado = getDatosRequest();
            string tabla = "";
            var serializer = new JavaScriptSerializer();

            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

            foreach (var root in values)
            {

                tabla += "<tr>";
                tabla += "<td>" + root.S_key_name + "</td>";
                tabla += "<td>" + root.nombre + "</td>";
                tabla += "<td>" + root.S_original_language + "</td>";
                tabla += "<td>" + root.S_translate_language + "</td>";
                tabla += "<td>" + root.S_register_date + "</td>";
                tabla += "<td>" + root.S_desired_date + "</td>";
                tabla += "<td>" + root.T_Fecha_Estimada + "</td>";
                tabla += "<td>" + root.S_solicit_priority + "</td>";
                tabla += "</tr>";
            }



            result = tabla;
            return result;
        }

        public static string toExcel(string path1)
        {
            string respuesta = "";
            string path = path1 + "\\ExcelFiles\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + "ExcelFiles.xls";
            string fullPath = path1 + "\\ExcelFiles\\" + nombre;
            string contenido = getContenido();
            string data = "<tr><th width=\"10%\">Document Name</th><th width=\"10%\">State</th><th width=\"10%\">Original Language</th><th width=\"10%\">Translation Language</th><th width=\"10%\">Register Date</th><th width=\"10%\">Desired Date</th><th width=\"10%\">Estimated Date</th><th width=\"10%\">Priority</th></tr>";
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
            catch (Exception ex)
            {
                respuesta = "fail";
                WriteError(ex.Message, "revisor.aspx", "toExcel");
            }
            finally
            {
                respuesta = nombre;
            }
            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static string validaSession()
        {
            string result = "";
            var sessionUsuario = HttpContext.Current.Session;
            if (sessionUsuario["id"] == null)
            {
                result = "fail";
            }
            else
            {
                result = sessionUsuario["id"].ToString();
            }
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
            catch (Exception ex)
            {
                resultado = "fail";
                WriteError(ex.Message, "revisor.aspx", "cerrarSession");
            }
            return resultado;
        }

        //Metodo para escribir el error en un archivo log  *** Parametros error, archivo donde ocurrio el error, metodo donde ocurrio el error.
        public static string WriteError(string error, string file, string metodo)
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
            string contenido = "Date: " + ' ' + fecha + "  File: " + ' ' + file + "  Method:" + ' ' + metodo + "  Error :" + ' ' + error;

            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(contenido);
                }
            }
            catch (Exception ex)
            {
                respuesta = "fail";
                

            }
            finally
            {
                respuesta = nombre;
            }
            return respuesta;
        }

}

