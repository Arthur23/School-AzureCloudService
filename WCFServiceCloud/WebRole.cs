using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WCFServiceCloud
{
    public class WebRole : RoleEntryPoint
    {
        // Our Global variables !
        ServiceCloud SmallBoxService;

        public override bool OnStart()
        {
            // Init our Service - The constructor should be called to access our container
            try
            {
                // Try to access our Service class
                SmallBoxService = new ServiceCloud();
                SmallBoxService.CreateDirectoryStruct();
            }
            catch (Exception)
            {

                throw;
            }
            // Now we have access to our service methods !
            Trace.TraceInformation("Our service seems to be started ! Enjoy");

            RoleEnvironment.Changing += RoleEnvironmentChanging;

            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }

    }
}
