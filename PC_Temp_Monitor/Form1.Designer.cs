namespace PC_Temp_Monitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cpuName = new System.Windows.Forms.Label();
            this.cpuTemp = new System.Windows.Forms.Label();
            this.gpuName = new System.Windows.Forms.Label();
            this.gpuTemp = new System.Windows.Forms.Label();
            this.cpuLoad = new System.Windows.Forms.Label();
            this.gpuLoad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(849, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cpuName
            // 
            this.cpuName.AutoSize = true;
            this.cpuName.Location = new System.Drawing.Point(32, 24);
            this.cpuName.Name = "cpuName";
            this.cpuName.Size = new System.Drawing.Size(32, 13);
            this.cpuName.TabIndex = 1;
            this.cpuName.Text = "CPU:";
            // 
            // cpuTemp
            // 
            this.cpuTemp.AutoSize = true;
            this.cpuTemp.Location = new System.Drawing.Point(405, 24);
            this.cpuTemp.Name = "cpuTemp";
            this.cpuTemp.Size = new System.Drawing.Size(52, 13);
            this.cpuTemp.TabIndex = 2;
            this.cpuTemp.Text = "cpuTemp";
            // 
            // gpuName
            // 
            this.gpuName.AutoSize = true;
            this.gpuName.Location = new System.Drawing.Point(31, 55);
            this.gpuName.Name = "gpuName";
            this.gpuName.Size = new System.Drawing.Size(33, 13);
            this.gpuName.TabIndex = 3;
            this.gpuName.Text = "GPU:";
            // 
            // gpuTemp
            // 
            this.gpuTemp.AutoSize = true;
            this.gpuTemp.Location = new System.Drawing.Point(405, 55);
            this.gpuTemp.Name = "gpuTemp";
            this.gpuTemp.Size = new System.Drawing.Size(52, 13);
            this.gpuTemp.TabIndex = 4;
            this.gpuTemp.Text = "gpuTemp";
            // 
            // cpuLoad
            // 
            this.cpuLoad.AutoSize = true;
            this.cpuLoad.Location = new System.Drawing.Point(651, 24);
            this.cpuLoad.Name = "cpuLoad";
            this.cpuLoad.Size = new System.Drawing.Size(27, 13);
            this.cpuLoad.TabIndex = 5;
            this.cpuLoad.Text = "load";
            // 
            // gpuLoad
            // 
            this.gpuLoad.AutoSize = true;
            this.gpuLoad.Location = new System.Drawing.Point(654, 55);
            this.gpuLoad.Name = "gpuLoad";
            this.gpuLoad.Size = new System.Drawing.Size(35, 13);
            this.gpuLoad.TabIndex = 6;
            this.gpuLoad.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 261);
            this.Controls.Add(this.gpuLoad);
            this.Controls.Add(this.cpuLoad);
            this.Controls.Add(this.gpuTemp);
            this.Controls.Add(this.gpuName);
            this.Controls.Add(this.cpuTemp);
            this.Controls.Add(this.cpuName);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label cpuName;
        private System.Windows.Forms.Label cpuTemp;
        private System.Windows.Forms.Label gpuName;
        private System.Windows.Forms.Label gpuTemp;
        private System.Windows.Forms.Label cpuLoad;
        private System.Windows.Forms.Label gpuLoad;
    }
}

