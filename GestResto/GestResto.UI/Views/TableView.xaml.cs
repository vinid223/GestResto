using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using GestResto.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestResto.UI
{
    /// <summary>
    /// Logique d'interaction pour TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {
        public TableViewModel ViewModel { get { return (TableViewModel)DataContext; } }

        public TableView()
        {
            InitializeComponent();
            DataContext = new TableViewModel();

            lvwListeTable.ItemsSource = ViewModel.Tables;
        }

        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            int i = 0;
            // Vérification s'il existe un doublons.
            foreach (var table in ViewModel.Tables)
            {
                i = ViewModel.Tables.Where(j => j.NoTable == table.NoTable).Count();
                // S'il a plus d'une table identique, il y a un doublon.
                if(i > 1)
                {
                    MessageBox.Show("Il existe plusieurs tables identiques, veuillez avoir seulement des numéros de table uniques.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return;
                }
            }


            ViewModel.EnregistrerTable(ViewModel.table);
        }
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool Existe = false;
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            Table tableTemp = new Table();



            // Si la table par défaut existe déjà, je ne l'ajoute pas à la BD, je fais seulement l'afficher.
            foreach (var table in ViewModel.Tables)
            {
                if (table.NoTable == tableTemp.NoTable)
                {
                    ViewModel.table = table;
                    Existe = true;
                }
            }

            if (!Existe)
            {
                tableTemp.IdTable = ViewModel.AjouterUneTable(tableTemp);
                ViewModel.table = tableTemp;
                ViewModel.Tables.Add(ViewModel.table);
            }

            
        }
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

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
      
        /// <summary>
        /// Lorsque l'utilisateur clique sur une table dans la liste.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = (Table)((sender as Button).CommandParameter);
            ViewModel.table = table;
        }

        /// <summary>
        /// Permet de modifier le IsEnable de tous les contrôles consernant l'item binder.
        /// </summary>
        /// <param name="EstActif"></param>
        private void ModifierDispoChampsItem(bool EstActif, bool BoutonsAussi = false)
        {

            if (BoutonsAussi)
            {
                btnAjouter.IsEnabled = EstActif;
                btnEnregistrer.IsEnabled = EstActif;
            }

        }

    }
}
