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
       // public IList<Categorie> listeCategories;

        public ItemsView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();
            // Création de la liste d'items
            listeItems = ViewModelItem.ObtenirTousLesItems();
            lbxListeCategorie.ItemsSource = listeItems;
            // Création de la liste de catégories
            //listeCategories = ViewModelCategorie.ObtenirToutesLesCategories();
            //cboCategorieLiee.ItemsSource = listeCategories;

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


        }
    }
}
