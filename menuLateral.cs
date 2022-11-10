using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;

namespace libreria
{
    public partial class MenuLateral : Form
    {
        SqlDB con = new SqlDB(); 
        private CurrencyManager CM; 
        List<Claselibro> libros = new List<Claselibro>();


        public MenuLateral()
        {
            InitializeComponent();
            panel2.Visible = false;
            dataGridView1.DataSource = CargarDatos();
            CM = (CurrencyManager)dataGridView1.BindingContext[libros]; 
        }
        //para llenar la tabla de los datos 
        private List<Claselibro> CargarDatos(string query = "")
        {
            string sql = (query == "")
                ? "SELECT * FROM libros"
                : query;
            SqlConnection connection = con.ObtenerConexion(); 
            connection.Open();
            try
            {
                //para ejecutar consultas
                SqlCommand sqlcom = new SqlCommand(sql, connection);
                //para leer la tabla 
                SqlDataReader lector = sqlcom.ExecuteReader();

                while (lector.Read())
                {
                    Claselibro cl = new Claselibro()
                    {

                        isbn = Int64.Parse(lector[0].ToString()),
                        Titulo = lector[1].ToString(),
                        autor = lector[2].ToString(),
                        genero = lector[3].ToString(), 
                        noeditorial = int.Parse(lector[4].ToString())
                    };
                    libros.Add(cl);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                "Error inesperado" + e.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return libros; 
               
        }


        private void button1_Click(object sender, EventArgs e)
        { 
            
            if (panel2.Visible ==false)
            { 
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        private void x_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            menuPrestamo menuPrestamo = new menuPrestamo();
            menuPrestamo.Show();
            this.Hide();
        }
        Boolean val = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            val= true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(val == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            val = false;
        }

        private void buttonDev_Click(object sender, EventArgs e)
        {
            menuDevolver menuDevolver = new menuDevolver();
            menuDevolver.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonHis_Click(object sender, EventArgs e)
        {
            menuHistorial menuhistorial = new menuHistorial();
            menuhistorial.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(panel2.Visible == false)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        //boton para buscar 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                string sql = $"SELECT * FROM libros ";

                libros.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }
            else
            {
                string busca = textBox1.Text;

                string sql = $"SELECT * FROM libros Where" +
                $"(Titulo LIKE '%{busca}%')";

                libros.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }

            
        }
    }
}
