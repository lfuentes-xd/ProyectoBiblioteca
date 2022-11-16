using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreria
{
    public partial class menuHistorial : Form
    {
        SqlDB con = new SqlDB();
        private CurrencyManager CM;
        List<Prestamo> prestamos = new List<Prestamo>();

        public menuHistorial()
        {
            InitializeComponent();
            panel2.Visible = true;
            dataGridView1.DataSource = CargarDatos();
            CM = (CurrencyManager)dataGridView1.BindingContext[prestamos];
        }

        private List<Prestamo> CargarDatos(string query = "")
        {
            string sql = (query == "")
                ? "SELECT * FROM Prestamo ORDER BY FechaEntrega DESC"
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
                    Prestamo cl = new Prestamo()
                    {
                        noPrestamo = int.Parse(lector[0].ToString()),
                        noLector = int.Parse(lector[1].ToString()),
                        isbn = long.Parse(lector[2].ToString()),
                        Fecha = lector[3].ToString()
                    };
                    prestamos.Add(cl);
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
            return prestamos;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuLateral menuLateral = new MenuLateral();
            menuLateral.Show();
            this.Close();
        }
        Boolean val = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            val = true; 
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
            val=false;
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void buttonPres_Click(object sender, EventArgs e)
        {
            menuPrestamo menuPrestamo = new menuPrestamo();
            menuPrestamo.Show();
            this.Hide();
        }

        private void buttonDev_Click(object sender, EventArgs e)
        {
            menuDevolver menuDevolver = new menuDevolver();
            menuDevolver.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MenuLateral menuLateral = new MenuLateral();
            menuLateral.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarHistorial();
            
        }

        private void ConsultarHistorial()
        {
            if (textBox1.Text.IsNullOrEmpty() && textBox2.Text.IsNullOrEmpty()) {
                string sql = $"SELECT * FROM Prestamo ORDER BY FechaEntrega DESC";

                prestamos.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }
            else
            {
                if (textBox1.Text.IsNullOrEmpty())
                {
                    string busca = textBox2.Text;

                    string sql = $"SELECT * FROM Prestamo Where" +
                    $"(ISBN LIKE '%{busca}%') ORDER BY FechaEntrega DESC";

                    prestamos.Clear();
                    dataGridView1.DataSource = CargarDatos(sql);
                    CM.Refresh();
                }
                else {
                    string busca = textBox1.Text;

                    string sql = $"SELECT * FROM Prestamo Where" +
                    $"(NoLector LIKE '%{busca}%') ORDER BY FechaEntrega DESC";

                    prestamos.Clear();
                    dataGridView1.DataSource = CargarDatos(sql);
                    CM.Refresh();
                }

                
            } 
            
        }

        private void menuHistorial_Load(object sender, EventArgs e)
        {

        }
    }
}
