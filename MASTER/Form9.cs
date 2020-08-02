using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Timers;

namespace MASTER
{
    public partial class Form9 : Form
    {
        public float []d=new float[6];
        float[] dport = new float[6];
        float[] port1 = new float[6];
        float[] port2 = new float[6];
        float[] port3 = new float[6];
        float[] port4 = new float[6];
        float[] port5 = new float[6];
        float[] port6 = new float[6];
        public System.Timers.Timer t4 = new System.Timers.Timer(1000);
        public Form9()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        private void _refresh(object source, System.Timers.ElapsedEventArgs e)
        {
                for (int i = 0; i < 5; i++)
                {
                    port1[i] = port1[i + 1];
                    port2[i] = port2[i + 1];
                    port3[i] = port3[i + 1];
                    port4[i] = port4[i + 1];
                    port5[i] = port5[i + 1];
                    port6[i] = port6[i + 1];
                }
                port1[5] = d[0];
                port2[5] = d[1];
                port3[5] = d[2];
                port4[5] = d[3];
                port5[5] = d[4];
                port6[5] = d[5];
     
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            t4.Interval = 1000;
            t4.Enabled = true;
            t4.Elapsed += new ElapsedEventHandler(_refresh);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    { for (int i = 0;i <6;i++)
                        {
                            dport[i] = port1[i];
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i <6; i++)
                        {
                            dport[i] = port2[i];
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port3[i];
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port4[i];
                        }
                    }
                    break;
                case 4:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port5[i];
                        }
                    }
                    break;
                case 5:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port6[i];
                        }
                    }
                    break;

            }
            chart1.Series[0].ChartType = SeriesChartType.Column;          
            chart1.Series[0].Color = Color.Blue;
            // 添加数据
            chart1.Series[0].Points.AddXY(1, dport[0]);
            chart1.Series[0].Points.AddXY(2, dport[1]);
            chart1.Series[0].Points.AddXY(3, dport[2]);
            chart1.Series[0].Points.AddXY(4, dport[3]);
            chart1.Series[0].Points.AddXY(5, dport[4]);
            chart1.Series[0].Points.AddXY(6, dport[5]);
            double max = dport.Max();
            double bmax = 6;
            if (max != Convert.ToDouble (0))
            {
                bmax = max * 1.2;
            }
            chart1.ChartAreas[0].AxisY.Maximum = bmax;
            // 隐藏图示
            chart1.Legends[0].Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port1[i];
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port2[i];
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port3[i];
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port4[i];
                        }
                    }
                    break;
                case 4:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port5[i];
                        }
                    }
                    break;
                case 5:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            dport[i] = port6[i];
                        }
                    }
                    break;

            }
            chart1.Series[0].ChartType = SeriesChartType.Column;        
            chart1.Series[0].Color = Color.Blue;
            // 添加数据
            chart1.Series[0].Points.AddXY(1, dport[0]);
            chart1.Series[0].Points.AddXY(2, dport[1]);
            chart1.Series[0].Points.AddXY(3, dport[2]);
            chart1.Series[0].Points.AddXY(4, dport[3]);
            chart1.Series[0].Points.AddXY(5, dport[4]);
            chart1.Series[0].Points.AddXY(6, dport[5]);
            // Y的最大值
            double max = dport.Max();
            double bmax = 6;
            if (max != Convert.ToDouble(0))
            {
                bmax = max * 1.2;
            }
            chart1.ChartAreas[0].AxisY.Maximum = bmax;
            // 隐藏图示
            chart1.Legends[0].Enabled = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
