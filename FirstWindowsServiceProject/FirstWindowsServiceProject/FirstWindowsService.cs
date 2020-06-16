using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FirstWindowsServiceProject
{
    public partial class FirstWindowsService : ServiceBase
    {
        //In this tutorial we will write to the event log if the application has been started or closed
        //First lets define the list of command
        //Accepted value is between 128 and 256 for passing of value between application and service
        private enum ServiceCommands
        {
            ApplicationStarted = 128,
            ApplicationClosed = 129
        };

        public FirstWindowsService()
        {
            InitializeComponent();

            //now lets set the properties of the eventlog

            //this will be the subcategory of the log on Event Viewer > Windows Log
            eventLog1.Log = "Application";

            //this will be the name that will appear on "Source" column in Event Viewer
            eventLog1.Source = "FirstWindowsService";
        }

        //now lets write on the log once the service is started
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("The user starts the service.", EventLogEntryType.Information);
        }

        //also once it is stopped
        protected override void OnStop()
        {
            eventLog1.WriteEntry("The user stops the service.", EventLogEntryType.Information);
        }

        //now lets handle the command
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);

            //lets execute command based on value passed by our application
            if (command == (int)ServiceCommands.ApplicationStarted)
                eventLog1.WriteEntry("The application has been started.", EventLogEntryType.Information);
            else if (command == (int)ServiceCommands.ApplicationClosed)
                eventLog1.WriteEntry("The application has been closed", EventLogEntryType.Information);
        }
    }
}
