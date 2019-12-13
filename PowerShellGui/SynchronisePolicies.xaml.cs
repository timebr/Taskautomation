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
            string[] tempNodeArray = ComputerName.Text.Split(Environment.NewLine.ToCharArray()).ToArray();
            List<Computer> ComputerList = new List<Computer>();
            string[] NodeArray = new string[] { };
            foreach (string NodeName in tempNodeArray)
            {
                string[] tempArray2 = NodeName.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                string[] tempArray1 = NodeArray;
                NodeArray = new string[tempArray2.Length + tempArray1.Length];
                tempArray1.CopyTo(NodeArray, 0);
                tempArray2.CopyTo(NodeArray, tempArray1.Length);
            }
            foreach (string NodeName in NodeArray)
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
            ComputerView.ItemsSource = ComputerList;
        }
    }
}
