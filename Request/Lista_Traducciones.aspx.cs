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

public partial class Lista_Traducciones : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class Datos
        {
            public string solicit_id { get; set; }
            public string estado { get; set; }
            public string S_key_name { get; set; }
            public string nombre { get; set; }
            public string S_original_language { get; set; }
            public string S_translate_language { get; set; }
            public string S_register_date { get; set; }
            public string S_desired_date { get; set; }
            public string T_Fecha_Estimada { get; set; }
            public string S_solicit_priority { get; set; }

        }

        public class Calendar
        {
            public string id { get; set; }
            public string url { get; set; }
            public string clase { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
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
        public static string traerTraductores()
        {
            SqlDataReader datos;
            string result;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
            string strSQL = "select distinct Key_s as id, (nombre + ' ' + apellido) as nombre from UserData  where rol = 'traductor'";
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
                WriteError(ex.Message, "Lista_Traducciones.aspx", "traerTraductores");

            }
            finally
            {
                con.Close();
            }

            return result;
        }

        [WebMethod]
        public static string getEvents(int traductor)
        {
            string result = "";

            if (validaSession() == "fail")
            {
                result = "fail";
            }
            else
            {
                string clase = "";
                string resultado = getDatosCalendarioDetail(traductor);
                var serializer = new JavaScriptSerializer();
                List<Calendar> values = serializer.Deserialize<List<Calendar>>(resultado);

                string jsonarmado = "[";
                if (values != null)
                {
                    foreach (var root in values)
                    {
                        if (root.clase.Equals("Low")) { clase = "event-success"; }
                        if (root.clase.Equals("Medium")) { clase = "event-important"; }
                        if (root.clase.Equals("High")) { clase = "event-warning"; }
                        jsonarmado += "{\"id\": \"" + root.id + "\",\"url\": \"\", \"class\": \"" + clase + "\",\"title\": \"" + root.title + "\",\"start\": \"" + root.start + "\",\"end\": \"" + root.end + "\"},";
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

        //Metodo del Calendario
        [WebMethod(EnableSession = true)]
        public static string getDatosCalendarioDetail(int traductor)
        {
            
            string result = "";
            SqlDataReader datos;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
            string strSQL = "SELECT solicit_id AS url, S_solicit_priority AS 'clase', CONVERT(VARCHAR(20),solicit_id) AS id, S_key_name AS title, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', S_register_date2) AS bigint)*1000)) AS start, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', T_Fecha_Estimada2) AS bigint)*1000)) AS 'end' from Translate_Solicits where S_visible='YES' and estado not in (4,6,7,8,9,10,13) and T_Fecha_Estimada2 IS NOT NULL and responsable=" + traductor;
            SqlCommand cmd = new SqlCommand(strSQL, con);
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
                WriteError(ex.Message, "Lista_Traducciones.aspx", "getDatosCalendarioDetail");

            }
            finally
            {
                con.Close();
            }
            return result;

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
        public static string getDatosReg(int traductor)
        {
            string result = "";
            if (validaSession() == "fail")
            {
                result = "fail";
            }
            else
            {

                string resultado = getDatosRequest(traductor);
                var serializer = new JavaScriptSerializer();
                List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

                string jsonarmado = "[";
                foreach (var root in values)
                {
                   
                        jsonarmado += "{\"S_key_name\": \"" + root.S_key_name + "\", \"nombre\": \"" + root.nombre + "\",\"S_original_language\": \"" + root.S_original_language + "\",\"S_translate_language\": \"" + root.S_translate_language + "\",\"S_register_date\": \"" + root.S_register_date + "\",\"S_desired_date\": \"" + root.S_desired_date + "\",\"T_Fecha_Estimada\": \"" + root.T_Fecha_Estimada + "\",\"S_solicit_priority\": \"" + root.S_solicit_priority + "\"},";
                    
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
        public static string getDatosRequest(int traductor)
        {
            string result;
            SqlDataReader datos;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
            string strSQL = "select sol.solicit_id, sol.estado, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date,sol.T_Fecha_Estimada, sol.S_solicit_priority from Translate_Solicits sol, Translate_State sta  where sol.responsable = @responsable and sol.estado = sta.id and S_visible = 'YES' and estado not in (4,6,7,8,9,10,13) and T_Fecha_Estimada2 IS NOT NULL order by S_register_date2";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.Add("@responsable", SqlDbType.Int);
            cmd.Parameters["@responsable"].Value = traductor;

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
                WriteError(ex.Message, "Lista_Traducciones.aspx", "getDatosRequest");
            }
            finally
            {
                con.Close();
            }
            return result;

        }


    }
