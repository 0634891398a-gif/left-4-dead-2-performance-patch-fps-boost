```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Timer processCheckTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            SetupTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.Size = new System.Drawing.Size(300, 200);
            var patchButton = new Button
            {
                Text = "Apply Performance Patch",
                Width = 200,
                Height = 30,
                Location = new System.Drawing.Point(50, 50)
            };
            patchButton.Click += PatchButton_Click;
            this.Controls.Add(patchButton);
            this.FormClosing += MainForm_FormClosing;
        }

        private void SetupTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
        }

        private async void PatchButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Left 4 Dead 2 is not running. Please start the game first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            await ApplyPerformancePatch();
        }

        private async Task ApplyPerformancePatch()
        {
            // Simulate patching process
            MessageBox.Show("Applying performance patch...", "Please wait", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await Task.Delay(2000); // Simulate time taken to apply the patch
            MessageBox.Show("Performance patch applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }
    }
}
```