using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerShellGui
{
    class PsExec
    {
        Process PsProcess = new Process();
        string PsExecPath = @"C:\Windows\Buhler\SwInfo\PsExec.exe";
        string command;

        public PsExec(string command)
        {
            this.command = command;
            StartPsProcess();
        }

        private void StartPsProcess()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = PsExecPath;
            p.StartInfo.Arguments = command;
            p.Start();
            //p.WaitForExit();
            //int ExitCode = p.ExitCode;
            //return ExitCode;
        }
    }
}
