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


public partial class solicitante : System.Web.UI.Page
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
                    jsonarmado += "{\"S_key_name\": \"" + root.S_key_name + "\", \"nombre\": \"" + root.nombre + "\",\"S_original_language\": \"" + root.S_original_language + "\",\"S_translate_language\": \"" + root.S_translate_language + "\",\"S_register_date\": \"" + root.S_register_date + "\",\"S_desired_date\": \"" + root.S_desired_date + "\",\"T_Fecha_Estimada\": \"" + root.T_Fecha_Estimada + "\",\"S_solicit_priority\": \"" + root.S_solicit_priority + "\",\"Edit\": \"<a href=\'#\' onClick=\'editSolicit(" + root.solicit_id + ")\'><img title=\'Details\' src=\'images/edit.png\'/></a>\"},";
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
    public static string Convertir()
    {
        string resultado = "error0";
        string path = "";
        try
        {
            path = HttpContext.Current.Server.MapPath("~");
            resultado = toExcel(path);
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
        var sessionUsuario = HttpContext.Current.Session;
        string solicitante2 = sessionUsuario["id"].ToString();
        int solicitante = Convert.ToInt32(solicitante2);
        string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date, sol.T_Fecha_Estimada, sol.S_solicit_priority  from Translate_Solicits sol, Translate_State sta  where sol.solicitante_id = @solicitante and sol.estado = sta.id and S_visible = 'YES' and S_Fecha_modificacion = (select max(S_Fecha_modificacion) as fecha_registro from Translate_Solicits where solicit_id = sol.solicit_id) ";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicitante", SqlDbType.Int);
        cmd.Parameters["@solicitante"].Value = solicitante;

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
            WriteError(resultado, "solicitante.aspx", "getDatosRequest");
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
        string stmt = "select Key_s from UserData where idioma1 = @original and idioma2 = @translate and rol = 'traductor'";

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
            string resultado = ex.ToString();
            WriteError(resultado, "solicitante.aspx", "getTraductor");
        }
        finally
        {
            con.Close();
        }

        return traductor;
    }

    [WebMethod(EnableSession = true)]
    public static string putData(string translation_name,int document_type, string original_language, string translate_language, string desired_date, string prioridad, string priority_comment, string observations, string document_name)
    {
        string result = "";
		DateTime datt = DateTime.Now;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "SELECT CURRENT_TIMESTAMP AS registerDate";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        try
        {
            con.Open();
            datt = (DateTime)cmd.ExecuteScalar();
            con.Close();
            
        }
        catch (Exception ex)
        {
            string resultado = ex.Message;
            WriteError(resultado, "solicitante.aspx","putData");
        }
        finally
        {
            con.Close();
        }

        
        var sessionUsuario = HttpContext.Current.Session;
        int traductor = 0;
        string idioma = "";
        string solicitante2 = sessionUsuario["id"].ToString();
        int solicitante = Convert.ToInt32(solicitante2);
        int state = 1;  //estado = Requerida
        int i = 0;
        int j = 0;
        string error = "ok";
        string[] words = translate_language.Split(',');
        Dictionary<Int32, string> traductor_idioma = new Dictionary<Int32, string>();
        foreach (string word in words)
        {
            traductor = getTraductor(original_language, word);
            traductor_idioma.Add(traductor, word);
            j = j + 1;
        }
        
        int[] arr4 = new int[j];

        string errores = "";
        foreach (var datos in traductor_idioma)
        {
            traductor = datos.Key;
            idioma = datos.Value;
            arr4[i] = guardarDatos(translation_name,solicitante, traductor, state, document_type, original_language, idioma, prioridad, priority_comment, observations, datt, desired_date,document_name);
            if (arr4[i] != -1)
            {
               error = sendMails(translation_name, solicitante, traductor, state, document_type, original_language, idioma, prioridad, priority_comment, observations, datt, desired_date, document_name);
            }
            else {
                error = "fail";  
            }
            i = i + 1;
        }

        for (int k = 0; k < arr4.Length; k++)
        {
            if (arr4[k] != -1)
            {
                result = result + arr4[k].ToString() + ',';
            }
        }
        if (error != "ok") {
            result = error;
        }
       
        return result;
    }

    public static int getId(SqlConnection con, string consulta) { 
        SqlCommand cmd2 = new SqlCommand(consulta, con);
        int id = 0;
        try
        {
            con.Open();
            id = (Int32)cmd2.ExecuteScalar();
            con.Close();

        }
        catch (Exception ex)
        {
            id = -1;
            string resultado = ex.ToString();
            WriteError(resultado, "solicitante.aspx","getId");
        }
        finally
        {
            con.Close();
        } 
            return id;
    }

    public static int guardarDatos(string translation_name,int solicitante, int traductor,  int state, int document_type, string original_language, string translate_language, string prioridad,string priority_comment,string observations,DateTime datt,string desired_date, string document_name) {
        int result = 0;
        
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        desired_date = desired_date.Replace("/", "-");
        desired_date = desired_date + " 00:00:00";
        DateTime dt = DateTime.ParseExact(desired_date, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        string stmt = "INSERT INTO Translate_Solicits (solicit_id,S_Key_name, solicitante_id, responsable, estado, S_document_type,S_original_language,S_translate_language,S_solicit_priority,S_priority_comment,S_observations,S_register_date,S_register_date2,S_desired_date,S_desired_date2,S_document_name,S_visible,S_Fecha_modificacion) OUTPUT INSERTED.solicit_id VALUES (@solicit_id,@translation_name,@solicitante, @traductor, @state, @document_type, @original_language, @translate_language, @prioridad, @priority_comment, @comments, @register_date, @register_date2, @desired_date, @desired_date2, @document_name, @S_visible,@fecha_m)";

        SqlCommand cmd2 = new SqlCommand(stmt, con);
        cmd2.Parameters.Add("@solicit_id", SqlDbType.Int);
        cmd2.Parameters.Add("@translation_name", SqlDbType.VarChar, 200);
        cmd2.Parameters.Add("@solicitante", SqlDbType.Int);
        cmd2.Parameters.Add("@traductor", SqlDbType.Int);
        cmd2.Parameters.Add("@state", SqlDbType.Int);
        cmd2.Parameters.Add("@document_type", SqlDbType.Int);
        cmd2.Parameters.Add("@original_language", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@translate_language", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@prioridad", SqlDbType.VarChar, 10);
        cmd2.Parameters.Add("@priority_comment", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@comments", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@register_date", SqlDbType.VarChar, 60);
        cmd2.Parameters.Add("@register_date2", SqlDbType.DateTime);
        cmd2.Parameters.Add("@desired_date", SqlDbType.VarChar, 60);
        cmd2.Parameters.Add("@desired_date2", SqlDbType.DateTime);
        cmd2.Parameters.Add("@document_name", SqlDbType.VarChar,150);
        cmd2.Parameters.Add("@S_visible", SqlDbType.VarChar,4);
        cmd2.Parameters.Add("@fecha_m", SqlDbType.DateTime);

        cmd2.Parameters["@solicit_id"].Value = getId(con,"select max(solicit_id)+1 from Translate_Solicits");
        cmd2.Parameters["@translation_name"].Value = translation_name;
        cmd2.Parameters["@solicitante"].Value = solicitante;
        cmd2.Parameters["@traductor"].Value = traductor;
        cmd2.Parameters["@state"].Value = state;
        cmd2.Parameters["@document_type"].Value = document_type;
        cmd2.Parameters["@original_language"].Value = original_language;
        cmd2.Parameters["@translate_language"].Value = translate_language;
        cmd2.Parameters["@prioridad"].Value = prioridad;
        cmd2.Parameters["@priority_comment"].Value = priority_comment;
        cmd2.Parameters["@comments"].Value = observations;
        cmd2.Parameters["@register_date"].Value = datt;
        cmd2.Parameters["@register_date2"].Value = datt;
        cmd2.Parameters["@desired_date"].Value = dt;
        cmd2.Parameters["@desired_date2"].Value = dt;
        cmd2.Parameters["@document_name"].Value = document_name;
        cmd2.Parameters["@S_visible"].Value = "YES";
        cmd2.Parameters["@fecha_m"].Value = datt;

        try
        {
            con.Open();
            result = (Int32)cmd2.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            result = -1;
            string resultado = ex.ToString();
            WriteError(resultado, "solicitante.aspx","guardarDatos");
        }
        finally
        {
            con.Close();
        }
        return result;
    }

    public static string getCorreo(int traductor) {
        string resultado = "";
        string mail;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "SELECT email_empresa from UserData where id = @user";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@user", SqlDbType.Int);
        
        cmd.Parameters["@user"].Value = traductor;

        try
        {
            con.Open();
            mail = (String)cmd.ExecuteScalar();
            con.Close();
            resultado = mail;
            
        }
        catch (Exception ex)
        {
            resultado = "fail";
            string resultado2 = ex.Message;
            WriteError(resultado2, "solicitante.aspx","getCorreo");
        }
        finally
        {
            con.Close();
        }

        return resultado;
    }

        public static string sendMails(string translation_name, int solicitante, int traductor, int state, int document_type, string original_language, string translate_language, string prioridad, string priority_comment, string observations, DateTime datt, string desired_date, string document_name)
        {
            string result = "";
            string title = "Avaya Translation Requests";
            string correo = getCorreo(traductor);
            string correo2 = getCorreo(solicitante);
            string data = "<table width=\"500\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\" style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px;margin-top:14pt;margin-bottom:14pt;\"><tr><td style=\"color:#cc0000;\">Translation Name:</td><td>" + translation_name + "</td></tr><tr><td style=\"color:#cc0000;\">Original Language: </td><td>" + original_language + "</td></tr><tr><td style=\"color:#cc0000;\">Translate Language:</td><td>" + translate_language + "</td></tr><tr><td style=\"color:#cc0000;\"> Priority:</td><td>" + prioridad + "</td></tr><tr><td style=\"color:#cc0000;\"> Priority Comment:</td><td>" + priority_comment + "</td></tr><tr><td style=\"color:#cc0000;\"> Desired Date:</td><td>" + desired_date + "</td></tr></table>"; 
            try

            {
                string plantilla = getContenidoMail(data, "traductor");
                string rta_mail = SendMail(correo, "e-marketing@avaya.com", title, plantilla);
                
                plantilla = getContenidoMail(data, "solicitante");
                rta_mail = SendMail(correo2, "e-marketing@avaya.com", title, plantilla);

                result = rta_mail;
            }
            catch (Exception ex)
            {
                result = "false" + ex.Message;
                string resultado = ex.Message;
                WriteError(resultado, "solicitante.aspx","sendMails");
            }
            return result;
            
        }

        public static string SendMail(string to, string from, string subject, string contenido)
        {
            string respuesta = "";

            MailAddress sendfrom = new MailAddress(from,"Avaya Translation Requests");
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
                respuesta = "fail" + e.Message;
                throw new SmtpException(e.Message);
                string resultado = e.Message;
                WriteError(resultado, "solicitante.aspx","SendMail");

            }
            return respuesta;
        }

        public static string getContenidoMail(string data, string tipo_destinatario)
        {
            
            string plantilla = getPlantilla(tipo_destinatario);
            

            Dictionary<string, string> dataIndex = new Dictionary<string, string>();
            dataIndex.Add("{data}", data);
            
            
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

        public static string getPlantilla(string tipo_destinatario)
        {
            string fullPath = HttpContext.Current.Server.MapPath("~");

			string html = "";
            if (tipo_destinatario == "solicitante")
            {
                html = File.ReadAllText(fullPath + "\\mails\\solicitante.html");
            }
            else if (tipo_destinatario == "traductor")
            {
                html = File.ReadAllText(fullPath + "\\mails\\solicitud_a_traductor.html");
            }
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
            catch (Exception e)
            {
                respuesta = "fail";
                string resultado = e.Message;
                WriteError(resultado, "solicitante.aspx", "SendMail");
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

