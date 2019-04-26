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
    public partial class Reg : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        Form auth;
        public Reg()
        {
            InitializeComponent();
        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int errors = 0;
            string message = "";
            if (textBox1.Text =="")
            {
                errors++;
                message += "Пожалуйста введите имя\n";
            }
            if (textBox2.Text == "")
            {
                errors++;
                message += "Пожалуйста введите логин\n";
            }
            if (textBox3.Text == "")
            {
                errors++;
                message += "Пожалуйста введите пароль\n";
            }
            if (textBox4.Text == "")
            {
                errors++;
                message += "Пожалуйста подтвердите пароль\n";
            }
            if (textBox3.Text != textBox4.Text)
            {
                errors++;
                message += "Пароли не совпадают";
            }
            if(errors>0)
            {
                MessageBox.Show(message);
            } else {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO users(login,password,role,name) VALUES (@login,@password,@role,@name)",connection);
                    command.Parameters.AddWithValue("@login", textBox2.Text);
                    command.Parameters.AddWithValue("@password", textBox3.Text);
                    command.Parameters.AddWithValue("@role", "zakaz");
                    command.Parameters.AddWithValue("@name", textBox1.Text);
                    int regged = Convert.ToInt32(command.ExecuteNonQuery());
                    connection.Close(); 
                    MessageBox.Show("Пользователь успешно зарегистрирован");
                }
                catch
                {
                    MessageBox.Show("Логин уже занят");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            auth = new Auth();
            auth.Show();

        }
    }
}
