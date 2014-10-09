using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
using GestResto.UI.ViewModel;
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

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour AuthentificationView.xaml
    /// </summary>
    public partial class AuthentificationView : UserControl
    {
        //public ItemsViewModel ViewModel { get { return (ItemsViewModel)DataContext; } }
       // public ItemsViewModel ViewModel { get { return (ItemsViewModel)DataContext; } }

        public AuthentificationView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();
        }

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
