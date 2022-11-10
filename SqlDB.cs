using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace libreria
{
    public class SqlDB
    {

        public SqlConnection ObtenerConexion()
        {
            string cadena = "Data Source=DESKTOP-OTBPRV3;Initial Catalog=login;Integrated Security=True";
            try
            {
                SqlConnection connectionbd = new SqlConnection(cadena);
                return connectionbd;
            }
            catch (Exception e)
            {
                MessageBox.Show($"error : {e.Message}", "error a la obtenerConexion de la bd");
                return null;
            }
        }






        //string cadena = "Data Source=DESKTOP-OTBPRV3;Initial Catalog=baselibro;Integrated Security=True";

        //public SqlConnection conectarBD = new SqlConnection();
        //private SqlCommandBuilder comandbuild;
        //public DataSet dataS = new DataSet();
        //public SqlDataAdapter dataAdap;
        //public SqlCommand comando;

        //public SqlDB()
        //{
        //    conectarBD.ConnectionString = cadena;
        //}

        //public void abrir()
        //{
        //    try
        //    {
        //        conectarBD.Open();
        //        MessageBox.Show("conexion establecida " +
        //            "\n sir fuentes");
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("error al establecer la conexion" +
        //            "\n error : " + e.Message);
        //    }
        //    finally
        //    {
        //        conectarBD.Close();
        //    }
        //}

    }
}
