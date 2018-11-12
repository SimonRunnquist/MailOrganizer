﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace KevinsMailOrganizer
{
    class PS_Controller
    {
        public void GetFolderStructure(string srcFolder, string destFolder) {
            using (PowerShell powerInstance = PowerShell.Create()) {
                string script = @"Copy-Item -Path " + srcFolder + " -Destination " + destFolder + " -recurse -Force";
                Console.WriteLine(script);
                powerInstance.AddScript(script);
                powerInstance.Invoke();
            }
        }
    }
}
