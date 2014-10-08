using GestResto.Logic.Model.Entities;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
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
    }
}
