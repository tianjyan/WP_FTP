using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPFTP.Resources;
using Microsoft.Phone.Storage;
using Microsoft.Phone.Reactive;
using Windows.Storage;

namespace WPFTP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            IObservable<RoutedEventArgs> Observable_IP = Observable.Select<IEvent<TextChangedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<TextChangedEventHandler, TextChangedEventArgs>
                (h => new TextChangedEventHandler(h.Invoke), delegate(TextChangedEventHandler h)
                {
                    this.tb_IP.TextChanged += h;
                }, delegate(TextChangedEventHandler h)
                {
                    this.tb_IP.TextChanged -= h;
                }), evt => new RoutedEventArgs()
                );

            IObservable<RoutedEventArgs> Observable_Port = Observable.Select<IEvent<TextChangedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<TextChangedEventHandler, TextChangedEventArgs>
                (h => new TextChangedEventHandler(h.Invoke), delegate(TextChangedEventHandler h)
                {
                    this.tb_Port.TextChanged += h;
                }, delegate(TextChangedEventHandler h)
                {
                    this.tb_Port.TextChanged -= h;
                }), evt => new RoutedEventArgs()
                );

            IObservable<RoutedEventArgs> Observable_Name = Observable.Select<IEvent<TextChangedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<TextChangedEventHandler, TextChangedEventArgs>
                (h => new TextChangedEventHandler(h.Invoke), delegate(TextChangedEventHandler h)
                {
                    this.tb_UserName.TextChanged += h;
                }, delegate(TextChangedEventHandler h)
                {
                    this.tb_UserName.TextChanged -= h;
                }), evt => new RoutedEventArgs()
                );

            IObservable<RoutedEventArgs> Observable_Password = Observable.Select<IEvent<RoutedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>
                (h => new RoutedEventHandler(h.Invoke), delegate(RoutedEventHandler h)
                {
                    this.tb_Password.PasswordChanged += h;
                }, delegate(RoutedEventHandler h)
                {
                    this.tb_Password.PasswordChanged -= h;
                }), evt => new RoutedEventArgs()
                );

            IObservable<RoutedEventArgs> Observable_Anonymous_Checked = Observable.Select<IEvent<RoutedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>
                (h => new RoutedEventHandler(h.Invoke), delegate(RoutedEventHandler h)
                {
                    this.cb_Anonymous.Checked += h;
                }, delegate(RoutedEventHandler h)
                {
                    this.cb_Anonymous.Checked -= h;
                }), evt => new RoutedEventArgs()
                );

            IObservable<RoutedEventArgs> Observable_Anonymous_Unchecked = Observable.Select<IEvent<RoutedEventArgs>, RoutedEventArgs>
                (
                Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>
                (h => new RoutedEventHandler(h.Invoke), delegate(RoutedEventHandler h)
                {
                    this.cb_Anonymous.Unchecked += h;
                }, delegate(RoutedEventHandler h)
                {
                    this.cb_Anonymous.Unchecked -= h;
                }), evt => new RoutedEventArgs()
                );

            ObservableExtensions.Subscribe(Observable.Select(Observable.Merge<RoutedEventArgs>(
                Observable_IP, Observable_Port, Observable_Name, Observable_Password,Observable_Anonymous_Checked,Observable_Anonymous_Unchecked), 
                evt => new FTPServiceLoginInfo { IP = this.tb_IP.Text, Port = this.tb_Port.Text, UserName = this.tb_UserName.Text, 
                    Password = this.tb_Password.Password ,IsAnonymous=(bool)this.cb_Anonymous.IsChecked}),
                fTPServiceLoginInfo => {
                    if (!fTPServiceLoginInfo.IsAnonymous)
                    {
                        btn_Login.IsEnabled = !string.IsNullOrWhiteSpace(fTPServiceLoginInfo.IP) && !string.IsNullOrWhiteSpace(fTPServiceLoginInfo.Port) &&
                                              !string.IsNullOrWhiteSpace(fTPServiceLoginInfo.UserName) && !string.IsNullOrWhiteSpace(fTPServiceLoginInfo.Password);
                        
                    }
                    else
                    {
                        btn_Login.IsEnabled =!string.IsNullOrWhiteSpace(fTPServiceLoginInfo.IP) && !string.IsNullOrWhiteSpace(fTPServiceLoginInfo.Port);
                    }
                    tb_UserName.IsEnabled = tb_Password.IsEnabled = !fTPServiceLoginInfo.IsAnonymous;
                });
        }

        private async void Login_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await (App.Current as App).ConnectToFtpServer(tb_IP.Text, tb_Port.Text, tb_UserName.Text, tb_Password.Password);
        }
    }
}