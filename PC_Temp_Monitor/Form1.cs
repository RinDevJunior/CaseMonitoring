using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;

namespace PC_Temp_Monitor
{
    public partial class Form1 : Form
    {
        #region Declare variable

        private Computer Computer { get; set; }
        private SerialPort Port { get; set; }
        private IList<Tuple<SensorType, object>> CpuNameDictionary { get; set; }
        private IList<Tuple<SensorType, object>> GpUNameDictionary { get; set; }
        private IList<Tuple<SensorType, object>> SuffixDictionary { get; set; }
        private IList<Tuple<SensorType, object>> GpuLabelDictionary { get; set; }
        private IList<Tuple<SensorType, object>> CpuLabelDictionary { get; set; }

        #endregion

        public Form1()
        {
            InitializeComponent();

            DefineList();
            SetupPort();
            Computer.Open();

            var timer = new Timer();
            timer.Tick += Status; // Every time timer ticks, timer_Tick will be called
            timer.Interval = 500; // Timer will tick event second
            timer.Enabled = true; // Enable the timer
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Status(object sender, EventArgs e)
        {
            foreach (var hardwadre in Computer.Hardware)
            {
                // GPU
                if (hardwadre.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwadre.Update();
                    foreach (var sensor in hardwadre.Sensors)
                    {
                        gpuName.Text = hardwadre.Name;
                        ShowValue(sensor, SetValue);
                    }
                }

                // CPU
                if (hardwadre.HardwareType == HardwareType.CPU)
                {
                    hardwadre.Update();
                    hardwadre.Sensors.ToList();
                    foreach (var sensor in hardwadre.Sensors)
                    {
                        cpuName.Text = hardwadre.Name;
                        ShowValue(sensor, SetValue);
                    }
                }
            }

            SendValue();
        }


        private void ShowValue(ISensor sensor, Action<ISensor, Func<ISensor, string>> setvalue)
        {
            switch (sensor.Hardware.HardwareType)
            {
                case HardwareType.GpuNvidia:
                    if (GpUNameDictionary.SelectWhere(condition: tuple => tuple.Item1 == sensor.SensorType, selector: tuple => tuple.Item2, output: out IList<string> gpuname) &&
                        sensor.Name.Equals(gpuname.First())) setvalue(sensor, GetSuffix);
                    break;
                case HardwareType.CPU:
                    if (CpuNameDictionary.SelectWhere(condition: tuple => tuple.Item1 == sensor.SensorType,
                            selector: tuple => tuple.Item2, output: out IList<string> name) &&
                        sensor.Name.Equals(name.First())) setvalue(sensor, GetSuffix);
                    break;
            }
        }

        private void SetValue(ISensor sensor, Func<ISensor, string> getSuffix)
        {
            switch (sensor.Hardware.HardwareType)
            {
                case HardwareType.GpuNvidia:
                    if (GpuLabelDictionary.SelectWhere(condition: tuple => tuple.Item1 == sensor.SensorType,
                        selector: tuple => tuple.Item2, output: out IList<Label> output))
                        output.First().Text = Convert.ToInt32(sensor.Value.GetValueOrDefault()) + getSuffix(sensor);
                    break;

                case HardwareType.CPU:
                    if (CpuLabelDictionary.SelectWhere(condition: tuple => tuple.Item1 == sensor.SensorType,
                        selector: tuple => tuple.Item2, output: out IList<Label> labels))
                        labels.First().Text = Convert.ToInt32(sensor.Value.GetValueOrDefault()) + getSuffix(sensor);
                    break;
            }
        }

        private string GetSuffix(ISensor sensor)
        {
            return SuffixDictionary.SelectWhere(condition: tuple => tuple.Item1 == sensor.SensorType,
                selector: tuple => tuple.Item2, output: out IList<string> suffix) ? suffix.First() : string.Empty;
        }

        private void SendValue()
        {
            var item = new
            {
                CPU = new { cpuName = cpuName.Text, cpuTemp = cpuTemp.Text, cpuLoad = cpuLoad.Text },
                GPU = new
                {
                    gpuName = gpuName.Text,
                    gpuLoad = gpuLoad.Text,
                    gpuClock = gpuClock.Text,
                    gpuFan = gpuFan.Text
                }
            };

            var text = JsonConvert.SerializeObject(item);
            richTextBox1.Focus();

            richTextBox1.AppendText($@"{Environment.NewLine}{text}");

            //try
            //{
            //    if (!port.IsOpen)
            //    {
            //        port.PortName = comboBox1.Items[0].ToString();
            //        port.Open();
            //    }

            //    port.Write(text);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void DefineList()
        {
            GpuLabelDictionary = new List<Tuple<SensorType, object>>
            {
                new Tuple<SensorType, object>(SensorType.Temperature, gpuTemp),
                new Tuple<SensorType, object>(SensorType.Clock, gpuClock),
                new Tuple<SensorType, object>(SensorType.Load, gpuLoad),
                new Tuple<SensorType, object>(SensorType.Fan, gpuFan)
            };

            CpuLabelDictionary = new List<Tuple<SensorType, object>>
            {
                new Tuple<SensorType, object>(SensorType.Temperature, cpuTemp),
                new Tuple<SensorType, object>(SensorType.Load, cpuLoad),
                new Tuple<SensorType, object>(SensorType.Clock, cpuCore1)
            };

            CpuNameDictionary = new List<Tuple<SensorType, object>>
            {
                new Tuple<SensorType, object>(SensorType.Temperature, "CPU Package"),
                new Tuple<SensorType, object>(SensorType.Load, "CPU Total"), 
                new Tuple<SensorType, object>(SensorType.Clock, "CPU Core #1")
            };

            GpUNameDictionary = new List<Tuple<SensorType, object>>
            {
                new Tuple<SensorType, object>(SensorType.Temperature, "GPU Core"),
                new Tuple<SensorType, object>(SensorType.Clock, "GPU Core"),
                new Tuple<SensorType, object>(SensorType.Load, "GPU Core"),
                new Tuple<SensorType, object>(SensorType.Fan, "GPU")
            };

            SuffixDictionary = new List<Tuple<SensorType, object>>
            {
                new Tuple<SensorType, object>(SensorType.Temperature, "℃"),
                new Tuple<SensorType, object>(SensorType.Clock, " MHz"),
                new Tuple<SensorType, object>(SensorType.Load, "%"),
                new Tuple<SensorType, object>(SensorType.Fan, " RPM")
            };

            Computer = new Computer
            {
                GPUEnabled = true,
                CPUEnabled = true
            };

            Port = new SerialPort();
        }

        private void SetupPort()
        {
            Port.Parity = Parity.None;
            Port.StopBits = StopBits.One;
            Port.DataBits = 8;
            Port.Handshake = Handshake.None;
            Port.RtsEnable = true;
            var ports = SerialPort.GetPortNames();
            foreach (var p in ports)
                comboBox1.Items.Add(p);
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch
            {
                //do nothing
            }
            Port.BaudRate = 9600;
        }
    }
}