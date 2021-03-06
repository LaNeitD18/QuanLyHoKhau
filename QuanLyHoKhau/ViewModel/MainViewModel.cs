﻿using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuanLyHoKhau.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Variables
        private Page _FrameContent;
        public Page FrameContent
        {
            get { return _FrameContent; }
            set { _FrameContent = value; OnPropertyChanged(); }
        }

        static public DispatcherTimer _timer;

        public bool isLoaded = false;
        #endregion

        #region ICommand
        public ICommand LoadedWindowCommand { get; set; }
        #endregion

        #region Functions

        #endregion

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand((p) => {
                if (p == null)
                    MessageBox.Show(p.GetType().Name);
                Window mainWindow = p as Window;
                //mainWindow.Hide(); // main view hide in login window
                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.ShowDialog();
                isLoaded = true;

                //if (loginWindow.DataContext == null) return;
                //var loginVM = loginWindow.DataContext as LoginViewModel;
                //if (loginVM.isLogin)
                //{
                mainWindow.Show();
                //    LoadRemainsData(); // show remain table
                //}
                //else
                //{

                //}

                //_timer = new DispatcherTimer(DispatcherPriority.Render);
                //_timer.Interval = TimeSpan.FromSeconds(1);
                //_timer.Tick += (sender, args) =>
                //{
                //    if (LoginViewModel.currentUser != null)
                //    {
                //        InitButtonsForUsing(LoginViewModel.currentUser);

                //        _timer.Stop();
                //    }
                //};
                //_timer.Start();

                FrameContent = new NhapGiayTamVangPage();
            });
        }
    }
}
