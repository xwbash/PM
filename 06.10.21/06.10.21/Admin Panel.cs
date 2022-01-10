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
    public partial class Admin_Panel : Form
    {
        SqlConnection connection = new SqlConnection(settings.ConnectServer);
        SqlCommandBuilder commandBuilder;
        SqlDataAdapter adtr;
        DataTable dt;
        DataSet ds;
        DataTable tbl = new DataTable();
        public Admin_Panel()
        {
            InitializeComponent();
            this.CenterToParent();
            connection.Open();


        }

        private void Admin_Panel_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rol_Ekle rol = new Rol_Ekle();
            rol.Visible = true;
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Departman_Ekle dep = new Departman_Ekle();
            dep.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Ekle user = new User_Ekle();
            user.Visible = true;
            this.Visible = false;
        }
       
        private void button6_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            dt = new DataTable();
            adtr = new SqlDataAdapter("SELECT * FROM users", connection);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            commandBuilder = new SqlCommandBuilder(adtr);
            adtr.Update(dt);
            MessageBox.Show("Information Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            dt = new DataTable();
            adtr = new SqlDataAdapter("SELECT * FROM roller", connection);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            dt = new DataTable();
            adtr = new SqlDataAdapter("SELECT * FROM departman", connection);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DersEkle ders = new DersEkle();
            ders.Show();
            this.Visible = false;
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
