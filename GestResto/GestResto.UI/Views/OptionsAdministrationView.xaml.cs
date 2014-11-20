using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
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
    /// Logique d'interaction pour OptionsAdministrationView.xaml
    /// </summary>
    public partial class OptionsAdministrationView : UserControl
    {
        public OptionsAdministrationView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();
        }

        private void btnGererCategories(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CategorieView>(new CategorieView());
        }

        private void btnGererInformationsRestaurants(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<RestaurantView>(new RestaurantView());
        }

        private void btnGererFormat(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<FormatView>(new FormatView());
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<ItemsView>(new ItemsView());
        }

        private void btnRapports_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<RapportView>(new RapportView());
        }

        private void btnTables_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<TableView>(new TableView());
        }

        private void btnEmployes_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<EmployeView>(new EmployeView());
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        // Fonction qui sert à quitter l'application
        private void btnQuitter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            MessageBoxResult messageBoxResult = new MessageBoxResult();
            messageBoxResult = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Voulez-vous vraiment quitter ?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

            // On test les retour possible de l'utilisateur 
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        
    }
}
