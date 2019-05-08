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

namespace DemoApp
{
    public partial class Auth : Form
    {
        Admf adm = new Admf();
        Kladf klad = new Kladf();
        Manf man = new Manf();
        Zakf zak = new Zakf();

        Form reg;
        public Auth()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            reg = new Reg();
            reg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        { SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
            connection.Open();
            string login = textBox1.Text;
            string pass = textBox2.Text;
            SqlCommand command = new SqlCommand("SELECT role FROM users WHERE login ='" + login + "' AND password ='" + pass + "'", connection);
            //SqlDataReader reader = command.ExecuteS();
            String role = command.ExecuteScalar().ToString();
            //while (reader.Read())
            //{
            //role = reader[2].ToString();
            //name = reader[3].ToString();
            //}
            switch (role)
            {
                case "zakaz":
                    Form user = new UserForm();
                    user.Show();
                    this.Hide();
                    break;
                case "admin":
                    adm.Show();
                    this.Hide();
                    break;
                case "klad":
                    klad.Show();
                    this.Hide();
                    break;
                case "manager":
                    man.Show();
                    this.Hide();
                    break;

                default:
                    MessageBox.Show ("Роль не установлена или пользователь не найден");
                    break;
            }
        }
    }
}
