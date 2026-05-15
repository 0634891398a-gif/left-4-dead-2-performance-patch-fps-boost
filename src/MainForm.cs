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
        private Timer processCheckTimer;
        private const string l4d2ProcessName = "left4dead2";
        private const string patchStatus = "Performance patch applied!";
        private bool patchApplied = false;

        public MainForm()
        {
            InitializeComponent();
            SetupTimer();
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
            var isL4D2Running = Process.GetProcessesByName(l4d2ProcessName).Any();
            if (isL4D2Running && !patchApplied)
            {
                ApplyPatch();
            }
            else if (!isL4D2Running && patchApplied)
            {
                RemovePatch();
            }
        }

        private void ApplyPatch()
        {
            // Logic to apply the performance patch
            MessageBox.Show(patchStatus, "Patch Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            patchApplied = true;
        }

        private void RemovePatch()
        {
            // Logic to remove the performance patch
            MessageBox.Show("Performance patch removed!", "Patch Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            patchApplied = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Name = "MainForm";
            this.Text = "Left 4 Dead 2 Performance Patch";
            this.ResumeLayout(false);
        }
    }
}
```