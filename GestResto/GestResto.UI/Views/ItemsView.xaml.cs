﻿using GestResto.Logic.Model.Entities;
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
            lblNom.Content = Constante.employe.ToString();



            try
            {
                DataContext = new ItemsViewModel();
            }
            catch (Exception e)
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


            dataGridPrix.CommitEdit();

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

            if (ViewModelItem.Item.Formats != null)
            {
                dataGridPrix.ItemsSource = ViewModelItem.Item.Formats;
            }

            dataGridPrix.Items.Refresh();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            FormatItem formatitem = (FormatItem)((sender as Button).CommandParameter);

            ViewModelItem.Item.Formats.Remove(formatitem);
            if (formatitem.ItemAssocie != null) // S'il n'a pas déjà été en BD
            {
                listeFormatItemASupprimer.Add(formatitem);
            }
            ViewModelItem.Item.EstModifie = true;

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

            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // Si l'item est null, je ne peux pas l'enregistrer.
            if (ViewModelItem.Item != null)
            {
                // Boucle qui permet de vérifier si les formats sont uniques,
                // pour empêcher le droit d'avoir 2 formats grands avec deux prix différents. 
                if (ViewModelItem.Item.Formats != null && ViewModelItem.Item.Formats.Count != 0)
                {
                    int i = 0;

                    foreach (var item in ViewModelItem.Items)
                    {
                        foreach (var formatitem in item.Formats)
                        {
                            if (formatitem.IdFormatItem != null && formatitem.FormatAssocie != null)
                            {
                                i = item.Formats.ToList().Where(x => x.FormatAssocie.IdFormat == formatitem.FormatAssocie.IdFormat).Count();
                                // Vérification s'il a plus de format identique que 2
                                if (i > 1)
                                {
                                    MessageBox.Show("Il existe plusieurs formats identiques dans l'item : " + item.Nom + ", veuillez avoir seulement des formats uniques associés au prix.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                    Constante.LogErreur("Il existe plusieurs formats identiques de l'item, veuillez avoir seulement des formats uniques associés au prix.");
                                    return;
                                }
                            }
                            else if (formatitem.FormatAssocie == null)
                            {
                                MessageBox.Show("Vous devez choisir un format pour l'item.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                return;
                            }
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("L'item doit posséder au moins un prix et un format associé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    Constante.LogErreur("L'item doit posséder au moins un prix et un format associé.");
                    return;
                }

                // Si la catégorie n'est pas sélectionnée
                if (ViewModelItem.Item.Categories == null)
                {
                    MessageBox.Show("L'item doit posséder une catégorie.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    Constante.LogErreur("L'item doit posséder une catégorie.");
                    return;
                }

                // Avant d'enregistrer l'item, je dois vérifier si les formatItems ne sont pas null.
                foreach (var formatItem in ViewModelItem.Item.Formats)
                {
                    // S'il a un formatItem ayant un champ null, le formatItem n'est pas complété.
                    if (formatItem.FormatAssocie == null)
                    {
                        // On affiche un mmessage d'erreur
                        MessageBox.Show("Votre prix et le format associé ne doit pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        Constante.LogErreur("Votre prix et le format associé ne doit pas être vide d'un item.");
                        return;
                    }
                }

                // Avant d'enregistrer je dois vérifier si l'item a subit des suppressions de formatItems
                foreach (var formatItem in listeFormatItemASupprimer)
                {
                    Constante.LogNavigation(" a supprimé un formatItem.");
                    if (formatItem.IdFormatItem != null) // Si l'id est null, il n'est pas en BD, donc on ne le supprime pas.
                    {
                        ViewModelItem.DeleteFormatItem(formatItem);
                    }
                }
                // Réinitialise.
                listeFormatItemASupprimer.Clear();

                try
                {
                    ViewModelItem.EnregistrerTousLesItems(ViewModelItem.Items);
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
                ViewModelItem.Item.EstModifie = false;
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
            Item itemTemp = new Item("Entrez le nom de l'item.", null, null, false);

            Constante.LogNavigation(" a créé un item.");

            if (ViewModelItem.Item != null && ViewModelItem.Item.Nom == itemTemp.Nom)
            {
                MessageBox.Show("Vous devez modifier cet item avant d'en ajouter un nouveau.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }


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
                ViewModelItem.Items.Add(itemTemp);
                lbxListeCategorie.ItemsSource = ViewModelItem.Items;
                ViewModelItem.Item = itemTemp;
                AjouterFormatItemDansListe();
            }
            dataGridPrix.Items.Refresh();
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
            Constante.onReleaseButton(sender, e);

            // On test si on a un ou plusieurs items de modifié
            if (TesterSiModifie())
            {
                // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
                Constante.Deconnexion();   
            }
        }

        private void AjoutFormatItem_Click(object sender, RoutedEventArgs e)
        {
            AjouterFormatItemDansListe();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e);

            // On test s'il y a un ou plusieurs catégories modifié
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
        /// Permet de modifier le IsEnable de tous les contrôles consernant l'item binder.
        /// </summary>
        /// <param name="EstActif"></param>
        private void ModifierDispoChampsItem(bool EstActif, bool BoutonsAussi = false)
        {
            txtNom.IsEnabled = EstActif;
            chkActif.IsEnabled = EstActif;
            cboCategorieLiee.IsEnabled = EstActif;
            btnAjoutFormatItem.IsEnabled = EstActif;

            if (BoutonsAussi)
            {
                btnAjouter.IsEnabled = EstActif;
                btnEnregistrer.IsEnabled = EstActif;
            }

        }

        /// <summary>
        /// Fonction permettant de tester tous les objets et vérifier s'ils sont modifié
        /// </summary>
        /// <returns>Retourne vrai pour continuer, Retourne faux pour ne pas continuer</returns>
        private bool TesterSiModifie()
        {
            // Bool global de la fonction qui permet d'afficher un message ou non s'il y a des enregistrement
            // non sauvegardé.
            bool ItemModifie = false;

            // Variable de message box qui permet de tester le résultat de la demande à l'utilisateur
            MessageBoxResult messageBoxResult = new MessageBoxResult();

            // Définition de notre string builder pour le message
            StringBuilder message = new StringBuilder();

            // Boucle parcourant la liste d'item
            foreach (var item in ViewModelItem.Items)
            {
                // On test si l'item a été modifié
                if (item.EstModifie)
                {
                    // Si c'est le cas on indique qu'on a des items de modifié et on écrit un message
                    ItemModifie = true;
                    message.Append("L'item ").Append(item.Nom).Append(" n'a pas été enregistré.\n");
                }
            }

            // S'il y a des objets non sauvegardé
            if (ItemModifie)
            {
                // On affiche un messagebox à l'utilisateur pour lui demander s'il veut continuer ou non
                message.Append("\n\nVoulez-vous continuer sans sauvegarder?");
                messageBoxResult = MessageBox.Show(message.ToString(), "Items non sauvegardé", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
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


        // Fonction qui permet de tester le focus
        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;

                    tb.Focus();
                }
            }
        }

        /// <summary>
        /// Fonction permettant de tout sélectionner dans le bouton lorsqu'on vient par le clavier
        /// </summary>
        private void txtSelectionnerTout(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        /// <summary>
        /// Fonction permettant de tout sélectionner dans le bouton lorsqu'on vient de la souris
        /// </summary>
        private void txtSelectionnerTout(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        /// <summary>
        /// Fonction qui permet de rediriger à la fenêtre de gestion des catégories
        /// </summary>
        private void btnAjoutCategorie_Click(object sender, RoutedEventArgs e)
        {
            // On s'assure qu'on a pas d'enregistrement non sauvegardé
            if (TesterSiModifie())
            {
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CategorieView>(new CategorieView(true));  
            }
        }

        /// <summary>
        /// Fonction permettant d'ajouter dans la liste des formats items un nouveau format item
        /// </summary>
        private void AjouterFormatItemDansListe()
        {
            // Enlève un bug, lors de l'ajout d'un item venant d'être créé.
            if (ViewModelItem.Item.Formats == null)
            {
                ViewModelItem.Item.Formats = new List<FormatItem>();
                dataGridPrix.ItemsSource = ViewModelItem.Item.Formats;
            }

            FormatItem temp = new FormatItem();
            temp.ItemAssocie = ViewModelItem.Item;

            ViewModelItem.Item.Formats.Add(temp);
            ViewModelItem.Item.EstModifie = true;
            dataGridPrix.Items.Refresh();
        }
    }
}
