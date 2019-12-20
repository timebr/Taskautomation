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
using System.Threading;
using System.ComponentModel;

namespace PowerShellGui
{
    /// <summary>
    /// Interaction logic for SynchronicePolicies.xaml
    /// </summary>
    public partial class SynchronisePolicies : UserControl
    {
        string[] nodeArray;
        List<Computer> computerList = new List<Computer>();
        BackgroundWorker worker;
        public SynchronisePolicies()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            
            
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int nodeProgress = 100 / this.nodeArray.Count();
            int percentFinished = 0;
            foreach (string nodeName in this.nodeArray)
            {
                Computer Node = new Computer(nodeName);
                computerList.Add(Node);
                if (Node.GetAvailability())
                {
                    string Command = @"-accepteula \\" + nodeName + " -s cmd /c \"C:\\\\Program Files (x86)\\LANDesk\\LDClient\\PolicySync.exe\"";
                    PsExec Sync = new PsExec(Command, false);
                }
                percentFinished += nodeProgress;
                worker.ReportProgress(percentFinished);
            }
            e.Result = percentFinished;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainWindow.AppWindow.changeProgressBar(e.ProgressPercentage);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MainWindow.AppWindow.changeProgressBar(100);
            ComputerView.ItemsSource = computerList;
        }

        private void ButtonSynchronise_Click(object sender, RoutedEventArgs e)
        {
            //Create Array with all Node Names
            
            string[] tempNodeArray = ComputerName.Text.Split(Environment.NewLine.ToCharArray()).ToArray();
            List<Computer> ComputerList = new List<Computer>();
            this.nodeArray = new string[] { };
            foreach (string NodeName in tempNodeArray)
            {
                string[] tempArray2 = NodeName.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                string[] tempArray1 = this.nodeArray;
                this.nodeArray = new string[tempArray2.Length + tempArray1.Length];
                tempArray1.CopyTo(this.nodeArray, 0);
                tempArray2.CopyTo(this.nodeArray, tempArray1.Length);
            }
            this.computerList.Clear();
            worker.RunWorkerAsync();

            /*foreach (string NodeName in this.nodeArray)
            {
                Computer Node = new Computer(NodeName);
                ComputerList.Add(Node);
            }
            
            //Sync Policies of each available Computer
            foreach (Computer Computer in ComputerList)
            {
                string NodeName = Computer.GetComputerName();
                bool Availability = Computer.GetAvailability();
                if (Availability)
                {
                    string Command = @"-accepteula \\" + NodeName + " -s cmd /c \"C:\\\\Program Files (x86)\\LANDesk\\LDClient\\PolicySync.exe\"";
                    PsExec Sync = new PsExec(Command, false);
                }
            }
            ComputerView.ItemsSource = ComputerList;*/
        }
    }
}
