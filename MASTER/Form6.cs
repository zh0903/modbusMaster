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
    public partial class Form6 : Form
    {
        public Int16 slave = 1;
        public Int16 address = 1;
        public Int16 value = 10;


        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                value = Convert.ToInt16(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("请输入整型变量！");
            }
            try
            {
                slave = Convert.ToInt16(textBox1.Text);
                address = Convert.ToInt16(textBox2.Text);
                address -= 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error：参数不正确!", "Error");
            }


        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "10";
        }
    }
}
