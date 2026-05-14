```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private const int CheckInterval = 2000; // 2 seconds
        private Timer processDetectionTimer;
        private Process gameProcess;

        public MainForm()
        {
            InitializeComponent();
            StartProcessDetection();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.Size = new System.Drawing.Size(300, 200);
            this.FormClosing += MainForm_FormClosing;

            Button btnOptimize = new Button { Text = "Optimize", Dock = DockStyle.Top };
            btnOptimize.Click += BtnOptimize_Click;

            Button btnRestore = new Button { Text = "Restore Defaults", Dock = DockStyle.Top };
            btnRestore.Click += BtnRestore_Click;

            this.Controls.Add(btnRestore);
            this.Controls.Add(btnOptimize);

            processDetectionTimer = new Timer();
            processDetectionTimer.Interval = CheckInterval;
            processDetectionTimer.Tick += ProcessDetectionTimer_Tick;
        }

        private void StartProcessDetection()
        {
            processDetectionTimer.Start();
        }

        private void ProcessDetectionTimer_Tick(object sender, EventArgs e)
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();
            if (gameProcess != null)
            {
                // Update UI or status as needed
                this.Text = $"Left 4 Dead 2 Performance Patch - Running";
            }
            else
            {
                this.Text = $"Left 4 Dead 2 Performance Patch - Not Running";
            }
        }

        private void BtnOptimize_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Insert optimization logic here
                MessageBox.Show("Optimization applied to Left 4 Dead 2!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2 before optimizing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Insert logic to restore defaults here
                MessageBox.Show("Defaults restored for Left 4 Dead 2!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2 before restoring defaults.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processDetectionTimer.Stop();
            processDetectionTimer.Dispose();
        }
    }
}
```