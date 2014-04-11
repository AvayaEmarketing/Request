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


public partial class sol_details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
    public static string getInfo()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        var sessionUsuario = HttpContext.Current.Session;
        string usuario2 = sessionUsuario["id"].ToString();
        int usuario = Convert.ToInt32(usuario2);
        string strSQL = "select usuario,rol,nombre,apellido,email_empresa,direccion,celular  from userdata where id = @usuario";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@usuario", SqlDbType.Int);
        cmd.Parameters["@usuario"].Value = usuario;

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
            WriteError(ex.Message, "sol_details.aspx", "getInfo");
        }
        finally
        {
            con.Close();
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
            WriteError(ex.Message, "sol_details.aspx", "getId");
        }
        finally
        {
            con.Close();
        } 
            return id;
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
                WriteError(ex.Message, "sol_details.aspx", "cerrarSession");
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

