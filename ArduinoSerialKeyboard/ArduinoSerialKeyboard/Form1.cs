using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace ArduinoSerialKeyboard
{
    public partial class Form1 : Form
    {
        SerialPort port;
        bool started=false;
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            button1.Click += new EventHandler(startService);
            button2.Click += new EventHandler(button2_Click);
            button3.Click += new EventHandler(stopService);
            button4.Click += new EventHandler(disconnect);
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }
        void button2_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
                label1.Text = "Not connected";
            }
            port = new SerialPort("COM" + numericUpDown1.Value, Convert.ToInt32(numericUpDown2.Value));
            port.Open();
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            label1.Text = "Connected to " + port.PortName + ", " + port.BaudRate + " baud";
        }
        void PortWrite(string message)
        {
            if (port != null && port.IsOpen)
            {
                port.WriteLine(message);
            }
        }

        void disconnect(object sender, EventArgs e) {
            if (port != null && port.IsOpen)
            {
                port.Close();
                label1.Text = "Not connected";
            }
        }

        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            if (started)
            {
                SendKeys.SendWait(indata);
            }
        }
        void startService(object sender, EventArgs e) {
            label4.Text = "Started";
            started = true;
        }
        void stopService(object sender, EventArgs e) {
            label4.Text = "Stopped";
            started = false;
        }
    }
}