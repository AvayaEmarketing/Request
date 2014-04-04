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


public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        
        public string email { get; set; }
        public string firstname {get;set;}
        public string lastname {get;set;}
        public string company {get;set;}
        public string phone {get;set;}
		public string address {get;set;}
        public string job {get;set;}
        public string theatre { get; set; }
        public string organization {get;set;}
        public string conference_role {get;set;}
        public string Q1 {get;set;}
        public string Q2 {get;set;}
        public string diet {get;set;}
        public string emergency {get;set;}
        public string emergency_num {get;set;}
        public string send_info2 {get;set;}
        public string registration_date {get;set;}
        public string twitter { get; set; }
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
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string strSQL = "select email, firstname, lastname, company,phone,address,job,theatre,organization,conference_role,Q1,Q2,diet,emergency,emergency_num,send_info2,registration_dateS,twitter from Cala_Web.Tbl_ATF";
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


    [WebMethod]
    public static string putData(string inputEmail, string inputName, string inputLast, string inputCompany, string inputPhone, string inputJob, string theatre, string organization , string conference_role, string Workshop, string forum, string inputdiet, string inputEmergency, string inputEmergencyNum, string info, string address , string inputTwitter)
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

        //Se envian los datos a la consulta por parametro y no concatenandolos directamente para evitar inyección de código :D           
        string stmt = "INSERT INTO Cala_Web.Tbl_ATF (email, firstname, lastname, company,phone,job,theatre,organization,conference_role,Q1,Q2,diet,emergency,emergency_num,send_info2,registration_date,registration_dateS,address,twitter) VALUES (@email, @firstname, @lastname, @company, @phone, @job, @theatre, @organization, @conference_role, @Workshop, @forum, @inputdiet, @inputEmergency,@inputEmergencyNum,@info,@date,@dates,@address,@inputTwitter)";

        SqlCommand cmd2 = new SqlCommand(stmt, con);

        cmd2.Parameters.Add("@email", SqlDbType.VarChar, 100);
        cmd2.Parameters.Add("@firstname", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@lastname", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@company", SqlDbType.VarChar, 150);
        cmd2.Parameters.Add("@phone", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@job", SqlDbType.VarChar, 150);
        cmd2.Parameters.Add("@theatre", SqlDbType.VarChar, 100);
        cmd2.Parameters.Add("@organization", SqlDbType.VarChar, 150);
        cmd2.Parameters.Add("@conference_role", SqlDbType.VarChar, 155);
        cmd2.Parameters.Add("@Workshop", SqlDbType.VarChar, 20);
        cmd2.Parameters.Add("@forum", SqlDbType.VarChar, 20);
        cmd2.Parameters.Add("@inputdiet", SqlDbType.VarChar, 200);
        cmd2.Parameters.Add("@inputEmergency", SqlDbType.VarChar, 200);
        cmd2.Parameters.Add("@inputEmergencyNum", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@info", SqlDbType.VarChar, 20);
        cmd2.Parameters.Add("@date", SqlDbType.DateTime);
        cmd2.Parameters.Add("@dates", SqlDbType.VarChar, 60);
		cmd2.Parameters.Add("@address", SqlDbType.VarChar, 150);
        cmd2.Parameters.Add("@inputTwitter", SqlDbType.VarChar, 150);


        cmd2.Parameters["@email"].Value = inputEmail;
        cmd2.Parameters["@firstname"].Value = inputName;
        cmd2.Parameters["@lastname"].Value = inputLast;
        cmd2.Parameters["@company"].Value = inputCompany;
        cmd2.Parameters["@phone"].Value = inputPhone;
        cmd2.Parameters["@job"].Value = inputJob;
        cmd2.Parameters["@theatre"].Value = theatre;
        cmd2.Parameters["@organization"].Value = organization;
        cmd2.Parameters["@conference_role"].Value = conference_role;
        cmd2.Parameters["@Workshop"].Value = Workshop;
        cmd2.Parameters["@forum"].Value = forum;
        cmd2.Parameters["@inputdiet"].Value = inputdiet;
        cmd2.Parameters["@inputEmergency"].Value = inputEmergency;
        cmd2.Parameters["@inputEmergencyNum"].Value = inputEmergencyNum;
        cmd2.Parameters["@info"].Value = info;
        cmd2.Parameters["@date"].Value = datt;
		cmd2.Parameters["@dates"].Value = datt;
		cmd2.Parameters["@address"].Value = address;
        cmd2.Parameters["@inputTwitter"].Value = inputTwitter;
        
        try
        {
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            result = "ok";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = "fail";
        }
        finally
        {
            con.Close();
        }
		
		if (result == "ok") {
            string nombrec = inputName + " " + inputLast;
            resultado = sendMails(nombrec, inputCompany, inputEmail);
		}
        return resultado;
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
                    tabla += "<td>" + root.firstname + "</td>";
                    tabla += "<td>" + root.lastname + "</td>";
                    tabla += "<td>" + root.email + "</td>";
                    tabla += "<td>" + root.company + "</td>";
                    tabla += "<td>" + root.phone + "</td>";
					tabla += "<td>" + root.address + "</td>";
                    tabla += "<td>" + root.job + "</td>";
					tabla += "<td>" + root.theatre + "</td>";
                    tabla += "<td>" + root.organization + "</td>";
                    tabla += "<td>" + root.conference_role + "</td>";
                    tabla += "<td>" + root.Q1 + "</td>";
                    tabla += "<td>" + root.Q2 + "</td>";
                    tabla += "<td>" + root.diet + "</td>";
                    tabla += "<td>" + root.emergency + "</td>";
                    tabla += "<td>" + root.emergency_num + "</td>";
                    tabla += "<td>" + root.send_info2 + "</td>";
                    tabla += "<td>" + root.registration_date + "</td>";
                    tabla += "<td>" + root.twitter + "</td>";
                    
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
            string resultado = "fail";
            if (result != "fail")
            {
                var test = result.Split(',');
                string nombre = test[0];
                string rol = test[1];
                string id = test[2];

                if (name == nombre)
                {
                    resultado = rol;
                    var sessionUsuario = HttpContext.Current.Session;
                    sessionUsuario["name"] = name;
                    sessionUsuario["rol"] = rol;
                    sessionUsuario["id"] = id;
                }
                else
                {
                    resultado = "fail";
                }
            }
            return resultado;
        }

        public static string validarIngreso(string name, string pass, string app)
        {
            
            string resultado = "";
            string usuario;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

            string strSQL = "SELECT distinct (usuario + ',' + rol + ',' + cast(Key_s as varchar)) as data from UserData where usuario = @name and password = @pass and application = @app";
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
                if (usuario == "null" || usuario == null) {
                    usuario = "fail";
                }
                con.Close();
                resultado = usuario;
                
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
            if (sessionUsuario["id"] == null)
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

