```csharp
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string gameProcessName = "Left4Dead2";
        private const string patchFilePath = @"C:\Path\To\Your\PatchFile.dll"; // Update with actual patch file path
        private Process gameProcess;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.ClientSize = new System.Drawing.Size(400, 200);
            Button applyPatchButton = new Button()
            {
                Text = "Apply Patch",
                Location = new System.Drawing.Point(150, 80),
                Size = new System.Drawing.Size(100, 30)
            };
            applyPatchButton.Click += ApplyPatchButton_Click;
            this.Controls.Add(applyPatchButton);
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeProcessCheckTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            gameProcess = GetRunningGameProcess();
            if (gameProcess != null)
            {
                processCheckTimer.Stop(); // Stop checking if the game is running
                MessageBox.Show("Game detected! You can now apply the patch.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Process GetRunningGameProcess()
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals(gameProcessName, StringComparison.OrdinalIgnoreCase))
                {
                    return process;
                }
            }
            return null;
        }

        private void ApplyPatchButton_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                TryApplyingPatch();
            }
            else
            {
                MessageBox.Show("The game is not running. Start Left 4 Dead 2 to apply the patch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryApplyingPatch()
        {
            // Implement actual patch application logic here
            // This is a placeholder message for demonstration purposes.
            MessageBox.Show("Patch applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }
    }
}
```