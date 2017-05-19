using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;
        }

        public static int exeCommandInCmd() {
            DirectoryInfo di = new DirectoryInfo(@"C:\Program Files\Oracle\VirtualBox");
            FileInfo[] DDFiles = di.GetFiles("*.dd");
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
               RedirectStandardError = true,
               RedirectStandardOutput = true,
               UseShellExecute = false,
               CreateNoWindow = true,
               FileName = "cmd.exe",
               WorkingDirectory = @"C:\Program Files\Oracle\VirtualBox",
               Arguments = "/user:Administrator cmd /C " + "VBoxManage convertfromraw "+ DDFiles[0]+" Si_image.vmdk --format VMDK"

            };
   
            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo = startInfo;
                proc.Start();
                proc.Refresh();
                int done = 0;
                proc.WaitForExit();
                return 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int output = exeCommandInCmd();
            if(output == 1)
            Cursor = Cursors.Arrow;
            MessageBox.Show("Successfully created a file");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Program Files\Oracle\VirtualBox");
            FileInfo[] DDFiles = di.GetFiles("*.dd");
            if(DDFiles.Length == 0)
            {
                MessageBox.Show("No .dd files present. Please set .dd file in your VirtualBox directory.");
            }
            else
            {
              
                MessageBox.Show("There is a file to convert");
                button1.Visible = true;

            }

        }
    }
}
