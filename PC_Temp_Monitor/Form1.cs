using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using Timer = System.Windows.Forms.Timer;


namespace PC_Temp_Monitor
{
    public partial class Form1 : Form
    {
        SerialPort port = new SerialPort();

        Computer _computer = new Computer()
        {
            GPUEnabled = true,
            CPUEnabled = true
        };

        static float value1;
        static float value2;

        public Form1()
        {
            InitializeComponent();
            _computer.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timer = new Timer();
            timer.Tick += Status; // Everytime timer ticks, timer_Tick will be called
            timer.Interval = (1000) * (1);              // Timer will tick evert second
            timer.Enabled = true;                       // Enable the timer
            timer.Start();
        }

        private void Status(object sender, EventArgs e)
        {
            foreach (var hardwadre in _computer.Hardware)
            {
                if (hardwadre.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwadre.Update();
                    foreach (var sensor in hardwadre.Sensors)
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            value1 = sensor.Value.GetValueOrDefault();
                            gpuTemp.Text = value1.ToString(CultureInfo.InvariantCulture);
                        }
                }
                if (hardwadre.HardwareType == HardwareType.CPU)
                {
                    hardwadre.Update();
                    foreach (var sensor in hardwadre.Sensors)
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            value2 = sensor.Value.GetValueOrDefault();
                            cpuTemp.Text = value2.ToString(CultureInfo.InvariantCulture);
                        }
                }
            }
        }

    }
}
