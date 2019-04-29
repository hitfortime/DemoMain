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
    public partial class furn : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        public furn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form klad = new Kladf();
            klad.Show();
            this.Close();
        }

        private void furn_Load(object sender, EventArgs e)
        {
            string query = "select * from furniture";
            // Адаптер для получения данных в нужном формате
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            // Сет для ГридView 
            DataSet ds = new DataSet();
            // Заполнение адаптера с помощью сета из таблицы 
            sda.Fill(ds, "furniture");
            // Указываем источник данных для отображения 
            dataGridView1.DataSource = ds.Tables["furniture"];

            DataGridViewImageColumn img = new DataGridViewImageColumn();//Создание новой колонки "имг"
            img.Name = "img1";// Присвоение колонке имени для обращения к ней
            img.HeaderText = "Картинка";//Название для колонки имг
            dataGridView1.Columns.Add(img);//добавление созданной колонки к датагриду

            for (int i= 0; i < dataGridView1.RowCount; i++)// создание цикла для перебора каждой строки
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)//пока в строке =i и ячейке =1 есть данные, выполнять следующий цикл
                {
                    string basePath = "C:/Users/User2/source/repos/images/";//создание переменной, в которой хранится путь к папке с картинками
                    string filename = dataGridView1.Rows[i].Cells[1].Value.ToString() + ".jpeg";//присвоение переменной значение первой ячейки каждой строки поочередно + расширение картинки
                    string fullPath = basePath + filename;//присвоение переменной полного путя и наименования картинки
                    Image image;//создание переменной,для хранения изображения
                    if (File.Exists(fullPath))//если существует файл по данному пути, то
                    {
                        image = Image.FromFile(fullPath);// присвоить изображение по данному пути этой переменной

                    }
                    else
                    {
                        image = Image.FromFile("C:/Users/User2/source/repos/images/empt.jpg");//иначе использовать пустое изображение (если не существует файла по пути filename)

                    }
                    dataGridView1.Rows[i].Cells["img1"].Value = image;//присвоить картинку из переменной имэйдж в ячейку имг1 строки i
                        
                }
            }

        }
    }
}
