namespace BTEAManual
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.ComponentModel;

    public partial class Main : Form
    {
        public static DateTime SelectedDate
        { get; private set; }

        public Main()
        {
            InitializeComponent();

            DateTime selectedDate;
            DateTime todaysDate;

            if (File.Exists("BTEAManual.ini"))
            {
                dateRecordDate.Value = DateTime.Today.AddDays(-1);
                dateRecordDate.Enabled = true;
                btnGetSendData.Enabled = true;

                pictureProgress.Image = Properties.Resources.InfoSmall;
                lblProgress.Text = "Ready to process the data :)";

                btnGetSendData.Click += (sender, eArgs) =>
                {
                    selectedDate = dateRecordDate.Value.Date;
                    todaysDate = DateTime.Now.Date;

                    if (selectedDate == todaysDate)
                    {
                        btnGetSendData.Enabled = false;
                        pictureProgress.Image = Properties.Resources.ErrorSmall;
                        lblProgress.Text = "The date must be less than today!";

                        return;
                    }

                    SelectedDate = selectedDate;
                    pictureProgress.Image = Properties.Resources.Progress;

                    dateRecordDate.Enabled = false;
                    btnGetSendData.Enabled = false;

                    DoWork();
                };

                dateRecordDate.ValueChanged += (sender, eArgs) =>
                {
                    selectedDate = dateRecordDate.Value.Date;
                    todaysDate = DateTime.Now.Date;

                    if (selectedDate >= todaysDate)
                    {
                        btnGetSendData.Enabled = false;
                        pictureProgress.Image = Properties.Resources.ErrorSmall;
                        lblProgress.Text = "The date must be less than today!";
                    }
                    else
                    {
                        btnGetSendData.Enabled = true;
                        pictureProgress.Image = Properties.Resources.InfoSmall;
                        lblProgress.Text = "Ready to process the data :)";
                    }
                };
            }
        }

        private void DoWork()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, eArgs) =>
            {
                lblProgress.SafeInvoke(l => l.Text = $"Getting data for Business Date {SelectedDate.ToString("dd/MM/yyyy")} ...");
                RoomData.Export();

                Upload();
            };
            worker.RunWorkerAsync();
        }

        private void Upload()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            string pscp = Path.GetTempPath() + "pscp.exe";
            File.WriteAllBytes(pscp, Properties.Resources.pscp);

            var psi = new ProcessStartInfo();
            psi.FileName = pscp;
            psi.Arguments = $"-P {Params.BTEAPort} -pw {Params.BTEAPass} -hostkey 4e:a5:1d:09:55:36:4b:3d:5d:03:1f:06:f1:05:23:c4 {RoomData.FileName} {Params.BTEAUser}@{Params.BTEAHostname}:/{Params.BTEAUploadDir}";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            var startUpload = Process.Start(psi);
            while (!startUpload.HasExited)
            {
                lblProgress.SafeInvoke(l => l.Text = $"Uploading {RoomData.FileName}...");
            };
            startUpload.WaitForExit();
            File.Delete(pscp);

            if (startUpload.ExitCode != 0)
            {
                pictureProgress.SafeInvoke(p => p.Image = Properties.Resources.ErrorSmall);
                lblProgress.SafeInvoke(l => l.Text = "Could not Upload the file!");
            }
            else
            {
                pictureProgress.SafeInvoke(p => p.Image = Properties.Resources.DoneSmall);
                lblProgress.SafeInvoke(l => l.Text = "File has been uploaded succesfully!");
            }
            dateRecordDate.SafeInvoke(d => d.Enabled = true);
            btnGetSendData.SafeInvoke(b => b.Enabled = true);
        }
    }
}
