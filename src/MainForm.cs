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
        private const string GameProcessName = "left4dead2";
        private Timer processCheckTimer;
        private Button patchButton;
        private Label statusLabel;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Text = "L4D2 Performance Patch";
        }

        private void InitializeCustomComponents()
        {
            patchButton = new Button
            {
                Text = "Patch Game",
                Dock = DockStyle.Fill
            };
            patchButton.Click += PatchButton_Click;

            statusLabel = new Label
            {
                Text = "Waiting for game to start...",
                Dock = DockStyle.Bottom,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.Controls.Add(patchButton);
            this.Controls.Add(statusLabel);

            processCheckTimer = new Timer { Interval = 1000 };
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private async void PatchButton_Click(object sender, EventArgs e)
        {
            if (IsGameRunning())
            {
                statusLabel.Text = "Patching...";
                await Task.Run(() => ApplyPerformancePatch());
                statusLabel.Text = "Patch Applied Successfully!";
            }
            else
            {
                statusLabel.Text = "Game is not running. Please start L4D2.";
            }
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!IsGameRunning())
            {
                statusLabel.Text = "Waiting for game to start...";
            }
        }

        private bool IsGameRunning()
        {
            return Process.GetProcessesByName(GameProcessName).Any();
        }

        private void ApplyPerformancePatch()
        {
            // Here, you would include the logic to apply the performance patch 
            // (e.g., modifying config files, modifying the memory, etc.).
            System.Threading.Thread.Sleep(2000); // Simulate patching delay
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
```