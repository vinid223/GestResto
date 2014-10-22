using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        StringBuilder messageErreur = new StringBuilder();

        public ItemsView()
        {
            InitializeComponent();
            DataContext = new ItemsViewModel();

            // Mets tous les items dans la listes d'items dans l'écran.
            lbxListeCategorie.ItemsSource = ViewModelItem.Items;

            // Empêche l'utilisateur de faire des modifications sur un items qui n'existe pas.
            ModifierDispoChampsItem(false);

            // Sélection de la catégorie Tous les items
            lbxListeCategorie.SelectedItem = ViewModelItem.categTest;
            

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
            messageErreur.Clear();

            // Si l'item est null, je ne peux pas l'enregistrer.
            if (ViewModelItem.Item != null)
            {
                Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

                try
                {
                    ViewModelItem.EnregistrerUnItem(ViewModelItem.Item);
                }
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                    messageErreur.Append("Impossible d'enregistrer l'item.\n");

                    // On vérifie si l'exception provient du nom
                    if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                    {
                        messageErreur.Append("Le nom : ").Append(ViewModelItem.Item.Nom).Append(" existe déjà.");
                        Constante.LogErreur("Le nom n'est pas unique lors de l'enregistrement d'un item");
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

        /// <summary>
        /// Permet de modifier le IsEnable de tous les contrôles consernant l'item binder.
        /// </summary>
        /// <param name="EstActif"></param>
        private void ModifierDispoChampsItem(bool EstActif)
        {
            txtNom.IsEnabled = EstActif;
            chkActif.IsEnabled = EstActif;
            cboCategorieLiee.IsEnabled = EstActif;
            btnAjoutFormatItem.IsEnabled = EstActif;
            
        }
    }
}
