using Octokit;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace koyus_wintools
{
    public partial class Main : Form
    {
        int version = 9;
        Process p;
        string temppath;
        bool mctdownloaded = false;
        bool adwdownloaded = false;
        bool rufusdownloaded = false;
        Downloading downloading = new Downloading();

        public Main()
        {
            temppath = GetRandomTempPath();
            InitializeComponent();
            this.Hide();
        }

        string GetRandomTempPath()
        {
            var tempDir = System.IO.Path.GetTempPath();

            string fullPath;
            do
            {
                var randomName = System.IO.Path.GetRandomFileName();
                fullPath = System.IO.Path.Combine(tempDir, randomName);
            }
            while (Directory.Exists(fullPath));

            return fullPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%\\system32\\WindowsPowerShell\\v1.0\\powershell.exe"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if (MessageBox.Show("Logout to desktop?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            timer2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("taskkill", "/IM explorer.exe /f");
            Thread.Sleep(500);
            p = Process.Start("explorer.exe", Path.Combine(temppath + "\\koyu's WinTools"));
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                try
                {
                    Directory.Delete(temppath, true);
                }
                catch { }
                try
                {
                    Directory.Delete(temppath);
                }
                catch { }
                Process.Start("taskkill", "/IM explorer.exe /f");
                Process.Start("taskkill", "/IM iexplore.exe /f");
                Process.Start("taskkill", "/IM koyu.space.exe /f");
                Process.Start("taskkill", "/IM powershell.exe /f");
                Thread.Sleep(500);
                string explorer = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
                Process process = new Process();
                process.StartInfo.FileName = explorer;
                process.StartInfo.UseShellExecute = true;
                process.Start();
                Environment.Exit(0);
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!mctdownloaded)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("https://go.microsoft.com/fwlink/?LinkId=691209");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback2);
                client.DownloadFileAsync(uri, Path.Combine(temppath + "\\MediaCreationTool.exe"));
            }
            else
            {
                try
                {
                    Process.Start(Path.Combine(temppath + "\\MediaCreationTool.exe"));
                }
                catch { }
            }
        }

        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                mctdownloaded = true;
                progressBar2.Value = 100;
                Process.Start(Path.Combine(temppath + "\\MediaCreationTool.exe"));
            }
            catch { }
        }

        private void DownloadProgressCallback2(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm");
            try
            {
                new WebClient().DownloadString("https://koyu.space");
                this.Show();
            }
            catch
            {
                this.Hide();
                label1.Enabled = true;
            }
        }

        void ChangeVisibility(bool visible)
        {
            foreach (Control c in this.Controls)
            {
                c.Visible = visible;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("regedit.exe");
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!adwdownloaded)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("https://download.toolslib.net/download/direct/1/latest?channel=release");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed4);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
                client.DownloadFileAsync(uri, Path.Combine(temppath + "\\adwcleaner.exe"));
            }
            else
            {
                try
                {
                    Process.Start(Path.Combine(temppath + "\\adwcleaner.exe"));
                }
                catch { }
            }
        }

        private void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar4.Value = e.ProgressPercentage;
        }

        private void Completed4(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                adwdownloaded = true;
                progressBar4.Value = 100;
                Process.Start(Path.Combine(temppath + "\\adwcleaner.exe"));
            }
            catch { }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start("taskmgr.exe");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!rufusdownloaded)
            {
                Octokit.ProductHeaderValue productInformation = new Octokit.ProductHeaderValue("koyuswintools");
                GitHubClient ghclient = new GitHubClient(productInformation);
                var releases = ghclient.Repository.Release.GetAll("pbatard", "rufus").Result;
                string latestrufus = releases[0].TagName.Replace("v", "");
                WebClient client = new WebClient();
                Uri uri = new Uri($"https://github.com/pbatard/rufus/releases/download/v{latestrufus}/rufus-{latestrufus}.exe");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed5);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback5);
                client.DownloadFileAsync(uri, Path.Combine(temppath + "\\rufus.exe"));
            }
            else
            {
                try
                {
                    Process.Start(Path.Combine(temppath + "\\rufus.exe"));
                }
                catch { }
            }
        }

        private void DownloadProgressCallback5(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar5.Value = e.ProgressPercentage;
        }

        private void Completed5(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                rufusdownloaded = true;
                progressBar5.Value = 100;
                Process.Start(Path.Combine(temppath + "\\rufus.exe"));
            }
            catch { }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height - this.Height);
            this.WindowState = FormWindowState.Minimized;
            label1.Text = DateTime.Now.ToString("HH:mm");
            try
            {
                int remoteversion = Convert.ToInt32(new WebClient().DownloadString("https://updates.koyu.space/wintools/latest").Split('\n')[0]);
                if (remoteversion == version)
                {
                    // Show download window
                    downloading.Show();
                    // Download the WinTools
                    Directory.CreateDirectory(temppath);
                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed6);
                    client.DownloadFileAsync(new Uri("https://updates.koyu.space/wintools/wintools.zip"), Path.Combine(temppath + "\\wintools.zip"));
                }
                else
                {
                    downloading.Close();
                    if (MessageBox.Show("New version available. Download now?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("https://updates.koyu.space/wintools/wintools.exe") { UseShellExecute = true });
                    }
                    Environment.Exit(0);
                }
            }
            catch
            {
                downloading.Close();
                MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void Completed6(object sender, AsyncCompletedEventArgs e)
        {
            // Extract files
            ZipFile.ExtractToDirectory(Path.Combine(temppath + "\\wintools.zip"), temppath);
            // Close download window
            downloading.Close();
            //Ask the user before running WinTools
            if (MessageBox.Show("The WinTools are now downloaded. Do you want to open them?\n\nWarning: This will kill all processes and enter the WinTools mode.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Kill explorer so we get a clean working environment
                Process.Start("taskkill", "/IM explorer.exe /f");
                // Also kill every browser as the user may have downloaded the file from the internet
                Process.Start("taskkill", "/IM iexplore.exe /f");
                Process.Start("taskkill", "/IM firefox.exe /f");
                Process.Start("taskkill", "/IM chrome.exe /f");
                Thread.Sleep(500);
                this.WindowState = FormWindowState.Maximized;
                ChangeVisibility(false);
                this.Visible = true;
                Thread.Sleep(1000);
                ChangeVisibility(true);
                timer2.Enabled = true;
            }
            else
            {
                // Clean up if the user says no
                try
                {
                    Directory.Delete(temppath, true);
                }
                catch { }
                try
                {
                    Directory.Delete(temppath);
                }
                catch { }
                Environment.Exit(0);
            }
        }
    }
}
