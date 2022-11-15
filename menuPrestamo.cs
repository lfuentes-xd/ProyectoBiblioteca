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
using System.Data;

namespace libreria
{
    public partial class menuPrestamo : Form
    {
        SqlDB con = new SqlDB();
        private CurrencyManager CM;
        List<Prestamo> libros = new List<Prestamo>();
       //List<Claselibro> libro = new List<Claselibro>();
        public menuPrestamo()
        {
            InitializeComponent();
            panel2.Visible = true;
            dataGridView1.DataSource = CargarDatos();
            //data2.DataSource = CargarDatos2(); 
            CM = (CurrencyManager)dataGridView1.BindingContext[libros];
        }

        //para llenar la tabla de prestamos 
        private List<Prestamo> CargarDatos(string query = "")
        {
            string sql = (query == "")
                ? "SELECT * FROM Prestamo"
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
                        isbn = Int64.Parse(lector[2].ToString()),
                        Fecha = lector[3].ToString()
                    };
                    libros.Add(cl);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                "Error inesperado: " + e.Message,
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
            MenuLateral menuLateral = new MenuLateral();
            this.Close();
            menuLateral.Show();

        }

        private void menuPrestamo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MenuLateral MenuLateral = new MenuLateral();
            MenuLateral.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Boolean val = false;
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
            val = false;
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

        private void buttonPres_Click(object sender, EventArgs e)
        {

        }

        private void buttonDev_Click(object sender, EventArgs e)
        {
            menuDevolver menuDevolver = new menuDevolver();
            menuDevolver.Show();
            this.Hide();
        }

        private void buttonHis_Click(object sender, EventArgs e)
        {
            menuHistorial menuhistorial = new menuHistorial();
            menuhistorial.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MenuLateral menuLateral = new MenuLateral();
            menuLateral.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                string sql = $"SELECT * FROM Prestamo ";

                libros.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }
            else
            {
                string busca = textBox1.Text;

                string sql = $"SELECT * FROM Prestamo Where" +
                $"(ISBN LIKE '%{busca}%')";

                libros.Clear();
                dataGridView1.DataSource = CargarDatos(sql);
                CM.Refresh();
            }
        }
        //extraer
        private void extraer_Click(object sender, EventArgs e)
        {
            Prestamo pres = libros[CM.Position];
            textBox4.Text = pres.noPrestamo.ToString();
            textBox1.Text = pres.isbn.ToString();
            textBox2.Text = pres.noLector.ToString();
            textBox3.Text = pres.Fecha.ToString();
        }
        //limpiar
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        //boton para insertar
        private void insertar_Click(object sender, EventArgs e)
        {
            int noPrestamo = int.Parse(textBox4.Text);
            int noLector = int.Parse(textBox2.Text);
            Int64 ISBN = Int64.Parse(textBox1.Text);
            string fech = textBox3.Text;
            DateTime dateAndTime = DateTime.Now;
            String fecha = dateAndTime.ToString("yyyy/MM/dd");


            string sql = $"INSERT INTO Prestamo (NoPrestamo,NoLector,ISBN,FechaEntrega) "
                +$"VALUES ('{noPrestamo}','{noLector}','{ISBN}',{dateAndTime})";

            SqlConnection connection = con.ObtenerConexion();
            connection.Open();

            try
            {
                SqlCommand sqlcom = new SqlCommand(sql, connection);
                sqlcom.ExecuteNonQuery();

                MessageBox.Show("Dato Registrado con exito",
                    "exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information); 

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error en el registro : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            finally {
                connection.Close();
                limpiar(); 
                libros.Clear();
                dataGridView1.DataSource = CargarDatos();
                CM.Refresh(); 
            }
        }
        //boton poara actualizar
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = con.ObtenerConexion();
            connection.Open();
            try
            {
                DateTime dateAndTime = DateTime.Now;
                
                String fecha = dateAndTime.ToString("yyyy/MM/dd");
                String sql = "UPDATE Prestamo SET FechaEntrega = '" + fecha + "' WHERE NoPrestamo = " + textBox4.Text + " AND NoLector = " + textBox2.Text + " AND ISBN = " + textBox1.Text;

                SqlCommand sqlcom = new SqlCommand(sql, connection);
                sqlcom.ExecuteNonQuery(); 

                MessageBox.Show(
                    "dato modificado con exito",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                  );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Error en el registro: " + ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
            }
            finally
            {
                connection.Close();
                libros.Clear();
                dataGridView1.DataSource = CargarDatos();
                CM.Refresh();

            }
        }
        //boton para eliminar
        private void button13_Click(object sender, EventArgs e)
        {

        }

        //buscar ISBN
        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                MessageBox.Show("neceistar meter un ISBN"); 
            }
            else
            {
                
                string busca = textBox1.Text;
                string sql = $"SELECT * FROM Libros Where" +
                $"(ISBN = '{busca}')";

                SqlConnection connection = con.ObtenerConexion();
                connection.Open();

                try
                {
                    //para ejecutar consultas
                    SqlCommand sqlcom = new SqlCommand(sql, connection);
                    //para leer la tabla 
                    SqlDataReader lector = sqlcom.ExecuteReader();

                    if (lector.Read())
                    {
                        MessageBox.Show("se encontro el libro buscado");
                        textBox2.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("sucedio un error en la busqueda: "+ex); 
                }
                finally
                {
                    connection.Close(); 
                }
                
            }

        }

        

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == " ")
            {
                MessageBox.Show("neceistar meter un ISBN");
            }
            else
            {

                string busca = textBox2.Text;
                string sql = $"SELECT * FROM Lector Where" +
                $"(NoLector = '{busca}')";

                SqlConnection connection = con.ObtenerConexion();
                connection.Open();

                try
                {
                    //para ejecutar consultas
                    SqlCommand sqlcom = new SqlCommand(sql, connection);
                    //para leer la tabla 
                    SqlDataReader lector = sqlcom.ExecuteReader();

                    if (lector.Read())
                    {
                        MessageBox.Show("se encontro el usuario buscado");
                        insertar.Enabled = true;
                        Random r = new Random();
                        int nolec = r.Next(0,4);
                        DateTime dateAndTime = DateTime.Now;
                        String fecha = dateAndTime.ToString("yyyy/MM/dd");

                        textBox4.Text = nolec.ToString();
                        textBox3.Text = fecha; 

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("sucedio un error en la busqueda: " + ex);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
    }
}
