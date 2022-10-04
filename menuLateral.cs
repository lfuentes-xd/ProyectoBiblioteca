using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace libreria
{
    public partial class MenuLateral : Form
    {
        public MenuLateral()
        {
            InitializeComponent();
            panel2.Visible = false;
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
    }
}
