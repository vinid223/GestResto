﻿using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
using GestResto.UI.ViewModel;
using GestResto.UI.Views;
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

namespace GestResto.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Configure();

            ViewModel.CurrentView = new AuthentificationView();
        }

        private void Configure()
        {
            
        }
    }
}
