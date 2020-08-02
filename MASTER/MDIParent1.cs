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
using System.Timers;
using System.Collections;
using System.IO;

namespace MASTER
{
    public partial class MDIParent1 : Form
    {   //起始地址要减一
        // 01 05 15||03 04 06 16
        //register两种数据类型；
        string display = null;//显示
        //TX########################
        public Int16 slave010304;
        public Int16 slave05;
        public Int16 slave06;
        public Int16 slave15;
        public Int16 slave16;
        public Int16 function;
        public Int16 address010304;
        public Int16 address05;
        public Int16 address06;
        public Int16 address15;
        public Int16 address16;
        public Int16 length010304;
        public Int16 length05;
        public Int16 length06;
        public Int16 length15;
        public Int16 length16;
        //控制和传输##################
        public  string leixing = "0";//默认int类型
        public bool onoff;   //开关量
        public Int16 value06;//寄存器量
        System.Timers.Timer t1 = new System.Timers.Timer(1000);
        public System.Timers.Timer t2 = new System.Timers.Timer(1000);
        public System.Timers.Timer t3 = new System.Timers.Timer(1500);
        //写从机#####################
        string s15;
        string s16;
        string sendstring;
        string recestring;
      
        
      public MDIParent1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            

        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }//以上为框体生成代码########################################
        

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            childForm8.Text = " 报文 ";
            t1.AutoReset = true;
            t1.Enabled = false;
            t2.AutoReset = true;
            t2.Enabled = false;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(recieve);
            t1.Elapsed += new ElapsedEventHandler(_fa);//（elapse-流逝）
            t2.Elapsed += new ElapsedEventHandler(_xianshi);
            t3.Elapsed += new ElapsedEventHandler(_clear);
        }
        private void _clear(object source, System.Timers.ElapsedEventArgs e)
        {
            recestring = " ";
        }
        Form8 childForm8 = new Form8();

        private void _xianshi(object source, System.Timers.ElapsedEventArgs e)//主从应答
        {
            if (recestring != " ")
            {
                string sr = sendstring + "\r\n" + recestring + "\r\n";
                childForm8.textBox1.Text += sr;
            }
            else
            {
                string sr = sendstring + "\r\n";
                childForm8.textBox1.Text += sr;
            }
            
        }

        private void _fa(object source, System.Timers.ElapsedEventArgs e)
        {
            switch (function)
            {
                case 1:
                    send01();
                    break;
                case 4:
                    send04();
                    break;
                case 3:
                    send03();
                    break;
            }
        }

        //主机问########################################################
        private void send01()
        {
            byte[] _address = new byte[2];
            byte[] _slave = new byte[2];
            byte[] _length = new byte[2];
            ConvertIntToByteArray(address010304, ref _address);
            ConvertIntToByteArray(slave010304, ref _slave);
            ConvertIntToByteArray(length010304, ref _length);
            byte[] data = { _slave[0], 0x01, _address[1], _address[0], _length[1], _length[0] };
            byte[] CRC = GetCRC(data);
            byte[] data1 = { _slave[0], 0x01, _address[1], _address[0], _length[1], _length[0], CRC[0], CRC[1] };
            serialPort1.Write(data1, 0, data1.Length);
            sendstring = "TX:  ";
            for (int i = 0; i < data1.Length; i++)
            {
                sendstring += data1[i].ToString("X2") + "  ";//X2使得A=>0A.并加空格
            }
        }

        private void send03()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            byte[] lengthb = new byte[2];
            ConvertIntToByteArray(address010304, ref addressb);
            ConvertIntToByteArray(slave010304, ref slaveb);
            ConvertIntToByteArray(length010304, ref lengthb);
            byte[] data = { slaveb[0], 0x03, addressb[1], addressb[0], lengthb[1], lengthb[0] };//供crc调用
            byte[] CRC = GetCRC(data);
            byte[] data1 = { slaveb[0], 0x03, addressb[1], addressb[0], lengthb[1], lengthb[0], CRC[0], CRC[1] };
            serialPort1.Write(data1, 0, data1.Length);
            sendstring = "TX:  ";
            for (int i = 0; i < data1.Length; i++)
            {
                sendstring += data1[i].ToString("X2") + "  ";
            }
        }

        private void send04()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            byte[] lengthb = new byte[2];
            ConvertIntToByteArray(address010304, ref addressb);
            ConvertIntToByteArray(slave010304, ref slaveb);
            ConvertIntToByteArray(length010304, ref lengthb);
            byte[] data = { slaveb[0], 0x04, addressb[1], addressb[0], lengthb[1], lengthb[0] };
            byte[] CRC = GetCRC(data);
            byte[] data1 = { slaveb[0], 0x04, addressb[1], addressb[0], lengthb[1], lengthb[0], CRC[0], CRC[1] };
            serialPort1.Write(data1, 0, data1.Length);
            sendstring = "TX:  ";
            for (int i = 0; i < data1.Length; i++)
            {
                sendstring += data1[i].ToString("X2") + "  ";
            }
        }

        private void send05()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            ConvertIntToByteArray(address05, ref addressb);
            ConvertIntToByteArray(slave05, ref slaveb);
            byte _onoff;
            if (onoff == true)
            {
                _onoff = 0xFF;
            }
            else
            {
                _onoff = 0x00;
            }
            byte[] data = { slaveb[0], 0x05, addressb[1], addressb[0], _onoff, 0x00 };
            byte[] CRC = GetCRC(data);
            byte[] data05 = { slaveb[0], 0x05, addressb[1], addressb[0], _onoff, 0x00, CRC[0], CRC[1] };
            serialPort1.Write(data05, 0, data05.Length);
            sendstring = "TX:  ";
            for (int i = 0; i < data05.Length; i++)
            {
                sendstring += data05[i].ToString("X2") + "  ";
            }
        }
        private void send06()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            byte[] valueb = new byte[2];
            ConvertIntToByteArray(address06, ref addressb);
            ConvertIntToByteArray(slave06, ref slaveb);
            ConvertIntToByteArray(value06, ref valueb);
            byte[] data = { slaveb[0], 0x06, addressb[1], addressb[0], valueb[1], valueb[0] };
            byte[] CRC = GetCRC(data);
            byte[] data06 = { slaveb[0], 0x06, addressb[1], addressb[0], valueb[1], valueb[0], CRC[0], CRC[1] };
            serialPort1.Write(data06, 0, data06.Length);
            sendstring = "TX:  ";
            for (int i = 0; i < data06.Length; i++)
            {
                sendstring += data06[i].ToString("X2") + "  ";
            }
        }

        private void send15()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            byte[] lengthb = new byte[2];
            ConvertIntToByteArray(address15, ref addressb);
            ConvertIntToByteArray(slave15, ref slaveb);
            ConvertIntToByteArray(length15, ref lengthb);
            string sendnoNull = s15.Trim();
            string[] strArray = sendnoNull.Split(' ');
            int byteBufferLength = strArray.Length;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == "")
                {
                    byteBufferLength--;
                }
            }
            BitArray mybit = new BitArray(byteBufferLength);//bitarray二进制值数组
            int ii = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                int decNum = 0;
                if (strArray[i] == "")
                {
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16);
                }
                try
                {
                    mybit[ii] = Convert.ToBoolean(decNum);
                }
                catch { }
                ii++;
            }
            string mybi = "";
            for (int i = 0; i < byteBufferLength; i++)
            {
                mybi += mybit[i].ToString();
            }
            int n = 1;
            while (length15 > n * 8)
            {
                n++;
            }//求出字节长度
            byte[] data = new byte[n];
            for (int i = 0; i < n - 1; i++)
            {
                for (int m = 0; m < 8; m++)
                {
                    data[i] = Convert.ToByte(data[i] * 0x02 + Convert.ToInt16(mybit[i * 8 + 7 - m]));// 左移+处理下一位

                }//data隐式转化
            }
            //处理余数字节
            int tail = length15 - (n - 1) * 8;
            for (int i = 0; i < tail; i++)
            {
                data[n - 1] = Convert.ToByte(data[n - 1] * 0x02 + Convert.ToInt16(mybit[byteBufferLength - i - 1]));
            }
            byte[] data1 = new byte[7+ n ];//从机 功能 地址 地址 数量 数量 字节数
            data1[0] = slaveb[0];
            data1[1] = 0x0F;
            data1[2] = addressb[1];
            data1[3] = addressb[0];
            data1[4] = lengthb[1];
            data1[5] = lengthb[0];
            data1[6] = Convert.ToByte(n);
            for (int i = 0; i < n; i++)
            {
                data1[7 + i] = data[i];
            }
            byte[] CRC = GetCRC(data1);
            byte[] data2 = new byte[6 + n + 1 + 2];
            for (int i = 0; i < 6 + n + 1; i++)
            {
                data2[i] = data1[i];
            }
            data2[6 + n + 1] = CRC[0];
            data2[6 + n + 2] = CRC[1];
            serialPort1.Write(data2, 0, data2.Length);
            /*string sho = "";
            for (int i = 0; i < n; i++)
            {
                sho += data[i].ToString("X2");
            }*/
            sendstring = "TX:  ";
            for (int i = 0; i < data2.Length; i++)
            {
                sendstring += data2[i].ToString("X2") + "  ";
            }
        }

        private void send16()
        {
            byte[] addressb = new byte[2];
            byte[] slaveb = new byte[2];
            byte[] lengthb = new byte[2];
            ConvertIntToByteArray(address16, ref addressb);
            ConvertIntToByteArray(slave16, ref slaveb);
            ConvertIntToByteArray(length16, ref lengthb);
            string sendnoNull = s16.Trim();
            string[] strArray = sendnoNull.Split(' ');//空格分割
            int byteBufferLength = strArray.Length;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == "")
                {
                    byteBufferLength--;
                }
            }
            Int16[] myint = new Int16[byteBufferLength];
            int ii = 0;
            for (int i = 0; i < strArray.Length; i++)   
            {
                int decNum = 0;
                if (strArray[i] == "")
                {
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16);
                }
                try
                {
                    myint[ii] = Convert.ToInt16(decNum);
                }
                catch
                {
                }
                ii++;
            }
            byte[] data = new byte[7 + length16 * 2];//从机 16 地址 地址 长 长 计数
            data[0] = slaveb[0];
            data[1] = 0x10;
            data[2] = addressb[1];
            data[3] = addressb[0];
            data[4] = lengthb[1];
            data[5] = lengthb[0];
            data[6] = Convert.ToByte(length16 * 2);
            for (int i = 0; i < length16; i++)
            {
                byte[] shujv = new byte[2];//分寄存器转化
                ConvertIntToByteArray(myint[i], ref shujv);
                data[7 + i * 2] = shujv[1];
                data[8 + i * 2 ] = shujv[0];
            }
            byte[] CRC = GetCRC(data);
            byte[] data2 = new byte[7 + length16 * 2 + 2];
            for (int i = 0; i < 7 + length16 * 2; i++)
            {
                data2[i] = data[i];
            }
            data2[7 + length16 * 2] = CRC[0];
            data2[8 + length16 * 2] = CRC[1];
            serialPort1.Write(data2, 0, data2.Length);
            string sho = "";
            for (int i = 0; i < 9 + length16 * 2 ; i++)
            {
                sho += data2[i].ToString("X2");
            }
            sendstring = "TX:  ";
            for (int i = 0; i < data2.Length; i++)
            {
                sendstring += data2[i].ToString("X2") + "  ";
            }
        }
        //接收显示#########################################
        private void recieve(object sender, SerialDataReceivedEventArgs e)//接收事件触发
        {
            try
            {
                t3.Stop();
                Byte[] receivedData = new Byte[serialPort1.BytesToRead];       
                serialPort1.Read(receivedData, 0, receivedData.Length);     //从缓冲区读取数据                          
                recestring = "RX:  ";
                for (int i = 0; i < receivedData.Length; i++)
                {
                    recestring += receivedData[i].ToString("X2") + "  ";
                }
                if (Convert.ToInt16(receivedData[1]) > 80)
                {
                    t1.Stop();
                    t2.Stop();
                    MessageBox.Show("设置错误");
                    serialPort1.Close();
                    断开连接ToolStripMenuItem.Enabled = false;
                    连接ToolStripMenuItem1.Enabled = true;
                }
                    
                switch (function)
                {
                    case 1:
                        recieve01(receivedData);
                        break;
                    case 3:
                        recieve03(receivedData);
                        break;
                    case 4:
                        recieve04(receivedData);
                        break;

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");

            }
            display = "";
            t3.Interval = 1500;
            t3.Start();
        }


        private void recieve01(Byte[] receivedData)
        {
            try
            {
                int n = receivedData[2];//第三个字节
                byte[] shuzhi = new byte[n];
                for (int i = 0; i < n; i++)
                {
                    shuzhi[i] = receivedData[3 + i];
                }
                BitArray mybit = new BitArray(shuzhi);

                for (int i = 0; i < length010304; i++)
                {
                    display += "线圈： " + Convert.ToString(address010304 + 1 + i) + " = " + Convert.ToInt16(mybit[i]).ToString() + "\r\n" + "\r\n";
                }
                if (textBox1.Text != display)//刷新
                {
                    textBox1.Text = display;
                }
            }
            catch
            {
            }
        }
        public float[] d = new float[6];
        private void recieve03(Byte[] receivedData)
        {
            try
            {
                int n = receivedData[2];
                int m = n / 2;
                int L = n / 4;
                byte[] _int = new byte[2];
                byte[] _float = new byte[4];
                if (leixing == "0")
                {
                    for (int i = 0; i < m; i++)
                    {
                        _int[0] = receivedData[3 + i * 2];
                        _int[1] = receivedData[3 + i * 2 + 1];
                        int zhi = Convert.ToInt16(receivedData[3 + i * 2]) * 256 + Convert.ToInt16(receivedData[3 + i * 2 + 1]);
                        display += "寄存器： " + Convert.ToString(address010304 + 1 + i) + " = " + zhi.ToString() + "\r\n" + "\r\n";
                    }
                }
                else
                {
                    for (int i = 0; i < L; i++)
                    {
                        _float[1] = receivedData[3 + i * 4];
                        _float[0] = receivedData[3 + i * 4 + 1];
                        _float[3] = receivedData[3 + i * 4 + 2];
                        _float[2] = receivedData[3 + i * 4 + 3];
                        float zhi = BitConverter.ToSingle(_float, 0);
                        display += "寄存器： " + Convert.ToString(address010304 + 1 + i * 2) + " = " + zhi.ToString() + "\r\n" + "\r\n";
                        display += "寄存器： " + Convert.ToString(address010304 + 1 + i * 2 + 1) + " = " + "\r\n" + "\r\n";
                    }

                }
                if (textBox1.Text != display)
                {
                    textBox1.Text = display;
                }
            }
            catch
            {
            }
        }
        Form9 check = new Form9();
        private void recieve04(Byte[] receivedData)
        {   
            try
            {   
                int n = receivedData[2];
                int rint = n / 2;
                int rfloat = n / 4;
                byte[] _int = new byte[2];
                byte[] _float = new byte[4];
                if (leixing == "0")//0、1表示两种类型
                {
                    for (int i = 0; i < rint; i++)
                    {
                        _int[0] = receivedData[3 + i * 2];
                        _int[1] = receivedData[3 + i * 2 + 1];
                        int zhi = Convert.ToInt16(receivedData[3 + i * 2]) * 256 + Convert.ToInt16(receivedData[3 + i * 2 + 1]);
                        display += "温度： " + Convert.ToString(address010304 + 1 + i) + " = " + zhi.ToString() + "\r\n" + "\r\n";
                    }
                }
                else
                {
                    for (int i = 0; i < rfloat; i++)
                    {
                        _float[1] = receivedData[3 + i * 4];//低位高 低位低 高位高 高位低 即顺序1032
                        _float[0] = receivedData[3 + i * 4 + 1];
                        _float[3] = receivedData[3 + i * 4 + 2];
                        _float[2] = receivedData[3 + i * 4 + 3];
                        float zhi = BitConverter.ToSingle(_float, 0);
                        d[i] = zhi;
                        check.d[i] = d[i];
                        display += "温度： " + Convert.ToString(address010304 + 1 + i * 2) + " = " + zhi.ToString() + "\r\n" + "\r\n";
                        display += "温度： " + Convert.ToString(address010304 + 1 + i * 2 + 1) + " = " + "\r\n" + "\r\n";//显示空白
                    }

                }
                if (textBox1.Text != display)
                {
                    textBox1.Text = display;
                }
            }
            catch {
                     }
        }

        //菜单#############################################

        private void 连接ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 childForm = new Form3();
            childForm.Text = "连接";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                serialPort1.PortName = childForm.serialPort1.PortName;
                serialPort1.BaudRate = childForm.serialPort1.BaudRate;
                serialPort1.DataBits = childForm.serialPort1.DataBits;
                serialPort1.StopBits = childForm.serialPort1.StopBits;
                serialPort1.Parity = childForm.serialPort1.Parity;
            }
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
            try
            {
                serialPort1.Open();
                t1.Enabled = true;
                t2.Enabled = true;
                t1.Start();
                t2.Start();
                连接ToolStripMenuItem1.Enabled = false;
                断开连接ToolStripMenuItem.Enabled = true;
                toolStripMenuItem3.Enabled = true;
                toolStripMenuItem4.Enabled = true;
                toolStripMenuItem5.Enabled = true;
                toolStripMenuItem6.Enabled = true;
            }
            catch
            {
                MessageBox.Show("串口打开错误");
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form2 childForm = new Form2();
            childForm.Text = "设置";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                address010304 = childForm.address;
                slave010304 = childForm.slave;
                function = childForm.function;
                length010304 = childForm.length;
            }
            if (childForm.comboBox1.SelectedIndex == 2 & leixing == "1")
            {
                windowsMenu.Enabled = true;
            }
            t1.Interval = 1000;
            t2.Interval = 1000;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form4 childForm = new Form4();
            childForm.Text = " 设置";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                address05 = childForm.address;
                slave05 = childForm.slave;
                onoff = childForm.onoff;
            }
            function = 1;
            send05();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form5 childForm = new Form5();
            childForm.Text = " 设置";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                address15 = childForm.address;
                slave15 = childForm.slave;
                length15 = childForm.length;
                s15 = childForm.s;
            }
            function = 1;
            send15();
        }

        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            断开连接ToolStripMenuItem.Enabled = false;
            连接ToolStripMenuItem1.Enabled = true;
            toolStripMenuItem3.Enabled = false;
            toolStripMenuItem4.Enabled = false;
            toolStripMenuItem5.Enabled = false;
            toolStripMenuItem6.Enabled = false;
            t1.Enabled = false;
            t2.Enabled = false;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form6 childForm = new Form6();
            childForm.Text = "设置";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                address06 = childForm.address;
                slave06 = childForm.slave;
                value06 = childForm.value;
            }
            function = 3;
            send06();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Form7 childForm = new Form7();
            childForm.Text = " 设置";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            if (childForm.DialogResult == DialogResult.OK)
            {
                address16 = childForm.address;
                slave16 = childForm.slave;
                length16 = childForm.length;
                s16 = childForm.s;
            }
            function = 3;
            send16();
        }

        private void 数据传输ToolStripMenuItem_Click(object sender, EventArgs e)//选择后订阅
        {
            childForm8.textBox1.Text = "";
            childForm8.Show();
            childForm8.TransfEvent += xianshikongzhi;
        }

        private void floatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing = "1";
            floatToolStripMenuItem.Enabled = false;
            intToolStripMenuItem.Enabled = true;
        }

        private void intToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing = "0";
            intToolStripMenuItem.Enabled = false;
            floatToolStripMenuItem.Enabled = true;
        }

        private void 功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 数据类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void windowsMenu_Click(object sender, EventArgs e)
        {      
            check.Text = " 温度折线图";
            check.StartPosition = FormStartPosition.CenterScreen;
            check.ShowDialog();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 childForm = new Form1();
            childForm.Text = "帮助";
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
        }
        //善后处理
                private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定关闭？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                serialPort1.Close();
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        //其他##########################################
        void xianshikongzhi(string value)//互报文显示的停止和开始
        {
            if (value == "1")
            {
                t2.Start();
            }
            else if (value == "0")
            {
                t2.Stop();
            }
        }

        static void ConvertIntToByteArray(Int16 m, ref byte[] _array)//ref表示为引用传递（地址传送）
        {
            _array[0] = (byte)(m & 0xFF);
            _array[1] = (byte)((m & 0xFF00) >> 8);
        }

        private byte[] GetCRC(byte[] byteData)//CRC校验码
        {
            byte[] CRC = new byte[2];

            UInt16 wCrc = 0xFFFF;
            for (int i = 0; i < byteData.Length; i++)
            {
                wCrc ^= Convert.ToUInt16(byteData[i]);
                for (int j = 0; j < 8; j++)
                {
                    if ((wCrc & 0x0001) == 1)
                    {
                        wCrc >>= 1;
                        wCrc ^= 0xA001;//异或多项式
                    }
                    else
                    {
                        wCrc >>= 1;
                    }
                }
            }

            CRC[1] = (byte)((wCrc & 0xFF00) >> 8);//取高位在后
            CRC[0] = (byte)(wCrc & 0x00FF);       //取低位在前
            return CRC;
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileMenu_Click(object sender, EventArgs e)
        {
            try
            {if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("当前无数据");
                }
                else
                {
                    OpenFileDialog _savefd = new OpenFileDialog();
                    _savefd.Filter = "TXT文本|*.txt";
                    if (_savefd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(_savefd.FileName, textBox1.Text);
                        File.AppendAllText(_savefd.FileName, "\r\n--------" + DateTime.Now.ToString() + "\r\n");
                        MessageBox.Show("保存成功");
                    }
                }
            }
            catch
            {
                MessageBox.Show("保存数据出错");
            }
        }
    }
}
