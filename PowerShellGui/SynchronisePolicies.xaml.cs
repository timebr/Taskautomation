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
            ComputerList = new List<Computer>();
            foreach (String ComputerName in NodeArray)
            {
                Computer Computer = new Computer(ComputerName);
                ComputerList.Add(Computer);
            }
            ComputerView.ItemsSource = ComputerList;
        }

        public IList<Computer> ComputerList { get; set; }
    }
}
