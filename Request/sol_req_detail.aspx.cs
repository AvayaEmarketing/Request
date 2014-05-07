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


public partial class sol_req_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        public string solicit_id { get; set; }
        public string S_Key_name {get;set;}
        public string nombre {get;set;}
        public string S_original_language {get;set;}
        public string S_translate_language {get;set;}
		public string S_register_date {get;set;}
        public string S_desired_date {get;set;}
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

    [WebMethod]
    public static string getRequest(int id)
    {
        string result = "";
        if (validaSession() == "fail")
        {
            result = "fail";
        }
        else
        {
            result = getDatosRequest(id);
        }
        
        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest(int id)
    {
        string result;
        string user;
        int usuario;
        SqlDataReader datos;

        SqlConnection con = new SqlConnection();
        user = validaSession(); 
        if ( user != "fail")
        {
            usuario = Convert.ToInt32(user);
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
            string strSQL = "select solicit_id,S_Key_name, solicitante_id, responsable, estado, S_document_type, S_original_language, S_translate_language, S_solicit_priority, S_priority_comment, S_observations, S_register_date, S_register_date2, S_desired_date, S_desired_date2, S_document_name, T_Fecha_Estimada, T_Fecha_Estimada2, T_Observaciones, T_requiere_revision, T_send_feedback, TR_send_review , sta.nombre , T_document_translate, TR_format_translate,ud.nombre,ud.apellido from Translate_Solicits sol,Translate_State sta, UserData ud  where ud.id = sol.solicitante_id and sol.solicit_id = @id and sol.estado = sta.id and sol.solicitante_id = @user and S_visible = 'YES' and S_Fecha_modificacion = (select max(S_Fecha_modificacion) as fecha_registro from Translate_Solicits where solicit_id = @id)";
            
            SqlCommand cmd = new SqlCommand(strSQL, con);
            
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters.Add("@user", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters["@user"].Value = usuario;

            try
            {
                con.Open();
                datos = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(datos);
                result = DataTableToJSON(dt);
                con.Close();

            }
            catch (Exception ex)
            {
                result = "fail";
                WriteError(ex.Message, "sol_req_detail.aspx", "getDatosRequest");

            }
            finally
            {
                con.Close();
            }
        }
        else {
            result = "fail";
        }
        
        return result;

    }

       
	// **************************  Envio de Correccion al traductor *******************************************//                                   
    [WebMethod]
        public static string putDataCorrection(int solicit_id, int solicitante_id, int responsable, int estado, string S_document_type, string S_document_name, string S_original_language, string S_translate_language, string S_solicit_priority, string S_priority_comment, string S_observations, string S_register_date, string S_desired_date, string S_Key_name, string estimated_date, string observations_feedback, int estado_rev, string revision, string correction, string c_observations)
    {
        string result = "";
        string error = "";
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
            WriteError(ex.Message, "sol_req_detail.aspx", "putDataCorrection");
        }
        finally
        {
            con.Close();
        }

        string stmt = "INSERT INTO Translate_Solicits (solicit_id,S_Key_name, solicitante_id, responsable, estado, S_document_type,S_original_language,S_translate_language,S_solicit_priority,S_priority_comment,S_observations,S_register_date,S_register_date2,S_desired_date,S_desired_date2,S_document_name,T_Fecha_Estimada,T_Fecha_Estimada2,T_Observaciones,T_requiere_revision, T_send_feedback,ST_correction,ST_observations,ST_send_correction, S_visible, S_Fecha_modificacion)  VALUES (@solicit_id,@translation_name,@solicitante, @traductor, @state, @document_type, @original_language, @translate_language, @prioridad, @priority_comment, @comments, @register_date, @register_date2, @desired_date, @desired_date2, @document_name,@estimated_date,@estimated_date2,@T_observation,@T_revision,@feedback,@correction,@c_observation, @send_correction, @S_visible, @fecha_m)";

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
        cmd2.Parameters.Add("@document_name", SqlDbType.VarChar, 150);
        cmd2.Parameters.Add("@estimated_date", SqlDbType.VarChar, 60);
        cmd2.Parameters.Add("@estimated_date2", SqlDbType.DateTime);
        cmd2.Parameters.Add("@T_observation", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@T_revision", SqlDbType.VarChar, 4);
        cmd2.Parameters.Add("@feedback", SqlDbType.VarChar, 4);
        cmd2.Parameters.Add("@correction", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@c_observation", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@send_correction", SqlDbType.VarChar, 4);
        cmd2.Parameters.Add("@S_visible", SqlDbType.VarChar, 4);
        cmd2.Parameters.Add("@fecha_m", SqlDbType.DateTime);

        cmd2.Parameters["@solicit_id"].Value = solicit_id;
        cmd2.Parameters["@translation_name"].Value = S_Key_name;
        cmd2.Parameters["@solicitante"].Value = solicitante_id;
        cmd2.Parameters["@traductor"].Value = responsable;
        cmd2.Parameters["@state"].Value = estado_rev;
        cmd2.Parameters["@document_type"].Value = S_document_type;
        cmd2.Parameters["@original_language"].Value = S_original_language;
        cmd2.Parameters["@translate_language"].Value = S_translate_language;
        cmd2.Parameters["@prioridad"].Value = S_solicit_priority;
        cmd2.Parameters["@priority_comment"].Value = S_priority_comment;
        cmd2.Parameters["@comments"].Value = observations_feedback;
        cmd2.Parameters["@register_date"].Value = S_register_date;
        cmd2.Parameters["@register_date2"].Value = S_register_date;
        cmd2.Parameters["@desired_date"].Value = S_desired_date;
        cmd2.Parameters["@desired_date2"].Value = S_desired_date;
        cmd2.Parameters["@document_name"].Value = S_document_name;
        cmd2.Parameters["@estimated_date"].Value = estimated_date;
        cmd2.Parameters["@estimated_date2"].Value = estimated_date;
        cmd2.Parameters["@T_observation"].Value = observations_feedback;
        cmd2.Parameters["@T_revision"].Value = revision;
        cmd2.Parameters["@feedback"].Value = "YES";
        cmd2.Parameters["@correction"].Value = correction;
        cmd2.Parameters["@c_observation"].Value = c_observations;
        cmd2.Parameters["@send_correction"].Value = "YES";
        cmd2.Parameters["@S_visible"].Value = "YES";
        cmd2.Parameters["@fecha_m"].Value = datt;

        try
        {
            con.Open();
            cmd2.ExecuteScalar();
            result = "ok";
            con.Close();

        }
        catch (Exception ex)
        {
            result = "fail";
            WriteError(ex.Message, "sol_req_detail.aspx", "putDataCorrection");
        }
        finally
        {
            con.Close();
        }

        if (result == "ok") {
            error = sendMails(solicit_id, S_Key_name, solicitante_id, responsable, "envio");
            updateStateRequest(solicit_id, solicitante_id, responsable, 12, datt);

        }
        return result;
        
    }

    public static void updateStateRequest(int solicit_id, int solicitante_id, int responsable, int estado, DateTime datt)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "update Translate_Solicits set S_visible = 'NO', S_Fecha_modificacion = @fecha_m where solicit_id = @solicit_id and solicitante_id = @solicitante_id and responsable = @responsable and estado = 3";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicit_id", SqlDbType.Int);
        cmd.Parameters.Add("@solicitante_id", SqlDbType.Int);
        cmd.Parameters.Add("@responsable", SqlDbType.Int);
        cmd.Parameters.Add("@fecha_m", SqlDbType.DateTime);
        
        cmd.Parameters["@solicit_id"].Value = solicit_id;
        cmd.Parameters["@solicitante_id"].Value = solicitante_id;
        cmd.Parameters["@responsable"].Value = responsable;
        cmd.Parameters["@fecha_m"].Value = datt;
        
        
        try
        {
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            WriteError(ex.Message, "sol_req_detail.aspx", "updateStateRequest");
        }
        finally
        {
            con.Close();
        }
    }


    // ************** Trae el ID del Revisor  **************  2 Parametros  Lenguage Origina y Lenguaje Traducido ****************************//
    public static int getRevisor(string original_language, string translate_language)
    {
        int traductor = 0;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string stmt = "select Key_s from UserData where idioma1 = @original and idioma2 = @translate and rol = 'revisor'";

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
            WriteError(ex.Message, "sol_req_detail.aspx", "getRevisor");
        }
        finally
        {
            con.Close();
        }

        return traductor;
    }

    // ************** Trae el ID del Traductor  **************  2 Parametros  Lenguage Original y Lenguaje Traducido ****************************//
    public static int getTraductor(string original_language, string translate_language)
    {
        int traductor = 0;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string stmt = "select key_s from UserData where idioma1 = @original and idioma2 = @translate and rol = 'traductor'";

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
            WriteError(ex.Message, "sol_req_detail.aspx", "getTraductor");
        }
        finally
        {
            con.Close();
        }

        return traductor;
    }

    public static void updateEstadoRevisor(int solicit_id, int solicitante_id, int revisor, int estado) {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "update Translate_Solicits set estado = @estado  where solicit_id = @solicit_id and solicitante_id = @solicitante_id and responsable = @responsable and estado = 6";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicit_id", SqlDbType.Int);
        cmd.Parameters.Add("@solicitante_id", SqlDbType.Int);
        cmd.Parameters.Add("@responsable", SqlDbType.Int);
        cmd.Parameters.Add("@estado", SqlDbType.Int);
        
        cmd.Parameters["@solicit_id"].Value = solicit_id;
        cmd.Parameters["@solicitante_id"].Value = solicitante_id;
        cmd.Parameters["@responsable"].Value = revisor;
        cmd.Parameters["@estado"].Value = estado;
        
        try
        {
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            WriteError(ex.Message, "sol_req_detail.aspx", "updateEstadoRevisor");
        }
        finally
        {
            con.Close();
        }
    }

    public static void updateSolicits(int id) {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "update Translate_Solicits set S_visible = 'NO' where solicit_id = @solicit_id and estado <> 7";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicit_id", SqlDbType.Int);
        
        cmd.Parameters["@solicit_id"].Value = id;
        
        try
        {
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            WriteError(ex.Message, "sol_req_detail.aspx", "updateSolicits");
        }
        finally
        {
            con.Close();
        }
    }

    [WebMethod]
    public static string closeRequest(int id) {
        string result = "";
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "update Translate_Solicits set S_visible = 'NO', estado = 10 where solicit_id = @solicit_id and estado = 7";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicit_id", SqlDbType.Int);

        cmd.Parameters["@solicit_id"].Value = id;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
            result = "ok";
        }
        catch (Exception ex)
        {
            result = "fail";
            WriteError(ex.Message, "sol_req_detail.aspx", "closeRequest");
        }
        finally
        {
            con.Close();
        }
        if (result == "ok")
        {
            sendMails(id, "NA", 0, 0, "close");
        }
        return result;
    }


    //  ****** Aca me va a tocar colocar un registro nuevo con todos los datos y con el estado de cancelado por el usuario  ***** //
    [WebMethod]
    public static string cancelRequest(int id) {
        string result = "";
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "update Translate_Solicits set S_visible = 'NO' where solicit_id = @solicit_id";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@solicit_id", SqlDbType.Int);

        cmd.Parameters["@solicit_id"].Value = id;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
            result = "ok";
        }
        catch (Exception ex)
        {
            result = "fail";
            WriteError(ex.Message, "sol_req_detail.aspx", "cancelRequest");
        }
        finally
        {
            con.Close();
        }
        if (result == "ok") {
            sendMails(id, "NA", 0, 0, "cancel");
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
            WriteError(ex.Message, "sol_req_detail.aspx", "cerrarSession");
        }
        return resultado;
    }

    // *****************************  Metodos Utilizados para el envio de correos ***************************************//
    public static string sendMails(int solicit_id, string S_Key_name, int solicitante, int traductor, string tipo_envio)
    {
        string result = "";
        string title = "Avaya Requests - Correction Translation";
        string correo = getCorreo(traductor);
        string correo2 = getCorreo(solicitante);
        
        try
        {
            string plantilla = getContenidoMail(S_Key_name, "traductor", tipo_envio);
            string rta_mail = SendMail(correo, "e-marketing@avaya.com", title, plantilla);

            plantilla = getContenidoMail(S_Key_name, "solicitante", tipo_envio);
            rta_mail = SendMail(correo2, "e-marketing@avaya.com", title, plantilla);

            result = rta_mail;
        }
        catch (Exception ex)
        {
            result = "false" + ex;
            WriteError(ex.Message, "sol_req_detail.aspx", "sendMails");
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
            respuesta = "fail" + e.Message;
            throw new SmtpException(e.Message);
            WriteError(e.Message, "sol_req_detail.aspx", "SendMail");

        }
        return respuesta;
    }

    public static string getContenidoMail(string S_key_name, string tipo_usuario, string tipo_envio)
    {
        string title = "";
        string data = "";
        string message = "";
        string plantilla = getPlantilla();
        if (tipo_envio == "envio")
        {
            if (tipo_usuario == "solicitante")
            {
                title = "Correction send";
                data = "<p>Correction send.</p><p>Sincerely, </p><p><strong>The Avaya Americas Marketing Experience Team</strong></p></td>";
                message = "Avaya Translation Requests ";
            }
            else if (tipo_usuario == "traductor")
            {
                title = "Translation request correction";
                data = "<p>Translation request correction</p><p>Translation name :  &nbsp;" + S_key_name + "</p><br/><p>For see the correction translation click here <a href=\"http://www4.avaya.com/Requests\" target=\"_blank\" style=\"color: #CC0000; text-decoration: none;\">Avaya Translation Requests </a>.</p>";
                message = "Avaya Translation Requests ";
            }
        } else if (tipo_envio == "cancel") {
            title = "Translation Request cancelled";
            data = "<p>Translation name :  &nbsp;" + S_key_name + "</p><br/><p>For more information please click here <a href=\"http://www4.avaya.com/Requests\" target=\"_blank\" style=\"color: #CC0000; text-decoration: none;\">Avaya Translation Requests </a>.</p>";
            message = "Avaya Translation Requests ";
        }
        else if (tipo_envio == "close") {
            title = "Translation Request Finished";
            data = "<p>Translation name :  &nbsp;" + S_key_name + "</p><br/><p>For more information please click here <a href=\"http://www4.avaya.com/Requests\" target=\"_blank\" style=\"color: #CC0000; text-decoration: none;\">Avaya Translation Requests </a>.</p>";
            message = "Avaya Translation Requests ";
        }


        Dictionary<string, string> dataIndex = new Dictionary<string, string>();
        dataIndex.Add("{data}", data);
        dataIndex.Add("{title}", title);
        dataIndex.Add("{message}", message);


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
        
            html = File.ReadAllText(fullPath + "\\mails\\generic_mail.html");
        
        return html;
    }


    //********************** getCorreo  :  Trael el correo del usuario enviando el id ***********************//
    public static string getCorreo(int user)
    {
        string resultado = "";
        string mail;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "SELECT email_empresa from UserData where id = @user";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@user", SqlDbType.Int);

        cmd.Parameters["@user"].Value = user;

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
            WriteError(ex.Message, "sol_req_detail.aspx", "getCorreo");
        }
        finally
        {
            con.Close();
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

