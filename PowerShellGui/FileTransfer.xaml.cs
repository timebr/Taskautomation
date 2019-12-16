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
        private object fileList;

        public FileTransfer()
        {
            InitializeComponent();
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
