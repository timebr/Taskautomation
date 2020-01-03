using System.Diagnostics;

namespace PowerShellGui
{
    class PsExec
    {
        Process PsProcess = new Process();
        string PsExecPath = @"C:\Windows\Buhler\SwInfo\PsExec.exe";
        string command;
        bool waitForFinish;

        public PsExec(string command, bool waitForFinish)
        {
            this.command = command;
            this.waitForFinish = waitForFinish;
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
            if (waitForFinish)
            {
                p.WaitForExit();
                //int ExitCode = p.ExitCode;
                //return ExitCode;
            }

        }
    }
}
