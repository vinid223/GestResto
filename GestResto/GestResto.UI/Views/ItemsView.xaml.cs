using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        StringBuilder messageErreur = new StringBuilder();
        List<FormatItem> listeFormatItemASupprimer = new List<FormatItem>();
        bool Erreur = false;

        public ItemsView()
        {
            InitializeComponent();
            try
            {
                DataContext = new ItemsViewModel();
            }
            catch(Exception e)
            {
                messageErreur.Clear();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste d'items : ");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur(messageErreur.ToString());
                Erreur = true;

            }
            if (!Erreur)
            {
                // Mets tous les items dans la listes d'items dans l'écran.
                lbxListeCategorie.ItemsSource = ViewModelItem.Items;

                // Empêche l'utilisateur de faire des modifications sur un items qui n'existe pas.
                ModifierDispoChampsItem(false);

                // Sélection de la catégorie Tous les items
                lbxListeCategorie.SelectedItem = ViewModelItem.categTest;
            }
            else
                ModifierDispoChampsItem(false, true);

        }
        /// <summary>
        /// Lorsque l'utilisateur clique sur le bouton d'un item dans la liste, 
        /// pour afficher les informations de l'items dans les champs de l'écran.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItem_Click(object sender, RoutedEventArgs e)
        {
            ModifierDispoChampsItem(true);

            Item item = (Item)((sender as Button).CommandParameter);
            ViewModelItem.Item = item;


        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            FormatItem formatitem = (FormatItem)((sender as Button).CommandParameter);

            ViewModelItem.Item.Formats.Remove(formatitem);
            listeFormatItemASupprimer.Add(formatitem);

            dataGridPrix.CommitEdit();
            dataGridPrix.Items.Refresh();
        }

        /// <summary>
        /// Fonction qui permet d'enregistrer l'item en cours dans la base de donnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            messageErreur.Clear();

            // Si l'item est null, je ne peux pas l'enregistrer.
            if (ViewModelItem.Item != null)
            {
                Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

                // Avant d'enregistrer l'item, je dois vérifier si les formatItems ne sont pas null.
                foreach (var formatItem in ViewModelItem.Item.Formats)
                {
                    // S'il a un formatItem ayant un champ null, le formatItem n'est pas complété.
                    if(formatItem.FormatAssocie == null)
                    {
                        // On affiche un mmessage d'erreur
                        MessageBox.Show("Votre prix et le format associé ne doit pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        return;
                    }
                }

                // Avant d'enregistrer je dois vérifier si l'item a subit des suppressions de formatItems
                foreach (var formatItem in listeFormatItemASupprimer)
                {
                    Constante.LogNavigation(" a supprimé un formatItem.");
                    ViewModelItem.DeleteFormatItem(formatItem);
                }
                // Réinitialise.
                listeFormatItemASupprimer.Clear() ;

                try
                {
                    ViewModelItem.EnregistrerUnItem(ViewModelItem.Item);
                }
                catch (Exception exception)
                {
                    string exceptionMessage = exception.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                    messageErreur.Append("Impossible d'enregistrer l'item.\n");

                    // On vérifie si l'exception provient du nom
                    if (exception.InnerException != null && Regex.IsMatch(exception.InnerException.Message, @"'nom'$"))
                    {
                        messageErreur.Append("Le nom : ").Append(ViewModelItem.Item.Nom).Append(" existe déjà.");
                        Constante.LogErreur("Le nom n'est pas unique lors de l'enregistrement d'un item");
                    }
                    else if (Regex.IsMatch(exceptionMessage, @"not-null"))
                    {
                        messageErreur.Append("Le format et le prix ne peuvent pas être vide.");
                        ViewModelItem.Items = ViewModelItem.Items;
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                        Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'une catégorie");
                    }
                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un item.");
            }
        }


        /// <summary>
        /// Lorsque l'utilisateur veut ajouter un item, on vide les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {

            bool Existe = false;

            ModifierDispoChampsItem(true);
            MouseButtonEventArgs m = null;
            Constante.onReleaseButton(sender, m); // On enlève l'effet du bouton pressé
            // Création d'un item temporaire.
            Item itemTemp = new Item("Entrez le nom de l'item.", null, null, true);

            Constante.LogNavigation(" a créé un item.");

            // Si l'item par défaut existe déjà, je ne l'ajoute pas à la BD, je fais seulement l'afficher.
            foreach (var item in ViewModelItem.Items)
            {
                if (item.Nom == itemTemp.Nom)
	            {
                    ViewModelItem.Item = item;
                    Existe = true;
	            }
            }

            // Si l'item par défaut n'existe pas, j'utilise le nouvel item avec l'item temporaire. 
            if (!Existe)
            {
                itemTemp.IdItem = ViewModelItem.AjouterUnItem(itemTemp);
                ViewModelItem.Item = itemTemp;
            }

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
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        /// <summary>
        /// Permet de modifier le IsEnable de tous les contrôles consernant l'item binder.
        /// </summary>
        /// <param name="EstActif"></param>
        private void ModifierDispoChampsItem(bool EstActif, bool BoutonsAussi = false)
        {
            txtNom.IsEnabled = EstActif;
            chkActif.IsEnabled = EstActif;
            cboCategorieLiee.IsEnabled = EstActif;
            btnAjoutFormatItem.IsEnabled = EstActif;

            if(BoutonsAussi)
            {
                btnAjouter.IsEnabled = EstActif;
                btnEnregistrer.IsEnabled = EstActif;
            }
            
        }

    }
}
