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


public partial class Request_details : System.Web.UI.Page
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
            Console.WriteLine(ex.Message);
            result = "";
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
    public static string getDatosReg()
    {
        string result = "";
        string resultado = getDatosRequest();
        var serializer = new JavaScriptSerializer();
        List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

        string jsonarmado = "[";
        foreach (var root in values)
        {
            jsonarmado += "{\"solicit_id\": \"" + root.solicit_id + "\", \"S_key_name\": \"" + root.S_key_name + "\", \"nombre\": \"" + root.nombre + "\",\"S_original_language\": \"" + root.S_original_language + "\",\"S_translate_language\": \"" + root.S_translate_language + "\",\"S_register_date\": \"" + root.S_register_date + "\",\"S_desired_date\": \"" + root.S_desired_date + "\",\"Edit\": \"<a href=\'#\' onClick=\'editSession(" + root.solicit_id + ")\'><img title=\'Edit\' src=\'images/edit.png\'/></a>\"},";
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
        string solicitante2 = sessionUsuario["id"].ToString();
        int solicitante = Convert.ToInt32(solicitante2);
        string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date  from Cala_Web.Translate_Solicits sol, Cala_Web.Translate_State sta  where sol.solicitante_id = @solicitante and sol.estado = sta.id";
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
            Console.WriteLine(ex.Message);
            result = "";
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
        string stmt = "select id from Cala_Web.UserData where idioma1 = @original and idioma2 = @translate";

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
            Console.WriteLine(ex.Message);
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
		string resultado = "";
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
            Console.WriteLine(ex.Message);
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
        string[] words = translate_language.Split(',');
        Dictionary<Int32, string> traductor_idioma = new Dictionary<Int32, string>();
        foreach (string word in words)
        {
            //Console.WriteLine(word);
            traductor = getTraductor(original_language, word);
            traductor_idioma.Add(traductor, word);
            j = j + 1;
        }
        
        int[] arr4 = new int[j]; 
        

        foreach (var datos in traductor_idioma)
        {
            traductor = datos.Key;
            idioma = datos.Value;
            arr4[i] = guardarDatos(translation_name,solicitante, traductor, state, document_type, original_language, idioma, prioridad, priority_comment, observations, datt, desired_date,document_name);
            i = i + 1;
        }

        for (int k = 0; k < arr4.Length; k++)
        {
            if (arr4[k] != -1)
            {
                result = result + arr4[k].ToString() + ',';
            }
        }





        //if (result == "ok")
        //{
        //    string nombrec = inputName + " " + inputLast;
        //    resultado = sendMails(nombrec, inputCompany, inputEmail);
        //}
        return result;
    }

    public static int guardarDatos(string translation_name,int solicitante, int traductor,  int state, int document_type, string original_language, string translate_language, string prioridad,string priority_comment,string observations,DateTime datt,string desired_date, string document_name) {
        int result;
       
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        desired_date = desired_date.Replace("/", "-");
        DateTime dt = DateTime.ParseExact(desired_date + " 00:00:00", "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        string stmt = "INSERT INTO Cala_Web.Translate_Solicits (S_Key_name, solicitante_id, responsable, estado, S_document_type,S_original_language,S_translate_language,S_solicit_priority,S_priority_comment,S_observations,S_register_date,S_register_date2,S_desired_date,S_desired_date2,S_document_name) OUTPUT INSERTED.solicit_id VALUES (@translation_name,@solicitante, @traductor, @state, @document_type, @original_language, @translate_language, @prioridad, @priority_comment, @comments, @register_date, @register_date2, @desired_date, @desired_date2, @document_name)";

        SqlCommand cmd2 = new SqlCommand(stmt, con);
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
        cmd2.Parameters["@desired_date"].Value = desired_date;
        cmd2.Parameters["@desired_date2"].Value = dt;
        cmd2.Parameters["@document_name"].Value = document_name;

        try
        {
            con.Open();
            result = (Int32)cmd2.ExecuteScalar();
            con.Close();
            
        }
        catch (Exception ex)
        {
            result = -1;
            Console.WriteLine(ex.Message);
        }
        finally
        {
            con.Close();
        }
        return result;
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

            message.From = new MailAddress(from);
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
            string resultado = getDatosReg();
            string tabla = "";
			var serializer = new JavaScriptSerializer();
			
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);
            
            foreach (var root in values)
            {
                 
                    tabla += "<tr>";
                    tabla += "<td>" + root.solicit_id + "</td>";
                    tabla += "<td>" + root.S_key_name + "</td>";
                    tabla += "<td>" + root.nombre + "</td>";
                    tabla += "<td>" + root.S_original_language + "</td>";
                    tabla += "<td>" + root.S_translate_language + "</td>";
					tabla += "<td>" + root.S_register_date + "</td>";
                    tabla += "<td>" + root.S_desired_date + "</td>";
                    
                    
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
            string data = "<tr><th width=\"10%\">First Name</th><th width=\"10%\">Last Name</th><th width=\"10%\">Email</th><th width=\"10%\">Company</th><th width=\"10%\">Phone</th><th width=\"10%\">Address</th><th width=\"10%\">Job</th><th width=\"10%\">Country</th><th width=\"10%\">Organization</th><th width=\"10%\">Conference Role</th><th width=\"10%\">Question 1</th><th width=\"10%\">Question 2</th><th width=\"10%\">Diet</th><th width=\"10%\">Emergency Name</th><th width=\"10%\">Emergency #</th><th width=\"10%\">Send Info</th><th width=\"10%\">Register Date</th><th width=\"10%\">Twitter</th></tr>";
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
            if (result == "ok")
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

                if (name == nombre)
                {
                    resultado = rol;
                }
                else
                {
                    resultado = "fail";
                }
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

        [WebMethod]
        public static string validateEmail(string email)
        {
            
            string resultado = "";
            string usuario;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

            string strSQL = "SELECT distinct email from Tbl_ATF where email = '"+email+ "'";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            try
            {
                con.Open();
                usuario = (String)cmd.ExecuteScalar();
                con.Close();
                if (usuario == null)
                {
                    resultado = "fail";
                }
                else
                {
                    resultado = "ok";
                }
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

    

        [WebMethod(EnableSession = true)]
        public static string validaSession()
        {
            string result = "";
            var sessionUsuario = HttpContext.Current.Session;
            if (sessionUsuario["ID"] == null)
            {
                result = "fail";
            }
            else
            {
                result = sessionUsuario["ID"].ToString();
            }
            return result;
        }

        [WebMethod]
        public static string getDatosOld(string email)
        {
            string result;
            SqlDataReader datos;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
            string strSQL = "select nombre,apellido,empresa,titleContact,email,telefono,postal,empleados,idCountry,registerDate from Cala_Web.General2 where email ='" + email + "'  and registerDate = (select max(registerDate) as fecha_registro from Cala_Web.General2 where email = '" + email + "')";
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
                Console.WriteLine(ex.Message);
                result = "";
            }
            finally
            {
                con.Close();
            }
            return result;

        }



        
        



}

