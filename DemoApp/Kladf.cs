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
    public partial class Kladf : Form
    {
        public Kladf()
        {
            InitializeComponent();
        }

        private void тканиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tkan = new Tkani();
            tkan.Show();
            this.Close();
        }

        private void фурнитураToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form furn = new furn();
            furn.Show();
            this.Close();
        }
    }
}
