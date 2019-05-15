using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class UserForm : Form
    {
        string user = "";
        public UserForm(String u)
        {
            InitializeComponent();
            this.user = u;
        }

        private void конструкторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form constr = new ConstructorForm();
            
            constr.Show();
            this.Close();
        }

        private void заказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form order = new UserOrderForm(user);
            order.Show();
            this.Close();
        }
    }
}
