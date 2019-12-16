using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class FileTransfer : UserControl
    {

        public FileTransfer()
        {
            InitializeComponent();
        }

        private void SwitchComputerButton_Click(object sender, RoutedEventArgs e)
        {
            //Get current values
            string textRoot = RootPC.Text;
            string textDestination = DestinationPC.Text;
            string textRootPath = RootPath.Text;
            string textDestinationPath = DestinationPath.Text;

            RootPC.Text = textDestination;
            DestinationPC.Text = textRoot;
            RootPath.Text = textDestinationPath;
            DestinationPath.Text = textRootPath;
        
        }

        private void Clear_all_Button_Click(object sender, RoutedEventArgs e)
        {
            RootPC.Clear();
            DestinationPC.Clear();
            DestinationPath.Text= "";
            RootPath.Text = "";
        }
    }
}
