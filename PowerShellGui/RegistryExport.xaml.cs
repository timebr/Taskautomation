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
using System.Text.RegularExpressions;

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
            TextRange textRange = new TextRange(RegistryExportTextBox.Document.ContentStart, RegistryExportTextBox.Document.ContentEnd);

            //Clear up all highligted Text
            textRange.ClearAllProperties();

            //get the richtextbox text
            string textBoxText = textRange.Text;

            //get search text
            string searchText = SearchTextBox.Text;

            if (string.IsNullOrWhiteSpace(textBoxText) || string.IsNullOrWhiteSpace(searchText))
            {
                //Error Message
            }
            else
            {
                Regex regex = new Regex(searchText);
                int count_MatchFound = Regex.Matches(textBoxText, regex.ToString()).Count;

                for (TextPointer startPointer = RegistryExportTextBox.Document.ContentStart;
                        startPointer.CompareTo(RegistryExportTextBox.Document.ContentEnd) <= 0;
                        startPointer = startPointer.GetNextContextPosition(LogicalDirection.Forward))
                {
                    //check if end of text
                    if (startPointer.CompareTo(RegistryExportTextBox.Document.ContentEnd) == 0)
                    {
                        break;
                    }
                    string parsedString = startPointer.GetTextInRun(LogicalDirection.Forward);

                    //check if the search string present here
                    int indexOfParseString = parsedString.IndexOf(searchText);

                    if (indexOfParseString >= 0) //present
                    {
                        //setting up the pointer here at this matched index
                        startPointer.GetPositionAtOffset(indexOfParseString);

                        if (startPointer != null)
                        {
                            //next pointer will be the length of the search string
                            TextPointer nextPointer = startPointer.GetPositionAtOffset(searchText.Length);

                            //create the text range
                            TextRange searchedTextRange = new TextRange(startPointer, nextPointer);

                            //color up
                            searchedTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Yellow));

                            //Other settings for format
                        }
                    }
                }
                //update the label text with count
                if (count_MatchFound > 0)
                {
                    //Report the number of matches
                }
                else
                {
                    //Report no match found
                }
            }

        }

        private void ButtonDeleteFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
