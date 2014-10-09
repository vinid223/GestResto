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
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FormatView.xaml
    /// </summary>
    public partial class FormatView : UserControl
    {
        public FormatView()
        {
            InitializeComponent();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bouton");
        }

        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Enregistrer");
        }
        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ajouter");
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Déconnexion"); // TODO Non implémenté pour l'instant 
            // Nous devrions créer une fonction commune qu'on va appeler
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }


    }
}
