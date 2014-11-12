using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour CategorieView.xaml
    /// </summary>
    public partial class CategorieView : UserControl
    {
        public CategorieViewModel ViewModel { get { return (CategorieViewModel)DataContext; } }
        public bool Erreur = false;

        public CategorieView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();

            try
            {
                DataContext = new CategorieViewModel();
            }
            catch (Exception e)
            {

                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des catégories :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des catégories: " + exceptionMessage);
                Erreur = true;
            }

            if (!Erreur)
            {
                listeBoutonCategories.ItemsSource = ViewModel.Categories;

            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)((sender as Button).CommandParameter);
            ViewModel.Categorie = categorie;

            // On active les champs pour permettre la modification et l'ajout d'informations
            txtNom.IsEnabled = true;
            cbxActif.IsEnabled = true;
            cbxComplementaire.IsEnabled = true;
        }

        // Fonction qui permet d'enregistrer la catégorie en cours dans la base de donnée.
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            // On test si un des champs est désactivé pour éviter de créer une erreur lors de la sauvegarde
            if (!txtNom.IsEnabled)
            {
                erreur = true;
                messageErreur.Append("Vous devez sélectionner une catégorie avant d'enregistrer\n");   
            }

            // Si un des champs est vide, on informe l'utilisateur
            else if (ViewModel.Categorie.Nom == "")
            {
                erreur = true;
                messageErreur.Append("Tous les champs doivent être remplis afin d'enregistrer\n");
            }

            // S'il y a une erreur, on affiche un message
            if (erreur)
            {
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Les champs d'enregistrement d'une catégorie ne sont pas valides");
                return; // On retourne afin d'évite de faire le code qui suit
            }
            else
            {
                // On tente d'enregistrer la catégorie
                try
                {
                    ViewModel.EnregistrerUneCategorie(ViewModel.Categorie);
                }
                // S'il y a eu un erreur dans l'enregistrement du format avec MySql
                catch (MySql.Data.MySqlClient.MySqlException mysqlException)
                {
                    string exceptionMessage = mysqlException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                    messageErreur.Append("Impossible d'enregistrer la catégorie.\n");

                    // S'il y a un erreur de doublon
                    if(mysqlException.Number == 1062)
                    {
                        messageErreur.Append("Le nom doit être unique");
                        Constante.LogErreur("Le nom n'est pas unique lors de l'enregistrement d'une catégorie");
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                        Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'une catégorie");
                    }
                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return; // On retourne afin d'évite de faire le code qui suit
                }
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                    messageErreur.Append("Impossible d'enregistrer la catégorie.\n");
                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'une catégorie");     

                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return; // On retourne afin d'évite de faire le code qui suit
                }

            }
            Constante.LogNavigation("Enregistrement de la catégorie " + ViewModel.Categorie.Nom.ToString());
        }
        // Fonction qui permet d'ajouter dans la base de données une catégorie.
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On crée une catégorie en mémoire
            Categorie categTemp = new Categorie("Entrez le nom de votre catégorie", false, false);

            StringBuilder messageErreur = new StringBuilder();


            // On tente de faire un ajout dans la base de données
            try
            {
                // On insert dans la base de donnée la nouvelle catégorie et on en retire l'id
                categTemp.IdCategorie = ViewModel.AjouterUneCategorie(categTemp);
            }
            catch (MySql.Data.MySqlClient.MySqlException mysqlException)
            {
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                messageErreur.Append("Impossible d'ajouter une catégorie.\n");
                string exceptionMessage = mysqlException.Message;

                if (mysqlException.Number == 1062)
                {
                    // On vérifie s'il y a un doublon
                    if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                    {
                        messageErreur.Append("Vous devez renommer le nom de votre catégorie avant d'en ajouter une nouvelle");
                        Constante.LogErreur("Tentative d'ajout d'une catégorie sans avoir modifié la précédente");
                    }
                }
                else
                {
                    messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'ajout d'une catégorie");
                }
                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suit
            }
            // Pour toute autre exception
            catch (Exception exception)
            {
                string exceptionMessage = exception.InnerException.Message;
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                messageErreur.Append("Impossible d'ajouter une catégorie.\n");
                messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'ajout d'une catégorie");
                
                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suis
            }
            // On ajoute dans la liste la catégorie créé
            ViewModel.Categories.Add(categTemp);

            // On indique dans le view model la nouvelle catégorie créé
            ViewModel.Categorie = categTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNom.IsEnabled = true;
            cbxActif.IsEnabled = true;
            cbxComplementaire.IsEnabled = true;

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonCategories.ItemsSource);
            view.Refresh();
            Constante.LogNavigation("Ajout d'une nouvelle catégorie");
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
    }
}
