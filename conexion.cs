using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace sqlserver
{
    public  class conexion
    {

        string cadena  = "Data Source=LAPTOP-KLM21BFT;Initial Catalog=biblioteca;Integrated Security=True";

        public SqlConnection conectarBD = new SqlConnection();
        private SqlCommandBuilder comandbuild; 
        public DataSet dataS = new DataSet();
        public SqlDataAdapter dataAdap;
        public SqlCommand comando; 

        public conexion()
        {
            conectarBD.ConnectionString = cadena; 
        }

        public void abrir()
        {
            try
            {
                conectarBD.Open();
                MessageBox.Show("conexion establecida " +
                    "\n sir fuentes"); 
            }
            catch (Exception e)
            {
                MessageBox.Show("error al establecer la conexion" +
                    "\n error : "+e.Message);
            }
            finally
            {
                conectarBD.Close(); 
            }
        }
    }
}
