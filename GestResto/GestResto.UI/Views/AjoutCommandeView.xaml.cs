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
    /// Logique d'interaction pour AjoutCommandeView.xaml
    /// </summary>
    public partial class AjoutCommandeView : UserControl
    {
        public AjoutCommandeView()
        {
            InitializeComponent();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandesView>(new CommandesView());
        }

        private void btnCreer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Créer");
        }

        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouter la table à la commande");
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
    }
}
