```csharp
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string gameProcessName = "left4dead2";
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processCheckTimer = new Timer
            {
                Interval = 2000 // Check every 2 seconds
            };
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessOpen(gameProcessName);
            UpdateUI();
        }

        private bool IsProcessOpen(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            return processes.Length > 0;
        }

        private void UpdateUI()
        {
            if (isGameRunning)
            {
                statusLabel.Text = "Left 4 Dead 2 is running.";
                patchButton.Enabled = true;
            }
            else
            {
                statusLabel.Text = "Left 4 Dead 2 is not running.";
                patchButton.Enabled = false;
            }
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Implement performance patch logic here
                MessageBox.Show("Performance patch applied!");
            }
            else
            {
                MessageBox.Show("Please run Left 4 Dead 2 first.");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
```