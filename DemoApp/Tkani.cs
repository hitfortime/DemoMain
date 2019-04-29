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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form klad = new Kladf();
            klad.Show();
            this.Close();
            
        }
    }
}
