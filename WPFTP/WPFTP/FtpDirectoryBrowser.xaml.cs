using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WPFTP
{
    public partial class FtpDirectoryBrowser : PhoneApplicationPage
    {
        public FtpDirectoryBrowser()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            while (this.NavigationService.CanGoBack)
            {
                this.NavigationService.RemoveBackEntry();
            }
            (App.Current as App).Client.FtpDirectoryListed += Client_FtpDirectoryListed;
            await(App.Current as App).Client.GetDirectoryListingAsync();
            base.OnNavigatedTo(e);
        }

        protected async override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Do you want to disconnect and exit", "WPFTP Demo App", MessageBoxButton.OKCancel))
            {
                case MessageBoxResult.OK:
                    await (App.Current as App).Client.DisconnectAsync();
                    break;

                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }

            base.OnBackKeyPress(e);
        }

        void Client_FtpDirectoryListed(object sender, WPFTPService.FtpService.FtpDirectoryListedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                if (e.GetFilenames().Length > 0)
                {
                    txtblkNoFiles.Visibility = System.Windows.Visibility.Collapsed;
                    lstFiles.Visibility = System.Windows.Visibility.Visible;
                    lstFiles.ItemsSource = e.GetFilenames();
                }
                else
                {
                    txtblkNoFiles.Visibility = System.Windows.Visibility.Visible;
                    lstFiles.Visibility = System.Windows.Visibility.Collapsed;
                }

                List<String> dirList = new List<String>();
                if (!(App.Current as App).ServerCurrentWorkingDirectory.Equals("/"))
                {
                    dirList.Add("...");
                }
                dirList.AddRange(e.GetDirectories());
                lstDirectories.ItemsSource = dirList;
            });
        }

        private async void txtblkDirName_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if ((sender as TextBlock).DataContext.ToString().Equals("..."))
            {
                await (App.Current as App).Client.ChangeWorkingDirectoryAsync("..");
            }
            else
            {
                await (App.Current as App).Client.ChangeWorkingDirectoryAsync((sender as TextBlock).DataContext.ToString());
            }
            await (App.Current as App).Client.GetDirectoryListingAsync();
        }

        private void txtblkFileName_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}