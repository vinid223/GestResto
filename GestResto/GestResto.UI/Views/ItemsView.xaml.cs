using GestResto.Logic.Model.Entities;
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
    /// Logique d'interaction pour ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        public ItemsViewModel ViewModelItem { get { return (ItemsViewModel)DataContext; } }
        //public CategorieViewModel ViewModelCategorie { get { return (CategorieViewModel)DataContext; } }


        public IList<Item> listeItems;
        public IList<Categorie> listeCategories;

        public ItemsView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();

            // Création de la liste d'items
            listeItems = ViewModelItem.ObtenirTousLesItems();
            lbxListeCategorie.ItemsSource = listeItems;

            // Création de la liste de catégories liées à l'item sélectionné
            listeCategories = ViewModelItem.ObtenirToutesLesCategories();
            cboCategorieLiee.ItemsSource = listeCategories;

            // Création de la liste des catégories qui affiche les items.
            listeCategories = ViewModelItem.ObtenirToutesLesCategories();
            cboCategorieAffichee.ItemsSource = listeCategories;


        }
        /// <summary>
        /// Lorsque l'utilisateur clique sur le bouton d'un item dans la liste, 
        /// pour afficher les informations de l'items dans les champs de l'écran.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)((sender as Button).CommandParameter);
            ViewModelItem.Item = item;

            // Sélection de la catégorie liée à l'item.
            cboCategorieLiee.SelectedItem = item.Categories;

        }

        /// <summary>
        /// Fonction qui permet d'enregistrer l'item en cours dans la base de donnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Permet d'enlever le focus sur les champs pour permettre la sauvegarde de l'item
            var scope = FocusManager.GetFocusScope(txtNom); 
            FocusManager.SetFocusedElement(scope, null);
            scope = FocusManager.GetFocusScope(chkActif);
            FocusManager.SetFocusedElement(scope, null);
            scope = FocusManager.GetFocusScope(lbxListePrix); 
            FocusManager.SetFocusedElement(scope, null);
            scope = FocusManager.GetFocusScope(cboCategorieAffichee);
            FocusManager.SetFocusedElement(scope, null);
            scope = FocusManager.GetFocusScope(cboCategorieLiee);
            FocusManager.SetFocusedElement(scope, null);
            Keyboard.ClearFocus();

            // On doit sélectionner la catégorie choisie et la changer dans l'item
            ViewModelItem.Item.Categories = (Categorie)cboCategorieLiee.SelectedItem;
            ViewModelItem.EnregistrerUnItem(ViewModelItem.Item);

        }


        /// <summary>
        /// Lorsque l'utilisateur veut ajouter un item, on vide les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            // Création d'un item temporaire.
            Item itemTemp = new Item("Entrez le nom de l'item.", null, null, true);

            itemTemp.IdItem = ViewModelItem.AjouterUnItem(itemTemp);

            ViewModelItem.Item = itemTemp;

        }

        /// <summary>
        /// Lorsque cette liste déroulante va changer, la liste d'items va seulement afficher 
        /// les items de la catégorie choisie dans la liste déroulante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCategorieAffichee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Categorie categorie = (Categorie)cboCategorieAffichee.SelectedItem;

            lbxListeCategorie.ItemsSource = ViewModelItem.ObtenirTousLesItemsDeLaCategorie(categorie);

        }

        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }
    }
}
