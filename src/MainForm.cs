```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private const int RefreshInterval = 1000; // In milliseconds

        private Timer processDetectionTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processDetectionTimer = new Timer();
            processDetectionTimer.Interval = RefreshInterval;
            processDetectionTimer.Tick += ProcessDetectionTimer_Tick;
            processDetectionTimer.Start();
        }

        private void ProcessDetectionTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (isGameRunning)
            {
                statusLabel.Text = "Game Running";
                patchButton.Enabled = true;
            }
            else
            {
                statusLabel.Text = "Game Not Running";
                patchButton.Enabled = false;
            }
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Please start Left 4 Dead 2 first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Code to apply performance patches goes here.
            ApplyPerformancePatches();
        }

        private void ApplyPerformancePatches()
        {
            // Example of simple patching logic
            // In a real scenario, this would involve modifying game config files or memory
            MessageBox.Show("Performance patches have been applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processDetectionTimer.Stop();
            processDetectionTimer.Dispose();
        }
    }
}
```