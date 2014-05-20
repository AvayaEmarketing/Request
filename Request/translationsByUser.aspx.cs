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


public partial class translationsByUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        public string solicitante { get; set; }
        public string solicitudes {get;set;}
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
    public static string getDatosGraph(int mes,int anio)
    {
        
        string jsonarmado = "";
        if (validaSession() == "fail")
        {
            jsonarmado = "fail";
        }
        else
        {
            
            string resultado = getDatosRequest(mes,anio);
            var serializer = new JavaScriptSerializer();
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);
            string valores = "\"valores\":[";
            string nombres = "\"nombres\": [";
            string tooltips = "\"tooltips\": [";
            jsonarmado = "{";
            if (values != null)
            {
                foreach (var root in values)
                {
                    nombres += "\""+ root.solicitante +"\",";
                    tooltips += "\"" + root.solicitudes + "\",";
                    valores += "["+ root.solicitudes +"],";
                }
            }

            nombres = nombres.Substring(0, nombres.Length - 1);
            valores = valores.Substring(0, valores.Length - 1);
            tooltips = tooltips.Substring(0, tooltips.Length - 1);

            nombres = (nombres == "") ? "[]" : nombres + "]";
            valores = (valores == "") ? "[]" : valores + "]";
            tooltips = (nombres == "") ? "[]" : tooltips + "]";
            
            jsonarmado = jsonarmado + valores + "," + nombres + "," + tooltips + "}";
            
        }
        return jsonarmado;
    }

   

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest(int mes,int anio)
    {
        string result;
        string strSQL;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        if (mes == 0)
        {
            strSQL = "select solicitante,COUNT(solicit_id) as solicitudes from (select distinct ud.nombre + ' ' + ud.apellido as solicitante,solicit_id from t_requests as tr, UserData as ud where tr.solicitante_id = ud.Key_s and year(S_register_date2) = @year) as datos group by solicitante";
        }
        else {
            strSQL = "select solicitante,COUNT(solicit_id) as solicitudes from (select distinct ud.nombre + ' ' + ud.apellido as solicitante,solicit_id from t_requests as tr, UserData as ud where tr.solicitante_id = ud.Key_s and month(S_register_date2)= @month and year(S_register_date2) = @year) as datos group by solicitante";
        }
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@month", SqlDbType.Int);
        cmd.Parameters["@month"].Value = mes;
        cmd.Parameters.Add("@year", SqlDbType.Int);
        cmd.Parameters["@year"].Value = anio;

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
            string Datos = sessionUsuario["name"].ToString() + "," + sessionUsuario["foto"].ToString();
            result = (sessionUsuario["rol"].ToString() == "administrador") ? Datos : "fail";
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

