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
    /// <summary>
    /// Interaction logic for SynchronicePolicies.xaml
    /// </summary>
    public partial class SynchronisePolicies : UserControl
    {
        public SynchronisePolicies()
        {
            InitializeComponent();
        }

        private void ButtonSynchronise_Click(object sender, RoutedEventArgs e)
        {
            string[] NodeArray = ComputerName.Text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            List<Computer> ComputerList = new List<Computer>();
            foreach (String ComputerName in NodeArray)
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.FileName = @"C:\Program Files (x86)\LANDesk\LDClient\PolicySync.exe";
                p.StartInfo.Arguments = @"\\200.200.20.200:5555 -u xyz -p abc123 -i -w D:\Selenium java -jar selenium-server-standalone.jar -role node -hub http://100.100.10.100:4444/grid/register";
                p.Start();
            }
            ComputerView.ItemsSource = ComputerList;

        }
    }
}
