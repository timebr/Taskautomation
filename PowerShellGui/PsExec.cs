using System.Diagnostics;

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
