using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace libreria
{
    public partial class menuDevolver : Form
    {

        SqlDB con = new SqlDB();
        private CurrencyManager CM;
        List<Prestamo> prestamos = new List<Prestamo>();

        public menuDevolver()
        {

            InitializeComponent();
            panel2.Visible = true;
            dataGridView1.DataSource = CargarDatos();
            CM = (CurrencyManager)dataGridView1.BindingContext[prestamos];
        }

        private List<Prestamo> CargarDatos(string query = "")
        {
            string sql = (query == "")
                ? "SELECT * FROM Prestamo WHERE FechaEntrega is null ORDER BY FechaEntrega DESC"
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

        private void button1_Click(object sender, EventArgs e)
        {
            MenuLateral menuLateral = new MenuLateral();
            menuLateral.Show();
            this.Close();
        }
        Boolean val=false;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            val = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (val == true)
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

        private void buttonHis_Click(object sender, EventArgs e)
        {
            menuHistorial menuhistorial = new menuHistorial();
            menuhistorial.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {
            ConsultarHistorial();
        }

        private void ConsultarHistorial()
        {
            if (textBox1.Text.IsNullOrEmpty() && textBox2.Text.IsNullOrEmpty())
            {
                string sql = $"SELECT * FROM Prestamo WHERE FechaEntrega is null ORDER BY FechaEntrega;";

                prestamos.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }
            else
            {
                if (textBox1.Text.IsNullOrEmpty())
                {
                    string busca = textBox2.Text;

                    string sql = $"SELECT p.NoPrestamo, p.NoLector, p.ISBN, p.FechaEntrega FROM Prestamo p, Libros li WHERE" +
                    $"(p.ISBN=li.ISBN) AND (Titulo LIKE '%{busca}%') AND (FechaEntrega is null) ORDER BY FechaEntrega DESC";

                    prestamos.Clear();
                    dataGridView1.DataSource = CargarDatos(sql);
                    CM.Refresh();
                }
                else
                {
                    string busca = textBox1.Text;

                    string sql = $"SELECT p.NoPrestamo, p.NoLector, p.ISBN, p.FechaEntrega FROM Prestamo p, Libros li WHERE" +
                    $"(p.ISBN=li.ISBN) AND (p.ISBN LIKE '%{busca}%') AND (FechaEntrega is null) ORDER BY FechaEntrega DESC";

                    prestamos.Clear();
                    dataGridView1.DataSource = CargarDatos(sql);
                    CM.Refresh();
                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Insertar(textBox1.Text, textBox3.Text, textBox4.Text);
        }

        private void Insertar(string ISBN, string NoPrestamo, string NoLector)
        {
            SqlConnection connection = con.ObtenerConexion();
            connection.Open();
            DateTime dateAndTime = DateTime.Now;
            String fecha = dateAndTime.ToString("yyyy/MM/dd");
            String sql = "UPDATE Prestamo SET FechaEntrega = '"+fecha+"' WHERE NoPrestamo = "+NoPrestamo+" AND NoLector = "+NoLector+" AND ISBN = "+ISBN;
            try
            {
                SqlCommand sqlcom = new SqlCommand(sql, connection);
                sqlcom.ExecuteNonQuery();
                MessageBox.Show("Libro devuelto exitosamente");
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
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow fila = dataGridView1.Rows[index];
            textBox1.Text = fila.Cells[2].Value.ToString();
            textBox3.Text = fila.Cells[0].Value.ToString();
            textBox4.Text = fila.Cells[1].Value.ToString();
            String sql = "SELECT Titulo FROM Libros WHERE ISBN =" + textBox1.Text;
            textBox2.Text = Consulta(sql);


        }

        private String Consulta(String sql)
        {
            SqlConnection connection = con.ObtenerConexion();
            connection.Open();
            String resultado="";
            try
            {
                SqlCommand sqlcom = new SqlCommand(sql, connection);
                //para leer la tabla 
                SqlDataReader lector = sqlcom.ExecuteReader();

                while (lector.Read())
                {
                    resultado = lector[0].ToString();
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
            return resultado;
        }

        

        private void label1_Click(object sender, EventArgs e)
        {
            
            
        }

        
    }
}
