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
            //string policySyncPath = @"C:\Program Files (x86)\LANDesk\LDClient\PolicySync.exe";
            foreach (String ComputerName in NodeArray)
            {
                ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\Buhler\SwInfo\PsExec.exe", @"-accepteula -d -i \\hw-wop-119100 cmd /c notepad.exe");
                info.UseShellExecute = false;
                Process p = Process.Start(info);
            }
            ComputerView.ItemsSource = ComputerList;

        }
    }
}
