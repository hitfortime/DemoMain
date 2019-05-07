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
    public partial class ConstructorForm : Form
    {
        
        private class Tkani
        {
            public int ID { get; set; }
            public string Название { get; set; }
            public string Цвет { get; set; }
            public double Цена { get; set; }
            
            public Tkani (int i, string n, string c, double p)
            {
                this.ID = i;
                this.Название = n;
                this.Цвет = c;
                this.Цена = p;
            }
        }
        SqlConnection con = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);

        public ConstructorForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConstructorForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kotovDataSet2.tkani' table. You can move, or remove it, as needed.
            this.tkaniTableAdapter1.Fill(this.kotovDataSet2.tkani);
            // TODO: This line of code loads data into the 'kotovDataSet1.tkani' table. You can move, or remove it, as needed.
            this.tkaniTableAdapter.Fill(this.kotovDataSet1.tkani);
            // TODO: This line of code loads data into the 'kotovDataSet.furniture' table. You can move, or remove it, as needed.
            this.furnitureTableAdapter.Fill(this.kotovDataSet.furniture);
           
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            string value = comboBox2.SelectedValue.ToString();
            int selectedfurn = Convert.ToInt32(comboBox2.SelectedValue);
            int selectedtkan = Convert.ToInt32(comboBox1.SelectedValue);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Insert into izdelie (Наименование, Длина, Ширина) Values (@name,@heigh,@width); Select SCOPE_iDENTITY();", con);
                

                command.Parameters.AddWithValue("@name", textBox1.Text);
                command.Parameters.AddWithValue("@heigh", textBox2.Text);
                command.Parameters.AddWithValue("@width", textBox3.Text);

                int izdelie = Convert.ToInt32(command.ExecuteScalar());

                SqlCommand com = new SqlCommand("Insert into tkani_izedelie (tkani_id, izdelie_id) Values (" + selectedtkan + "," + izdelie + ")",con);
                com.ExecuteScalar();

                con.Close();

             

                MessageBox.Show("Запись добавлена!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                
            }
            catch
            {
                MessageBox.Show("Ошибка !\n");
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRowView item = (DataRowView)comboBox1.SelectedItem;
            MessageBox.Show(item.Row.ItemArray[3].ToString());
            Draw();

        }

        private void Draw()
        {
            if(textBox2.Text.Trim().Length == 0 || textBox3.Text.Trim().Length==0)
            {
            MessageBox.Show("Введите размеры изделия");
            } else
            {
                Graphics g = panel1.CreateGraphics();
                g.Clear(Color.White);
                Pen p = new Pen(Color.Black, 1);
                int w = Convert.ToInt32(textBox2.Text.Trim());
                int h = Convert.ToInt32(textBox3.Text.Trim());
                g.DrawRectangle(p, 10, 10, w, h);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
