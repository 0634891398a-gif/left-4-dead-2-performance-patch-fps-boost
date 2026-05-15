```csharp
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string ProcessName = "left4dead2";
        private Timer _processCheckTimer;
        private Button _applyPatchButton;
        private Label _statusLabel;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            StartProcessCheck();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.Size = new Size(400, 200);

            _applyPatchButton = new Button
            {
                Text = "Apply Patch",
                Location = new Point(150, 100),
                Enabled = false
            };
            _applyPatchButton.Click += ApplyPatchButton_Click;

            _statusLabel = new Label
            {
                Location = new Point(50, 30),
                Size = new Size(300, 20)
            };

            this.Controls.Add(_applyPatchButton);
            this.Controls.Add(_statusLabel);
        }

        private void StartProcessCheck()
        {
            _processCheckTimer = new Timer { Interval = 1000 };
            _processCheckTimer.Tick += ProcessCheckTimer_Tick;
            _processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            var runningProcesses = Process.GetProcessesByName(ProcessName);
            if (runningProcesses.Any())
            {
                _applyPatchButton.Enabled = true;
                _statusLabel.Text = "Game Running: Ready to Apply Patch.";
            }
            else
            {
                _applyPatchButton.Enabled = false;
                _statusLabel.Text = "Game Not Running: Patch Disabled.";
            }
        }

        private void ApplyPatchButton_Click(object sender, EventArgs e)
        {
            // Simulated patch application logic
            ApplyPerformancePatch();
        }

        private void ApplyPerformancePatch()
        {
            try
            {
                // Perform the patch operation
                // e.g., Adjust graphics settings, modify config files, etc.

                MessageBox.Show("Performance patch applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying patch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```