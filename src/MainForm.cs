```csharp
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string gameProcessName = "left4dead2";
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(300, 200);
            this.Text = "L4D2 Performance Patch";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += MainForm_FormClosing;

            Button patchButton = new Button()
            {
                Text = "Apply Performance Patch",
                Dock = DockStyle.Top
            };
            patchButton.Click += PatchButton_Click;

            Label statusLabel = new Label()
            {
                Text = "Status: Not monitoring",
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(patchButton);
            this.Controls.Add(statusLabel);

            processCheckTimer = new Timer();
            processCheckTimer.Interval = 2000; // Check every 2 seconds
            processCheckTimer.Tick += ProcessCheckTimer_Tick;

            processCheckTimer.Start();
        }

        private void InitializeCustomComponents()
        {
            // Additional initialization logic if needed
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsGameRunning();
            UpdateStatusLabel();
        }

        private void PatchButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Please start Left 4 Dead 2 before applying the patch.", "Game Not Running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add the logic for applying performance patches here
            MessageBox.Show("Performance patches applied successfully!", "Patch Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsGameRunning()
        {
            Process[] processes = Process.GetProcessesByName(gameProcessName);
            return processes.Length > 0;
        }

        private void UpdateStatusLabel()
        {
            string statusText = isGameRunning ? "Status: Game is running" : "Status: Game is not running";
            Controls[1].Text = statusText; // Assuming it's the second control
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }
    }
}
```