using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace MASTER
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


        private void Form3_Load(object sender, EventArgs e)
        {

            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("串口未打开", "Error");
                return;
            }

            //添加串口项目  
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);//口个数
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;//第二个
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.DataBits = 8;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                switch (comboBox4.Text)
                {
                    case "1":
                        serialPort1.StopBits = StopBits.One;
                        break;
                    case "2":
                        serialPort1.StopBits = StopBits.Two;
                        break;
                    default:
                        MessageBox.Show("参数错误", "Error");
                        break;
                }

                switch (comboBox5.Text)
                {
                    case "无":
                        serialPort1.Parity = Parity.None;
                        break;
                    case "奇校验":
                        serialPort1.Parity = Parity.Odd;
                        break;
                    case "偶校验":
                        serialPort1.Parity = Parity.Even;
                        break;
                    default:
                        MessageBox.Show("参数错误!", "Error");
                        break;
                } 

                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("串口打开错误！");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
