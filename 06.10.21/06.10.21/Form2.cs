using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Eramake;
namespace _06._10._21
{
    public partial class Form2 : Form
    {
        
        private int rand_num;
        SqlConnection connect = new SqlConnection(settings.ConnectServer);
        SqlDataReader reader,readerr;
        SqlCommand command;
        private string ID;
        private string sifre;
        public Form2()
        {
            InitializeComponent();
            connect.Open();
            this.CenterToScreen();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            command = new SqlCommand("select sifre from users where eposta='"+textBox1.Text+"' AND sifre IS NULL", connect);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                label2.Visible = true;
                label3.Visible = true;

                textBox2.Visible = true;
                textBox3.Visible = true;
                button1.Visible = false;
                button2.Visible = true;
            }
            command = new SqlCommand("select sifre from users where eposta='" + textBox1.Text + "' AND sifre IS NOT NULL", connect);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                MessageBox.Show("Kayıtlı kullanıcısınız.");
                guna2GradientPanel2.Visible = true;

            }
            else
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            command = new SqlCommand("select sifre from users where eposta='" + textBox6.Text + "' AND sifre='"+ Eramake.eCryptography.Encrypt(textBox5.Text)+"'", connect);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                command = new SqlCommand("select rolID from users where eposta='"+textBox6.Text+"'", connect);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    command = new SqlCommand("select isim from roller where id='"+reader["rolID"].ToString()+"'", connect);
                    readerr = command.ExecuteReader();
                    if(readerr.Read())
                    {
                        if (readerr["isim"].ToString() == "Admin")
                        {
                            MessageBox.Show("Hoşgeldin admin!");
                            Admin_Panel admin_Paneli = new Admin_Panel();
                            admin_Paneli.Show();
                            this.Visible = false;

                        }
                        else if(readerr["isim"].ToString() == "Öğretmen")
                        {
                            MessageBox.Show("Öğretmen");
                        }
                        else
                        {
                            MessageBox.Show("Öğrenci"); // Eğer ki öğretmen ve adminden başka yanlış girilirse öğrenciye yönlendirir o yüzden eklersen dğeiştirmeyi unutma
                        }
                    }
                    
                }

                    
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }
        public static void Email(string usermail, string code)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("testingsmtpyigit@gmail.com");
                message.To.Add(new MailAddress(usermail));
                message.Subject = "8-Digit Number.";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Merhaba 8 haneli kodunuz; "+code+"";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("testingsmtpyigit@gmail.com", "A123456789++");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Visible = true;
            button4.Visible = true;
            command = new SqlCommand("SELECT PersonID FROM users Where eposta='"+textBox1.Text+"'", connect);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                ID= Convert.ToString(reader.GetInt32(0));
            }
            Random rd = new Random();

            rand_num = rd.Next(1000000, 99999999);
            Email(textBox1.Text, Convert.ToString(rand_num));
            
        }

        void Finish(int rand_num)
        {
            if (textBox4.Text == Convert.ToString(rand_num))
            {
                if (textBox2.Text == textBox3.Text)
                {
                    sifre = Eramake.eCryptography.Encrypt(textBox2.Text);


                    SqlCommand komut;
                    string kayit = "UPDATE users SET sifre= '" + sifre + "' WHERE PersonID='" + ID + "';";
                    komut = new SqlCommand(kayit, connect);
                    komut.ExecuteNonQuery();
                    guna2GradientPanel2.Visible = true;
                    button4.Visible = false;
                    button2.Visible = false;
                    textBox4.Visible = false;
                    MessageBox.Show("Kayıt başarılı");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Finish(rand_num);

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
