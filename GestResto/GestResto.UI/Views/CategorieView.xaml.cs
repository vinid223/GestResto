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
        public IList<Categorie> listeCategories;

        public CategorieView()
        {
            InitializeComponent();
            DataContext = new CategorieViewModel();

            // On tente d'obtenir toutes les catégories
            try
            { 
                listeCategories = ViewModel.ObtenirToutesLesCategories();
            }
            // Dans le cas d'une erreur
            catch(Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des catégories :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            listeBoutonCategories.ItemsSource = listeCategories;

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)((sender as Button).CommandParameter);
            ViewModel.Categorie = categorie;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNom.IsEnabled = true;
            cbxActif.IsEnabled = true;
            cbxComplementaire.IsEnabled = true;
        }

        // Fonction qui permet d'enregistrer la catégorie en cours dans la base de donnée.
        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            var scope = FocusManager.GetFocusScope(txtNom); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(cbxActif); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(cbxComplementaire); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            Keyboard.ClearFocus(); // remove keyboard focus

            // Si un des champs est vide, on informe l'utilisateur
            if (ViewModel.Categorie.Nom == "")
            {
                erreur = true;
                messageErreur.Append("Tous les champs doivent être remplis afin d'enregistrer\n");
            }

            // S'il y a une erreur, on affiche un message
            if (erreur)
            {
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            else
            {
                try
                {
                    ViewModel.EnregistrerUneCategorie(ViewModel.Categorie);
                }
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                    messageErreur.Append("Impossible d'enregistrer la catégorie.\n");

                    // On vérifie si l'exception provient du nom
                    if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                    {
                        messageErreur.Append("Le nom doit être unique");
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                    }

                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }

            }
        }
        // Fonction qui permet d'ajouter dans la base de données une catégorie.
        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On crée une catégorie en mémoire
            Categorie categTemp = new Categorie("Entrez le nom de votre catégorie", false, false);

            StringBuilder messageErreur = new StringBuilder();


            // On tente de faire un ajout dans la base de données
            try
            {
                // On insert dans la base de donnée la nouvelle catégorie et on en retire l'id
                categTemp.IdCategorie = ViewModel.AjouterUneCategorie(categTemp);
            }
            catch (Exception exception)
            {
                string exceptionMessage = exception.InnerException.Message;
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                messageErreur.Append("Impossible d'ajouter une catégorie.\n");

                // On vérifie si l'exception provient du nom
                if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                {
                    messageErreur.Append("Vous devez renommer le nom de votre catégorie avant d'en ajouter une nouvelle");
                }
                else
                {
                    messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                }

                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suis

            }
            // On ajoute dans la liste la catégorie créé
            listeCategories.Add(categTemp);

            // On indique dans le view model la nouvelle catégorie créé
            ViewModel.Categorie = categTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNom.IsEnabled = true;
            cbxActif.IsEnabled = true;
            cbxComplementaire.IsEnabled = true;

            // On donne la nouvelle source à notre liste view
            listeBoutonCategories.ItemsSource = listeCategories;

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonCategories.ItemsSource);
            view.Refresh();
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Déconnexion"); // TODO Non implémenté pour l'instant 
            // Nous devrions créer une fonction commune qu'on va appeler
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }
    }
}
