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
    public partial class DersEkle : Form
    {
        SqlConnection connection = new SqlConnection(settings.ConnectServer);
        SqlCommand command;
        SqlDataReader reader;
        public DersEkle()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void DersEkle_Load(object sender, EventArgs e)
        {
            connection.Open();
            string Sql = "select name, surname from users where rolID='2' order by name ASC";
            command = new SqlCommand(Sql, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0]+" "+reader[1]);

            }
            Sql = "select depNam from departman";
            command = new SqlCommand(Sql, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0]);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Panel admin_Paneli = new Admin_Panel();
            admin_Paneli.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hocaisim_soyisim = comboBox1.SelectedItem.ToString();
            string departman_isim = comboBox2.SelectedItem.ToString();
            string depid = "default", hocaid = "default";

            string[] hocaisim = hocaisim_soyisim.Split(' ');
            MessageBox.Show(hocaisim[0]+hocaisim[1]);
            command = new SqlCommand("select PersonID from users where name='" + hocaisim[0] + "' and surname='"+hocaisim[1]+"'", connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                hocaid = reader["PersonID"].ToString();
            }
            command = new SqlCommand("select depID from departman where depNam='" + departman_isim + "'", connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                depid = reader["depID"].ToString();
            }
            MessageBox.Show("depid: " + depid + "   hocaid: " + hocaid);
            command = new SqlCommand("INSERT INTO dersler(depID,dersKOD,dersAD,dersAKTS,dersHocaID) VALUES(@param1,@param2,@param3,@param4,@param5)", connection);
            command.Parameters.AddWithValue("@param1", depid);
            command.Parameters.AddWithValue("@param2", textBox1.Text);
            command.Parameters.AddWithValue("@param3", textBox3.Text);
            command.Parameters.AddWithValue("@param4", textBox2.Text);
            command.Parameters.AddWithValue("@param5", hocaid);
            command.ExecuteNonQuery();
            
        }
    }
}
