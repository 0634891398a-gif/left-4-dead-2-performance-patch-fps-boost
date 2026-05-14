```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string gameProcessName = "left4dead2";
        private const string patchMessage = "Applying performance patch...";
        
        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.checkButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(50, 40);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(200, 40);
            this.checkButton.TabIndex = 0;
            this.checkButton.Text = "Patch L4D2 Performance";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(50, 100);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.checkButton);
            this.Name = "MainForm";
            this.Text = "L4D2 Performance Patch";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeProcessCheckTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 2000; // Check every 2 seconds
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (IsGameRunning())
            {
                statusLabel.Text = "Game is running. Click to apply patch.";
                checkButton.Enabled = true;
            }
            else
            {
                statusLabel.Text = "Game is not running.";
                checkButton.Enabled = false;
            }
        }

        private bool IsGameRunning()
        {
            Process[] processes = Process.GetProcessesByName(gameProcessName);
            return processes.Length > 0;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            if (IsGameRunning())
            {
                ApplyPerformancePatch();
            }
            else
            {
                MessageBox.Show("L4D2 is not running. Please start the game and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyPerformancePatch()
        {
            statusLabel.Text = patchMessage;

            // Add the performance patch logic here (placeholder)
            System