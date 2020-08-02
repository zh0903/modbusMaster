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
    public partial class Form4 : Form
    {
        public Int16 slave = 1;
        public Int16 function = 1;
        public Int16 address = 1;
        public bool onoff; 
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                slave = Convert.ToInt16(textBox1.Text);
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        function = 5;
                        break;
                    case 1:
                        function = 15;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "Error");
                        break;
                }
                address = Convert.ToInt16(textBox2.Text);
                address -= 1;
                if(radioButton1.Checked)
                {
                    onoff = true;
                }
                else
                {
                    onoff = false;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error：参数不正确!", "Error");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            comboBox1.SelectedIndex = 0;
            textBox2.Text = "1";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
