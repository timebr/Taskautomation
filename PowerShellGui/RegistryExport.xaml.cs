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
using System.IO;
using System.Reflection;

namespace PowerShellGui
{
    /// <summary>
    /// Interaction logic for RegistryExport.xaml
    /// </summary>
    public partial class RegistryExport : UserControl
    {

        public RegistryExport()
        {
            InitializeComponent();
        }

        private void ButtonOpenEditor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            Computer node = new Computer(TargetPC.Text);
            string selectedKey = RegistryKey.Text;
            string selectedSavePath = SavePath.Text;
            string savePath;
            var keys = new List<string>();
            switch (selectedKey)
            {
                case "Uninstall x64":
                    keys.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    break;

                case "Uninstall x32":
                    keys.Add(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                    break;

                case "Uninstall":
                    keys.Add(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                    keys.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    break;

                default:
                    break;
            }

            switch (selectedSavePath)
            {
                case "Temp":
                    savePath = @"C:\temp\export.log";
                    break;

                default:
                    savePath = SavePath.Text + "export.log";
                    break;
            }

            string remotefile = @"\\" + node.GetComputerName() + @"\c$\temp\regexport.log";
            int i = 0;
            foreach (string key in keys)
            {
                string query = @"\\" + node.GetComputerName() + @"\HKLM\" + key;
                if (i == 1)
                {
                    PsExec exportCommand = new PsExec(@"-accepteula \\" + node.GetComputerName() + "Powershell reg query " + query + " /s | Out-File -FilePath " + remotefile + " -Append", true);
                }
                else
                {
                    PsExec exportCommand = new PsExec(@"-accepteula \\" + node.GetComputerName() + " Powershell reg query " + query + " /s | Out-File -FilePath " + remotefile, true);
                }
                i = 1;
            }
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.Copy(remotefile, savePath);
            File.Delete(@"\\" + node.GetComputerName() + @"\c$\temp\regexport.log");
            RegistryExportTextBox.Document.PageWidth = 1000;
            
            //Load File in to RichTextBox
            TextRange range = new TextRange(RegistryExportTextBox.Document.ContentStart, RegistryExportTextBox.Document.ContentEnd);

            FileStream fStream = new FileStream(savePath, FileMode.OpenOrCreate);

            range.Load(fStream, DataFormats.Text);

            fStream.Close();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeleteFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
