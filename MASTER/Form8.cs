using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MASTER
{
    public partial class Form8 : Form
    {
        public delegate void TransfDelegate(String value);
        public Form8()
        {
            InitializeComponent();
        }

        public event TransfDelegate TransfEvent;
        private void button2_Click(object sender, EventArgs e)
        {
            TransfEvent("0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
       
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TransfEvent("1");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
