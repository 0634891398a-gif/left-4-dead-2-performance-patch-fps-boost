```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "Left4Dead2"; // Main game process name
        private const int TimerInterval = 1000; // Timer set to check every second
        private Timer processDetectionTimer;
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessDetectionTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.Size = new System.Drawing.Size(400, 300);

            var patchButton = new Button
            {
                Text = "Apply Performance Patch",
                Dock = DockStyle.Bottom
            };
            patchButton.Click += PatchButton_Click;

            var statusLabel = new Label
            {
                Text = "Status: Waiting for game to start...",
                Dock = DockStyle.Top,
                AutoSize = true
            };

            this.Controls.Add(statusLabel);
            this.Controls.Add(patchButton);
        }

        private void InitializeProcessDetectionTimer()
        {
            processDetectionTimer = new Timer();
            processDetectionTimer.Interval = TimerInterval;
            processDetectionTimer.Elapsed += CheckGameProcess;
            processDetectionTimer.Start();
        }

        private void CheckGameProcess(object sender, ElapsedEventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateStatus));
                return;
            }

            var statusLabel = this.Controls.OfType<Label>().FirstOrDefault();
            statusLabel.Text = isGameRunning
                ? "Status: Game is running."
                : "Status: Waiting for game to start...";
        }

        private void PatchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Perform patch actions here
                MessageBox.Show("Performance patch applied successfully!", "Patch Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Game is not running. Please start Left 4 Dead 2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
```