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

        private class Order
        {
            public int id { get; set; }
            public int count { get; set; }
            public double price { get; set; }

            public Order (int i, int c, double p)
            {
                this.id = i;
                this.count = c;
                this.price = p;
            }
        }

        String user = "";
        SqlConnection con = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        double total=0;
        double izdelie_price=0;
        List<Order> cart = new List<Order>();
     

        public UserOrderForm(String u)
        {
            InitializeComponent();
            this.user = u;
        }

        private void UserOrderForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kotovDataSet3.izdelie' table. You can move, or remove it, as needed.
            this.izdelieTableAdapter.Fill(this.kotovDataSet3.izdelie);

            System.Object[] items2 = new System.Object[101];
            for (int i = 0; i < 101; i++)
            {
                items2[i] = i;
            }
            comboBox2.Items.AddRange(items2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView item = (DataRowView)comboBox1.SelectedItem;

            if (item != null)
            {
                double item_width = Convert.ToDouble(item.Row.ItemArray[3]);
                double item_height = Convert.ToDouble(item.Row.ItemArray[4]);

                
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

                
                izdelie_price = (item_width * item_height * price) / (width * height);
                total = Math.Round(Math.Abs(izdelie_price * Convert.ToInt32(comboBox2.SelectedIndex)));
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
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            total = Math.Round(Math.Abs(izdelie_price * Convert.ToInt32(comboBox2.SelectedIndex)));
            label5.Text = total.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                Random random = new Random();
                SqlCommand command = new SqlCommand("INSERT INTO [order] ([date], stage, client, manager, price) " +
                    "VALUES (getdate(),@stage,@client,@manager,@price); SELECT SCOPE_IDENTITY(); ", con);
                command.Parameters.AddWithValue("@stage", "Новый");
                command.Parameters.AddWithValue("@client", this.user);
                command.Parameters.AddWithValue("@manager", "manager"); // from users table
                command.Parameters.AddWithValue("@price", total); // from users table

                int order = Convert.ToInt32(command.ExecuteScalar());

                SqlCommand command1 = new SqlCommand("INSERT INTO order_izdelie (order_id, izdelie_id,counter) " +
                   "VALUES (" + order + "," + comboBox1.SelectedValue + ", 1);", con);
                command1.ExecuteScalar();

                MessageBox.Show("Ваш заказ N" + order + "  забронирован!");

                con.Close();

            }
            catch
            {
                MessageBox.Show("Ошибка !\n");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cart.Add(new Order(Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedIndex), total));

            StringBuilder s = new StringBuilder();
            foreach (Order cart_item in cart)
            {


                s.Append("Изделие:" + cart_item.id + ", Кол-во: " + cart_item.count + ", цена:" + cart_item.price);
                s.Append("\n");


            }
            label7.Text = s.ToString();
        }
    }
}
