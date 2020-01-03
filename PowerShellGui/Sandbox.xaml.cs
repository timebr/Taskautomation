using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for Sandbox.xaml
    /// </summary>
    public partial class Sandbox : UserControl
    {


        public Sandbox()
        {
            InitializeComponent();
    
        }
        private void goNavigateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get URI to navigate to  
            Uri uri = new Uri(this.addressTextBox.Text, UriKind.RelativeOrAbsolute);

            // Only absolute URIs can be navigated to  
            if (!uri.IsAbsoluteUri)
            {
                MessageBox.Show("The Address URI must be absolute. For example, 'http://www.microsoft.com'");
                return;
            }

            // Navigate to the desired URL by calling the .Navigate method  
            this.myWebBrowser.Navigate(uri);
        }

    }

}
