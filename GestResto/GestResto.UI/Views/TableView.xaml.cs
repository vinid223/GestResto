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
            lblNom.Content = Constante.employe.ToString();
            try
            {
                DataContext = new TableViewModel();
            }
            // Dans le cas d'une erreur
            catch (Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des tables :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des tables : " + exceptionMessage);
            }

            lvwListeTable.ItemsSource = ViewModel.Tables;
        }

        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // Si les champs ne sont pas activé, ça veut dire qu'aucun format n'est sélectionné
            if (!txtNumeroTable.IsEnabled)
            {
                MessageBox.Show("Vous devez sélectionner une table avant d'enregistrer\n", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Les champs d'enregistrement d'une table ne sont pas valides/nAucun numéro de table choisi");
                return;
            }
            
            if(txtNumeroTable.Text == "")
            {
                MessageBox.Show("Vous devez choisir un numéro de table", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Les champs d'enregistrement d'une table ne sont pas valides/nAucun numéro de table choisi");
                return;
            }

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

            Constante.LogNavigation("Enregistrement de table #" + ViewModel.table.NoTable);
            ViewModel.EnregistrerTable(ViewModel.table);
            ViewModel.table.EstModifie = false;
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
                    MessageBox.Show("Une nouvelle table a déjà été ajouté. Vous devez le renommer avant d'en ajouter une nouvelle", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    Constante.LogErreur("Tentative d'ajout d'une table sans avoir modifié la précédente");
                }
            }

            if (!Existe)
            {
                tableTemp.IdTable = ViewModel.AjouterUneTable(tableTemp);
                ViewModel.table = tableTemp;
                ViewModel.Tables.Add(ViewModel.table);
                Constante.LogNavigation("Ajout d'une nouvelle table");
            }

            
        }
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // On s'assure qu'il n'y a pas de table modifié non sauvegardé
            if (TesterSiModifie())
            {
                // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
                Constante.Deconnexion();
            }
            
        }

        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // On test s'il y a au moins une table modifié non sauvegardé
            if (TesterSiModifie())
            {
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());    
            }

        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNumeroTable.IsEnabled = true;
            chkActif.IsEnabled = true;
        }

        /// <summary>
        /// Fonction permettant de tester tous les objets et vérifier s'ils sont modifié
        /// </summary>
        /// <returns>Retourne vrai pour continuer, Retourne faux pour ne pas continuer</returns>
        private bool TesterSiModifie()
        {
            // Bool global de la fonction qui permet d'afficher un message ou non s'il y a des enregistrement
            // non sauvegardé.
            bool TableModifie = false;

            // Variable de message box qui permet de tester le résultat de la demande à l'utilisateur
            MessageBoxResult messageBoxResult = new MessageBoxResult();

            // Définition de notre string builder pour le message
            StringBuilder message = new StringBuilder();

            // Boucle parcourant la liste de catégorie
            foreach (var item in ViewModel.Tables)
            {
                // On test si la catégorie a été modifié
                if (item.EstModifie)
                {
                    // Si c'est le cas on indique qu'on a des items de modifié et on écrit un message
                    TableModifie = true;
                    message.Append("La table ").Append(item.NoTable).Append(" n'a pas été enregistré.\n");
                }
            }

            // S'il y a des objets non sauvegardé
            if (TableModifie)
            {
                // On affiche un messagebox à l'utilisateur pour lui demander s'il veut continuer ou non
                message.Append("\n\nVoulez-vous continuer sans sauvegarder?");
                messageBoxResult = MessageBox.Show(message.ToString(), "Table non sauvegardé", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }

            // On test les retour possible de l'utilisateur 
            if (messageBoxResult == MessageBoxResult.No)
            {
                return false;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return true;
            }

            return true;
        }

    }
}
