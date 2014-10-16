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
using GestResto.UI.ViewModel;
using GestResto.Logic.Model.Entities;
using System.ComponentModel;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FormatView.xaml
    /// </summary>
    public partial class FormatView : UserControl
    {
        public FormatViewModel ViewModel { get { return (FormatViewModel)DataContext; } }
        public IList<Format> listeFormat;

        public FormatView()
        {
            InitializeComponent();
            DataContext = new FormatViewModel();
            listeFormat = ViewModel.ObtenirTousLesFormats();
            listeBoutonFormats.ItemsSource = listeFormat;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Format format = (Format)((sender as Button).CommandParameter);
            ViewModel.Format = format;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNomFormat.IsEnabled = true;
            txtLibelle.IsEnabled = true;
            cbxActif.IsEnabled = true;
        }

        // Fonction qui permet d'enregistrer le format en cours dans la base de donnée.
        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var scope = FocusManager.GetFocusScope(txtNomFormat); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(txtLibelle); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(cbxActif); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            Keyboard.ClearFocus(); // remove keyboard focus

            ViewModel.EnregistrerUnFormat(ViewModel.Format);
        }

        // Fonction qui permet d'ajouter dans la base de données un format.
        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On crée un format en mémoire
            Format formatTemp = new Format("Entrez le nom de votre format", "lib", false);

            // On insert dans la base de donnée le nouveau format et on en retire l'id
            formatTemp.IdFormat = ViewModel.AjouterUnFormat(formatTemp);

            // On ajoute dans la liste le format créé
            listeFormat.Add(formatTemp);

            // On indique dans le view model le nouveau format créé
            ViewModel.Format = formatTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNomFormat.IsEnabled = true;
            txtLibelle.IsEnabled = true;
            cbxActif.IsEnabled = true;

            // On donne la nouvelle source à notre liste view
            listeBoutonFormats.ItemsSource = listeFormat;

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonFormats.ItemsSource);
            view.Refresh();
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Déconnexion : Nous devrions créer une fonction commune qu'on va appeler"); // TODO Non implémenté pour l'instant 
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
