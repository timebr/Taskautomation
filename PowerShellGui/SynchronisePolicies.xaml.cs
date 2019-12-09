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
            foreach (string NodeName in NodeArray)
            {
                Computer Node = new Computer(NodeName);
                ComputerList.Add(Node);
            }
            //string policySyncPath = @"C:\Program Files (x86)\LANDesk\LDClient\PolicySync.exe";
            foreach (Computer Computer in ComputerList)
            {
                string NodeName = Computer.GetComputerName();
                bool Availability = Computer.GetAvailability();
                if (Availability)
                {
                    ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\Buhler\SwInfo\PsExec.exe", @"-accepteula \\" + NodeName + " cmd /c \"C:\\\\Program Files (x86)\\LANDesk\\LDClient\\PolicySync.exe\"");
                    info.UseShellExecute = false;
                    info.CreateNoWindow = true;
                    Process p = Process.Start(info);
                }
            }
            ComputerView.ItemsSource = ComputerList;
        }
    }
}
