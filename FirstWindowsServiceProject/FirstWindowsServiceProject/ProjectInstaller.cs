using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWindowsServiceProject
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            //now lets set the properties of the objects

            //this will make the service to not require a username and password
            //upon installation of the service
            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;

            //now lets set the properties of the service that will be visible on
            //services.msc or taskmanager
            serviceInstaller1.ServiceName = "FirstWindowsService";
            serviceInstaller1.Description = "This is our first windows service project.";
            serviceInstaller1.DisplayName = "First Windows Service";
        }
        //It works
    }
}
