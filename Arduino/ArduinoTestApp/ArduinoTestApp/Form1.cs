using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management.Instrumentation;
using System.Management;

namespace ArduinoTestApp
{
    public partial class Form1 : Form
    {
        string _datastream;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementScope connectionScope = new ManagementScope();
           // SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            SelectQuery _qry = new SelectQuery("SELECT * FROM Win32_PnPEntity");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, _qry);
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, "SELECT * FROM MSSerial_PortName");
            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    if(desc.Contains("USB Serial Port"))
                    {
                        //MessageBox.Show(item["Name"].ToString());
                        string[] sep = {"(",")" };
                        string[] data = item["Name"].ToString().Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        comboBox1.Items.Add(data[1].ToString());
                    }
        
                }
                button1.Enabled = true;
                button2.Enabled = false;

            }
            catch (ManagementException ex)
            {
                /* Do Nothing */
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenPort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
            //textBox1.Text = "LED is on!";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("0");
            //textBox1.Text = "LED is on!";
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //OpenPort();
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("1");
            }
            else
            {
                OpenPort();
                serialPort1.Write("1");
            }
        }

        private void OpenPort()
        {
            if (serialPort1.IsOpen)
            {
                MessageBox.Show("Port is already open");
            }
            else
            {
                if(string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
                {
                    MessageBox.Show("No port selected");
                }
                else
                {                                                                                                   
                  serialPort1.PortName = comboBox1.SelectedItem.ToString();
                  serialPort1.BaudRate = 9600;
                  serialPort1.Open();
                }
            }
        }

        private void ClosePort()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
           // richTextBox1.Clear();
            if (serialPort1.IsOpen)
            {
                _datastream += serialPort1.ReadLine();
            }
            //for (int i = 0; i <= 10; i++)
            //{
            //    _datastream+=serialPort1.ReadLine();
            //}
            //richTextBox1.Text += _datastream;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("0");
            //ClosePort();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClosePort();
            richTextBox1.Clear();
            richTextBox1.Text += _datastream;
        }

    }
}
