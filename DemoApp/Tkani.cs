using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class Tkani : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);

        public Tkani()
        {
            InitializeComponent();
        }

        private void Tkani_Load(object sender, EventArgs e)
        {
            string query = "Select * from tkani";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            sda.Fill(ds, "tkani");
            dataGridView1.DataSource = ds.Tables["tkani"];

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Name = "img222";
            img.HeaderText = "Картинка";
            dataGridView1.Columns.Add(img);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {

                    string basePath = "C:/Users/User2/source/repos/images/";
                    string filename = dataGridView1.Rows[i].Cells[1].Value.ToString() + ".jpg";
                    string fullPath = basePath + filename;
                    Image image;
                    if (File.Exists(fullPath))
                    {
                        image = Image.FromFile(fullPath);
                    }
                    else
                    {
                        image = Image.FromFile(basePath + "empt.jpg");
                    }
                    dataGridView1.Rows[i].Cells["img222"].Value = image;
                }
                
                  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form klad = new Kladf();
            klad.Show();
            this.Close();
            
        }
    }
}
