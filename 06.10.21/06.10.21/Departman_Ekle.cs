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

namespace _06._10._21
{
    public partial class Departman_Ekle : Form
    {
        
        SqlConnection connect = new SqlConnection(settings.ConnectServer);
        SqlDataReader reader, readerr;
        SqlCommand command;
        public Departman_Ekle()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Panel admin_Paneli = new Admin_Panel();
            admin_Paneli.Show();
            this.Visible = false;
        }

        private void Departman_Ekle_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect.Open();
            command = new SqlCommand("INSERT INTO departman(depID,depName) VALUES(@param1,@param2)", connect);
            command.Parameters.AddWithValue("@param1", textBox1.Text);
            command.Parameters.AddWithValue("@param2", textBox2.Text);
            command.ExecuteNonQuery();
            Admin_Panel admin = new Admin_Panel();
            admin.Visible = true;
            this.Visible = false;
        }
    }
}
