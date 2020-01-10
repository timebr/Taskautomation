using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Threading;

namespace PowerShellGui.SubControls
{
    /// <summary>
    /// Interaction logic for ComputerDashboard.xaml
    /// </summary>
    public partial class ComputerDashboard : UserControl
    {
        BackgroundWorker loadingWorker;
        bool loading = true;
        int selectedBar = 2;
        int step = 2;
        int margin1 = 0;
        int margin2 = 0;
        int width1 = 0;
        int width2 = 0;
        public ComputerDashboard()
        {
            InitializeComponent();
            loadingWorker = new BackgroundWorker();
            loadingWorker.WorkerReportsProgress = true;
            loadingWorker.WorkerSupportsCancellation = true;

            loadingWorker.DoWork += new DoWorkEventHandler(loadingWorker_DoWork);
            loadingWorker.ProgressChanged += new ProgressChangedEventHandler(loadingWorker_ProgressChanged);
            loadingWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadingWorker_RunWorkerCompleted);
            loadingWorker.RunWorkerAsync();
        }

        void loadingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int step = this.step;
            int percentFinished = 818;
            while (this.loading)
            {
                if(percentFinished == 1022)
                {
                    if (selectedBar == 1)
                    {
                        selectedBar = 2;
                    }
                    else
                    {
                        selectedBar = 1;
                    }
                    percentFinished = 0;
                }
                percentFinished += step;
                loadingWorker.ReportProgress(percentFinished);
                Thread.Sleep(5);
            }
            e.Result = percentFinished;
        }

        void loadingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(this.selectedBar == 1)
            {
                if (e.ProgressPercentage >= 820)
                {
                    if (this.width1 > 0)
                        this.width1 = 1020 - e.ProgressPercentage;
                    if (this.width2 < 200)
                        this.width2 = 200 - (1020 - e.ProgressPercentage);
                }
                this.margin1 = e.ProgressPercentage;
                this.margin2 = 0;
            }
            else
            {
                if (e.ProgressPercentage >= 820)
                {
                    if (this.width2 > 0)
                        this.width2 = 1020 - e.ProgressPercentage;
                    if (this.width1 < 200)
                        this.width1 = 200 - (1020 - e.ProgressPercentage);
                }
                this.margin2 = e.ProgressPercentage;
                this.margin1 = 0;

            }
            MainWindow.AppWindow.changeLoadingBar(this.width1, this.width2, this.margin1, this.margin2);
        }

        void loadingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingBar1.Visibility = Visibility.Collapsed;
            LoadingBar2.Visibility = Visibility.Collapsed;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            this.loading = false;
        }
    }
}
