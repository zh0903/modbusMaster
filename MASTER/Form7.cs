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
    public partial class Form7 : Form
    {
        public Int16 slave = 1;
        public Int16 address = 1;
        public Int16 length = 10;
        public string s;

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "10";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                slave = Convert.ToInt16(textBox1.Text);
                address = Convert.ToInt16(textBox2.Text);
                address -= 1;
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
                    MessageBox.Show("预置内容与长度不符");
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
                MessageBox.Show("操作错误");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

