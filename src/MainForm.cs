```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "hl2"; // Left 4 Dead 2's process name
        private Timer processCheckTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.processCheckTimer = new System.Windows.Forms.Timer();
            this.startPatchButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // startPatchButton
            // 
            this.startPatchButton.Location = new System.Drawing.Point(12, 12);
            this.startPatchButton.Name = "startPatchButton";
            this.startPatchButton.Size = new System.Drawing.Size(120, 30);
            this.startPatchButton.TabIndex = 0;
            this.startPatchButton.Text = "Apply Patch";
            this.startPatchButton.UseVisualStyleBackColor = true;
            this.startPatchButton.Click += new System.EventHandler(this.StartPatchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 55);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(103, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Status: Not Running";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.startPatchButton);
            this.Controls.Add(this.statusLabel);
            this.Name = "MainForm";
            this.Text = "L4D2 Performance Patch";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeTimer()
        {
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessOpen(GameProcessName);
            statusLabel.Text = isGameRunning ? "Status: Running" : "Status: Not Running";
        }

        private bool IsProcessOpen(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private void StartPatchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                MessageBox.Show("Please exit the game before applying the patch.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ApplyPatch();
        }

        private void ApplyPatch()
        {
            // Logic to apply patch
            MessageBox.Show("Patch applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Button startPatch