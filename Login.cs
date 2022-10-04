using sqlserver;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Configuration; 

namespace BibliotecaAPP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        conexion cn = new conexion();
        private CurrencyManager CM;



        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public static extern void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text= "";
                txtUsuario.ForeColor = Color.LightGray; 
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.DimGray; 
            }
        }

        private void txtcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "")
            {
                txtcontraseña.Text = "Contraseña";
                txtcontraseña.ForeColor = Color.DimGray;
                txtcontraseña.UseSystemPasswordChar = false;
            }
        }

        private void txtcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "Contraseña")
            {
                txtcontraseña.Text = " ";
                txtcontraseña.ForeColor = Color.LightGray;
                txtcontraseña.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112,0xf012,0); 
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btn_acceder_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text!="Usuario")
            {
                if (txtcontraseña.Text!="Contraseña")
                { 
                    
                    try
                    {
                        
                         SqlConnection con = cn.conectarBD;
                         con.Open();
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT loginName, password from usuarios where loginName='"+txtUsuario.Text+"'AND password='" + txtcontraseña.Text + "'", con))
                            {
                                SqlDataReader dr = cmd.ExecuteReader();

                                if (dr.Read())
                                {
                                    MessageBox.Show("exitoso");
                                    libreria.MenuLateral mn = new libreria.MenuLateral();
                                    mn.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("datos incorrectos"); 
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                        "Error en el registro" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    }
                }
                else
                {
                    MessageBox.Show("la contrase�a debe ser diferente a" +
                    "\n Contrase�a");
                }
            }
            else
            {
                MessageBox.Show("El usuario debe ser diferente a" +
                    "\n Usuario"); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            libreria.MenuLateral menuLateral = new libreria.MenuLateral();
            menuLateral.Show();
            this.Hide();
        }
    }
}