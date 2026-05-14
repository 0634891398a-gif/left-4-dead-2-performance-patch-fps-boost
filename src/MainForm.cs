```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace L4D2PerformancePatch
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string GameProcessName = "Left4Dead2"; // Game executable name
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Initialize Timer
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();

            // Initialize Buttons
            Button applyPatchButton = new Button
            {
                Text = "Apply Performance Patch",
                Location = new System.Drawing.Point(10, 10)
            };
            applyPatchButton.Click += ApplyPatchButton_Click;

            Button removePatchButton = new Button
            {
                Text = "Remove Performance Patch",
                Location = new System.Drawing.Point(10, 50)
            };
            removePatchButton.Click += RemovePatchButton_Click;

            Controls.Add(applyPatchButton);
            Controls.Add(removePatchButton);
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            UpdateUIState();
        }

        private void UpdateUIState()
        {
            // Enable/Disable buttons based on game state
            foreach (Control control in Controls)
            {
                control.Enabled = isGameRunning;
            }
        }

        private void ApplyPatchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Implement your patch application logic here
                MessageBox.Show("Performance patch applied!");
            }
            else
            {
                MessageBox.Show("Left 4 Dead 2 is not running.");
            }
        }

        private void RemovePatchButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Implement your patch removal logic here
                MessageBox.Show("Performance patch removed!");
            }
            else
            {
                MessageBox.Show("Left 4 Dead 2 is not running.");
            }
        }
    }
}
```