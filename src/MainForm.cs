```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        // Game-specific fields
        private const string GameProcessName = "left4dead2";
        private bool isGameRunning = false;
        private Timer processCheckTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Initialize and configure the Timer
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();

            // Add event handlers for buttons
            btnApplyPatch.Click += BtnApplyPatch_Click;
            btnRemovePatch.Click += BtnRemovePatch_Click;
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            UpdateUI();
        }

        private void UpdateUI()
        {
            lblStatus.Text = isGameRunning ? "Game is running" : "Game is not running";
            btnApplyPatch.Enabled = !isGameRunning;
            btnRemovePatch.Enabled = isGameRunning;
        }

        private async void BtnApplyPatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to apply the performance patch?", "Apply Patch", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await Task.Run(() => ApplyPatch());
                MessageBox.Show("Performance patch applied successfully!", "Success", MessageBoxButtons.OK);
            }
        }

        private async void BtnRemovePatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove the performance patch?", "Remove Patch", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await Task.Run(() => RemovePatch());
                MessageBox.Show("Performance patch removed successfully!", "Success", MessageBoxButtons.OK);
            }
        }

        private void ApplyPatch()
        {
            // Implementation of patch application
            // This can involve file manipulations, registry edits, etc.
        }

        private void RemovePatch()
        {
            // Implementation of patch removal
            // This can involve reverting changes made by ApplyPatch
        }
    }
}
```