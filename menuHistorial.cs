using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreria
{
    public partial class menuHistorial : Form
    {
        public menuHistorial()
        {
            InitializeComponent();
            panel2.Visible = false;
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
    }
}
