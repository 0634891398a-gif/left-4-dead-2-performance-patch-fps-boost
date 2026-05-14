```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Timer processDetectionTimer;
        private Button applyPatchButton;
        private Label statusLabel;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            StartProcessDetectionTimer();
        }

        private void InitializeComponent()
        {
            this.applyPatchButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // applyPatchButton
            // 
            this.applyPatchButton.Location = new System.Drawing.Point(100, 50);
            this.applyPatchButton.Name = "applyPatchButton";
            this.applyPatchButton.Size = new System.Drawing.Size(100, 30);
            this.applyPatchButton.TabIndex = 0;
            this.applyPatchButton.Text = "Apply Patch";
            this.applyPatchButton.UseVisualStyleBackColor = true;
            this.applyPatchButton.Click += new System.EventHandler(this.ApplyPatchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(100, 100);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.applyPatchButton);
            this.Name = "MainForm";
            this.Text = "L4D2 Performance Patch";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeCustomComponents()
        {
            processDetectionTimer = new Timer();
            processDetectionTimer.Interval = 1000; // Check every second
            processDetectionTimer.Tick += ProcessDetectionTimer_Tick;
        }

        private void StartProcessDetectionTimer()
        {
            processDetectionTimer.Start();
        }

        private void ProcessDetectionTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            statusLabel.Text = isGameRunning ? "Game is running..." : "Game is not running.";
            applyPatchButton.Enabled = isGameRunning;
        }

        private void ApplyPatchButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("The game is not currently running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform the patching logic here. Placeholder implementation.
            MessageBox.Show("Patching L4D2 performance settings...", "Patching", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Here, you could call other methods to apply the actual patching logic.
        }
    }
}
```