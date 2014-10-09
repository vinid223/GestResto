using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
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
    /// Logique d'interaction pour RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : UserControl
    {
        public RestaurantViewModel ViewModel { get { return (RestaurantViewModel)DataContext; } }
        public Restaurant restaurant;

        public RestaurantView()
        {
            InitializeComponent();
            DataContext = new RestaurantViewModel();
            restaurant = ViewModel.ObtenirRestaurant(1);

            ViewModel.Restaurant = restaurant;
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }
    }
}
