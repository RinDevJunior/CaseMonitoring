using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using Timer = System.Windows.Forms.Timer;


namespace PC_Temp_Monitor
{
    public partial class Form1 : Form
    {
        private SerialPort _port = new SerialPort();

        private readonly Computer _computer = new Computer()
        {
            GPUEnabled = true,
            CPUEnabled = true
        };

        private readonly IDictionary<SensorType, string> _gpUNameDictionary = new Dictionary<SensorType, string>
        {
            { SensorType.Temperature,"GPU Core" },
            { SensorType.Clock, "GPU Core"}
        };

        private readonly IDictionary<SensorType, string> _cpuNameDictionary = new Dictionary<SensorType, string>
        {
            { SensorType.Temperature, "CPU Package"},
            {SensorType.Load, "CPU Total" }
        };

        private readonly IDictionary<SensorType,string> _suffixDictionary = new Dictionary<SensorType, string>
        {
            { SensorType.Temperature, "℃"},
            { SensorType.Clock, " Mhz"},
            { SensorType.Load, "%"}
        };

        public IDictionary<SensorType, Label> GpuLabelDictionary { get; }
        public IDictionary<SensorType, Label> CpuLabelDictionary { get; }

        public Form1()
        {
            InitializeComponent();
            _computer.Open();
            GpuLabelDictionary = new Dictionary<SensorType, Label>
            {
                { SensorType.Temperature, gpuTemp},
                { SensorType.Clock, gpuLoad}
            };

            CpuLabelDictionary = new Dictionary<SensorType, Label>
            {
                {SensorType.Temperature, cpuTemp},
                {SensorType.Load, cpuLoad }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timer = new Timer();
            timer.Tick += Status; // Everytime timer ticks, timer_Tick will be called
            timer.Interval = 500;              // Timer will tick evert second
            timer.Enabled = true;                       // Enable the timer
            timer.Start();
        }

        private void Status(object sender, EventArgs e)
        {
            foreach (var hardwadre in _computer.Hardware)
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
                    foreach (var sensor in hardwadre.Sensors)
                    {
                        cpuName.Text = hardwadre.Name;
                        ShowValue(sensor, SetValue);
                    }
                }
            }
        }


        private void ShowValue(ISensor sensor, Action<ISensor, Func<ISensor, string>> setvalue)
        {
            switch (sensor.Hardware.HardwareType)
            {
                case HardwareType.GpuNvidia:
                    if (_gpUNameDictionary.TryGetValue(sensor.SensorType, out string gpuname) && sensor.Name.Equals(gpuname)) setvalue(sensor, GetSuffix());
                    break;
                case HardwareType.CPU:
                    if (_cpuNameDictionary.TryGetValue(sensor.SensorType, out string value) && sensor.Name.Equals(value)) setvalue(sensor, GetSuffix());
                    break;
            }

        }

        private void SetValue(ISensor sensor, Func<ISensor, string> getSuffix)
        {
            switch (sensor.Hardware.HardwareType)
            {
                case HardwareType.GpuNvidia:
                    if (GpuLabelDictionary.TryGetValue(sensor.SensorType, out Label gpuLabel)) gpuLabel.Text = Convert.ToInt32(sensor.Value.GetValueOrDefault()) + getSuffix(sensor);
                    break;

                case HardwareType.CPU:
                    if (CpuLabelDictionary.TryGetValue(sensor.SensorType, out Label cpuLabel)) cpuLabel.Text = Convert.ToInt32(sensor.Value.GetValueOrDefault()) + getSuffix(sensor);
                    break;
            }
        }


        private Func<ISensor, string> GetSuffix()
        {
            return sensor => _suffixDictionary.TryGetValue(sensor.SensorType, out string suffix) ? suffix : string.Empty;
        }

    }
}
