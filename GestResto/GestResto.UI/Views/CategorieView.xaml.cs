using GestResto.Logic.Model.Entities;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour CategorieView.xaml
    /// </summary>
    public partial class CategorieView : UserControl
    {
        public CategorieViewModel ViewModel { get { return (CategorieViewModel)DataContext; } }
        public IList<Categorie> listeCategories;

        public CategorieView()
        {
            InitializeComponent();
            DataContext = new CategorieViewModel();
            listeCategories = ViewModel.ObtenirToutesLesCategories();
            listeBoutonCategories.ItemsSource = listeCategories;

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)((sender as Button).CommandParameter);
            ViewModel.Categorie = categorie;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EnregistrerUneCategorie(ViewModel.Categorie);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // On crée une catégorie en mémoire
            Categorie categTemp = new Categorie("Entrez le nom de votre catégorie",true,false);

            // On enregistre la catégorie dans la base de donnée
            //ViewModel.EnregistrerUneCategorie(categTemp);

            // On ajoute dans la liste la catégorie créé
            listeCategories.Add(categTemp);

            // On indique dans le view model la nouvelle catégorie créé
            ViewModel.Categorie = categTemp;

            // On donne la nouvelle source à notre liste view
            listeBoutonCategories.ItemsSource = listeCategories;

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonCategories.ItemsSource);
            view.Refresh();
        }
    }
}
