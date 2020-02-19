using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading;
using System.ComponentModel;
using System.Drawing;

namespace koyus_wintools
{
    public partial class Main : Form
    {
        int version = 0;
        Process p;
        string temppath;
        bool koyuspaceinstalled = false;

        public Main()
        {
            InitializeComponent();
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height - this.Height);
            this.TopMost = true;
            this.WindowState = FormWindowState.Minimized;
            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\koyu.space")))
            {
                koyuspaceinstalled = true;
                progressBar1.Value = 100;
            }
            try
            {
                int remoteversion = Convert.ToInt32(new WebClient().DownloadString("https://updates.koyu.space/wintools/latest").Split('\n')[0]);
                if (remoteversion == version)
                {
                    // Download the WinTools
                    temppath = GetRandomTempPath();
                    Directory.CreateDirectory(temppath);
                    new WebClient().DownloadFile("https://updates.koyu.space/wintools/wintools.zip", Path.Combine(temppath + "\\wintools.zip"));
                    ZipFile.ExtractToDirectory(Path.Combine(temppath + "\\wintools.zip"), temppath);
                    //Ask the user before running WinTools
                    if (MessageBox.Show("The WinTools are now downloaded. Do you want to open them?\nWarning: This will kill all process and enter the WinTools mode.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        this.WindowState = FormWindowState.Normal;
                        // Kill explorer so we get a clean working environment
                        Process.Start("taskkill", "/IM explorer.exe /f");
                        // Also kill every browser as the user may have downloaded the file from the internet
                        Process.Start("taskkill", "/IM iexplore.exe /f");
                        Process.Start("taskkill", "/IM firefox.exe /f");
                        Process.Start("taskkill", "/IM chrome.exe /f");
                        Thread.Sleep(500);
                        p = Process.Start("explorer.exe", Path.Combine(temppath + "\\koyu's WinTools"));
                        Thread.Sleep(500);
                    } else
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
                else
                {
                    if (MessageBox.Show("New version available. Download now?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("https://updates.koyu.space/wintools/wintools.exe");
                        Environment.Exit(0);
                    }
                }
            }
            catch
            {
                MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (!koyuspaceinstalled)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("https://koyu.keybase.pub/desktop.exe");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                client.DownloadFileAsync(uri, Path.Combine(temppath + "\\desktop.exe"));
            }
            else
            {
                try
                {
                    Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Programs\\koyuspace-desktop\\koyu.space.exe"));
                }
                catch
                {
                    Console.WriteLine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Programs\\koyuspace-desktop\\koyu.space.exe"));
                }
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Process.Start(Path.Combine(temppath + "\\desktop.exe"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("iexplore.exe", "https://duckduckgo.com/html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%\\system32\\WindowsPowerShell\\v1.0\\powershell.exe"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
            } catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("https://go.microsoft.com/fwlink/?LinkId=691209");
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback2);
            client.DownloadFileAsync(uri, Path.Combine(temppath + "\\MediaCreationTool.exe"));
        }

        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            Process.Start(Path.Combine(temppath + "\\MediaCreationTool.exe"));
        }

        private void DownloadProgressCallback2(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void button7_Click(object sender, EventArgs e)
        {
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
            Thread.Sleep(500);
            Process.Start("shutdown", "/r /t 0");
            Environment.Exit(0);
        }
    }
}
