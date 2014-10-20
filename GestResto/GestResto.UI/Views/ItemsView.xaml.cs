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
    /// Logique d'interaction pour ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        public ItemsViewModel ViewModelItem { get { return (ItemsViewModel)DataContext; } }

        public ItemsView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();

            // Mets tous les items dans la listes d'items dans l'écran.
            lbxListeCategorie.ItemsSource = ViewModelItem.Items;

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

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            FormatItem formatitem = (FormatItem)((sender as Button).CommandParameter);


            ViewModelItem.DeleteFormatItem(formatitem);
            dataGridPrix.Items.Refresh();
        }

        /// <summary>
        /// Fonction qui permet d'enregistrer l'item en cours dans la base de donnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            ViewModelItem.EnregistrerUnItem(ViewModelItem.Item);
        }


        /// <summary>
        /// Lorsque l'utilisateur veut ajouter un item, on vide les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            MouseButtonEventArgs m = null;
            Constante.onReleaseButton(sender, m); // On enlève l'effet du bouton pressé
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

        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
            Constante.Deconnexion();
        }

        private void AjoutFormatItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModelItem.Item.Formats.Add(new FormatItem());
            dataGridPrix.Items.Refresh();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
    }
}
