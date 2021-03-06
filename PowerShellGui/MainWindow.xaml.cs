﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
        }

        public void changeProgressBar(int value)
        {
            ProgressBar.Value = value;
        }
        private void ButtonPopupExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListviewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    break;

                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new SynchronisePolicies());
                    break;

                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new RegistryExport());
                    break;

                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FileTransfer());
                    break;

                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Sandbox());
                    break;

                default:
                    break;
            }
        }
    }
}
