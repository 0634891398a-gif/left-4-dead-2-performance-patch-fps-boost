```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2"; // Process name without ".exe"
        private Timer processCheckTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Length > 0;
            statusLabel.Text = isGameRunning ? "Game is running" : "Game is not running";

            patchButton.Enabled = isGameRunning && !IsPatched();
            unpatchButton.Enabled = isGameRunning && IsPatched();
        }

        private bool IsPatched()
        {
            // Replace with actual patch check implementation
            return false; 
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Implement patching logic here
                MessageBox.Show("Patching the game for performance improvements.", "Patching", MessageBoxButtons.OK);
            }
        }

        private void unpatchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Implement unpatching logic here
                MessageBox.Show("Unpatching the game.", "Unpatching", MessageBoxButtons.OK);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }
    }
}
```