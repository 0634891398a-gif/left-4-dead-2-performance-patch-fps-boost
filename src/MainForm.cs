```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Timers;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private System.Timers.Timer processCheckTimer;
        private readonly string processName = "left4dead2";
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            processCheckTimer = new System.Timers.Timer(2000); // Check every 2 seconds
            processCheckTimer.Elapsed += CheckGameProcess;
            processCheckTimer.AutoReset = true;
            processCheckTimer.Enabled = true;
        }

        private void CheckGameProcess(object sender, ElapsedEventArgs e)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0 && !isGameRunning)
            {
                isGameRunning = true;
                EnablePerformanceBoost();
            }
            else if (processes.Length == 0 && isGameRunning)
            {
                isGameRunning = false;
                DisablePerformanceBoost();
            }
        }

        private void EnablePerformanceBoost()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(EnablePerformanceBoost));
                return;
            }
            statusLabel.Text = "Performance boost enabled.";
            // Insert logic to apply performance boost for L4D2
        }

        private void DisablePerformanceBoost()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DisablePerformanceBoost));
                return;
            }
            statusLabel.Text = "Performance boost disabled.";
            // Insert logic to revert performance boost for L4D2
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                MessageBox.Show("Performance boost is already active.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2 to apply performance boosts.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeComponent()
        {
            this.patchButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // patchButton
            // 
            this.patchButton.Location = new System.Drawing.Point(12, 12);
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size(150, 30);
            this.patchButton.TabIndex = 0;
            this.patchButton.Text = "Toggle Performance Patch";
            this.patchButton.UseVisualStyleBackColor = true;
            this.patchButton.Click += new System.EventHandler(this.patchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12,