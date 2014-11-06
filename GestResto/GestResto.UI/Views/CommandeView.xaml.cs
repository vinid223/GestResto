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
    /// Logique d'interaction pour CommandeView.xaml
    /// </summary>
    public partial class CommandeView : UserControl
    {
        
        public CommandeViewModel ViewModel { get { return (CommandeViewModel)DataContext; } }
        int Scroll = 0;
        int HauteurListView;
        List<Item> ListeItemsTemp = new List<Item>();

        public CommandeView(Commande uneCommande = null)
        {
            InitializeComponent();

            DataContext = new CommandeViewModel();

            lbxListeItems.ItemsSource = ViewModel.Items;

           // List<Client> ListClienttemp = 

        }


        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)((sender as Button).CommandParameter);
            ViewModel.Categorie = categorie;
            lbxListeCategorie.SelectedItem = categorie;

            
            lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.IdCategorie == categorie.IdCategorie).ToList();

        }

        private void btnMonterCategorie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Categorie categ = (Categorie)lbxListeCategorie.SelectedItem;

            // J'obtiens environ le nombre de boutons actuellement visibles
            HauteurListView = (int)lbxListeCategorie.ActualHeight / 40;

            if (Scroll >= HauteurListView)
                Scroll -= HauteurListView;
            else
                Scroll = 0;

            lbxListeCategorie.SelectedIndex = Scroll;
            lbxListeCategorie.ScrollIntoView(lbxListeCategorie.SelectedItem);

        }

        private void btnDescendreCategorie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Categorie categ = (Categorie)lbxListeCategorie.SelectedItem;

            // J'obtiens environ le nombre de boutons actuellement visibles
            HauteurListView = (int)lbxListeCategorie.ActualHeight / 40;


            if (Scroll < lbxListeCategorie.Items.Count - HauteurListView)
                Scroll += HauteurListView;
            else
                Scroll = lbxListeCategorie.Items.Count-1;

            lbxListeCategorie.SelectedIndex = Scroll;
            lbxListeCategorie.ScrollIntoView(lbxListeCategorie.SelectedItem);

        }
















        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); /// On enlève l'effet du bouton pressé
            MessageBox.Show("Ajouter");
        }

        private void btnDiviser_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); /// On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<DivisionFactureView>(new DivisionFactureView());
        }

        private void btnImprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Imprimer");
        }

        private void btnSupprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Supprimer");
        }

        private void btnPayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<PaiementView>(new PaiementView());
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandesView>(new CommandesView());
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
    }
}
