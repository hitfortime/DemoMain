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
    public partial class UserOrderForm : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        double total;
        double izdelie_price;
        public UserOrderForm()
        {
            InitializeComponent();
        }

        private void UserOrderForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kotovDataSet3.izdelie' table. You can move, or remove it, as needed.
            this.izdelieTableAdapter.Fill(this.kotovDataSet3.izdelie);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView item = (DataRowView)comboBox1.SelectedItem;

            if (item != null)
            {
                double item_width = Convert.ToDouble(item.Row.ItemArray[3]);
                double item_height = Convert.ToDouble(item.Row.ItemArray[4]);

                MessageBox.Show(item_height + ", " + item_width);
                con.Open();
                SqlCommand com = new SqlCommand("Select tkani.ID, tkani.Ширина, tkani.Длина, tkani.Цена " +
                    "From tkani_izedelie INNER JOIN tkani ON tkani_izedelie.tkani_id = tkani.ID " +
                    "Where tkani_izedelie.izdelie_id=" + comboBox1.SelectedValue, con);
                SqlDataReader reader = com.ExecuteReader();
                int id = 0;
                double width = 0;
                double height = 0;
                double price = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader[0] == DBNull.Value ? 0 : reader[0]);
                    width = Convert.ToDouble(reader[1] == DBNull.Value ? 0 : reader[1]);
                    height = Convert.ToDouble(reader[2] == DBNull.Value ? 0 : reader[2]);
                    price = Convert.ToDouble(reader[3] == DBNull.Value ? 0 : reader[3]);
                }

                MessageBox.Show(price + "," + width + "," + height);
                izdelie_price = (item_width * item_height * price) / (width * height);
                total = izdelie_price * Convert.ToInt32(textBox1.Text);
                label5.Text = total.ToString();

                reader.Close();
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int parsedValue = 0;
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            total = izdelie_price * parsedValue;
            label6.Text = total.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
