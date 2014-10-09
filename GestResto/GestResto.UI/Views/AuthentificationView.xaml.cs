using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
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
    /// Logique d'interaction pour AuthentificationView.xaml
    /// </summary>
    public partial class AuthentificationView : UserControl
    {
        //public ItemsViewModel ViewModel { get { return (ItemsViewModel)DataContext; } }

        public ItemsViewModel ViewModel { get { return (ItemsViewModel)DataContext; } }
       // public CategorieViewModel CategViewModel { get { return (CategorieViewModel)DataContext; } }
        //public FormatItemViewModel ViewModel { get { return (FormatItemViewModel)DataContext; } }

        public IList<FormatItem> listeFormatItem;

        public AuthentificationView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();
           // listeFormatItem = ViewModel.ObtenirTousLesFormatItems();
           // float test2 = 100;

            /* QUESTION YANNICK est-ce que je suis obliger de garder
             * un objet item et un objet format dans la classe FormatItem
             * pour pouvoir ajouter des formatItem linker avec un format et un item...*/

            //FormatItem test = new FormatItem(test2);

            //ViewModel.AjouterUnFormatItem(test);


           // List<FormatItem> testFormats = new List<FormatItem>();
           // FormatItem testFormatItem = new FormatItem(100);
           // testFormats.Add(testFormatItem);
            Categorie testCateg = new Categorie(2,"Ceci est un super test",false,true);

            Item test = new Item("testTommy", null, testCateg);

            ViewModel.AjouterUnItem(test);

            

            //ViewModel.AjouterUnItem(test);



        }

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
