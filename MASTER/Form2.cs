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
    public partial class Form2 : Form
    {
        public Int16 slave = 1;
        public Int16 function = 1;
        public Int16 address = 1;
        public Int16 length = 10;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                slave = Convert.ToInt16(textBox1.Text);
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        function = 1;
                        break;
                    case 1:
                        function = 3;
                        break;
                    case 2:
                        function = 4;
                        break;
                    default:
                        MessageBox.Show("参数错误");
                        break;
                }
                slave = Convert.ToInt16(textBox1.Text);
                address = Convert.ToInt16(textBox2.Text);
                address -= 1;                        //地址减一
                length = Convert.ToInt16(textBox3.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("参数错误");
            }
        }
        protected override void OnClosing(CancelEventArgs e)//
        {
            base.OnClosing(e);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
