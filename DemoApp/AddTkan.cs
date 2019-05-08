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
    public partial class AddTkan : Form
        
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        string filename;
        public AddTkan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPEG images |*.jpg";
            openFileDialog1.InitialDirectory = "C:\\Users\\User2\\Downloads";
            openFileDialog1.Title = "Выбрать картинку";
            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                MessageBox.Show(filename);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            Random rand = new Random();
            string art = "User" + rand.Next(10000);
            SqlCommand com = new SqlCommand("insert into tkani (Артикул, Название, Рисунок) Values ('"+art+"','"+textBox1.Text+"','"+filename+"')",con);
            com.ExecuteScalar();
            MessageBox.Show("Ткань успешно добавлена");
            con.Close();
        }
    }
}
