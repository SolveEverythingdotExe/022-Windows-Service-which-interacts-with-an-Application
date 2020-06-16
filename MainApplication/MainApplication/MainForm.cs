using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        //now lets create our list of commands available
        private enum ServiceCommands
        {
            ApplicationStarted = 128,
            ApplicationClosed = 129
        };

        public MainForm()
        {
            InitializeComponent();
        }

        //now lets add our events
        private void MainForm_Load(object sender, EventArgs e)
        {
            //make sure service is running first
            StartService();

            //now lets execute the command on to our service
            serviceController1.ExecuteCommand((int)ServiceCommands.ApplicationStarted);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartService();

            //also upon closing of our form
            serviceController1.ExecuteCommand((int)ServiceCommands.ApplicationClosed);
        }

        //lets create a method that will ensure that our service is started first before executing our commands
        private void StartService()
        {
            //lets define our service that we want to connect
            serviceController1.ServiceName = "FirstWindowsService";

            //BUGFIX ========> we will execute the starting of the windows service in a separate thread so that it will not mess
            //                 up with the main/UI thread and also on the System.Threading.Thread.Sleep(1000) below
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (obj, se) =>
            {
                if (serviceController1.Status != ServiceControllerStatus.Running)
                    serviceController1.Start();
            };
            worker.RunWorkerAsync();

            //BUGFIX ========> starting of a service is quite delay so we need to add some delay after starting it
            System.Threading.Thread.Sleep(1000);
        }

        //It works!
    }
}
