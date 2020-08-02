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
    public partial class Form5 : Form
    {
        public Int16 slave = 1;
        public Int16 address = 1;
        public Int16 length = 10;
        public string s;

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                slave = Convert.ToInt16(textBox1.Text);
                address = Convert.ToInt16(textBox2.Text);
                address -= 1;//减一
                length = Convert.ToInt16(textBox3.Text);
                string sendBuf = textBox4.Text;
                string sendnoNull = sendBuf.Trim();
                string[] strArray = sendnoNull.Split(' ');
                int byteBufferLength = strArray.Length;
                for (int i = 0; i < strArray.Length; i++)//去空格重新计数
                {
                    if (strArray[i] == "")
                    {
                        byteBufferLength--;
                    }
                }
                if (byteBufferLength != length)
                {
                    MessageBox.Show("长度与内容不对应");
                }
                else
                {
                    s = sendBuf;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("设置错误");
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "3";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
