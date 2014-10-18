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
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using GestResto.Logic.Model.Entities;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FormatView.xaml
    /// </summary>
    public partial class FormatView : UserControl
    {
        public FormatViewModel ViewModel { get { return (FormatViewModel)DataContext; } }
        public IList<Format> listeFormat;

        public FormatView()
        {
            InitializeComponent();
            DataContext = new FormatViewModel();
            // On tente d'obtenir tous les formats
            try 
            { 
                listeFormat = ViewModel.ObtenirTousLesFormats();
            }
            // Dans le cas d'une erreur
            catch(Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des formats :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            listeBoutonFormats.ItemsSource = listeFormat;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Format format = (Format)((sender as Button).CommandParameter);
            ViewModel.Format = format;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNomFormat.IsEnabled = true;
            txtLibelle.IsEnabled = true;
            cbxActif.IsEnabled = true;
        }

        // Fonction qui permet d'enregistrer le format en cours dans la base de donnée.
        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            var scope = FocusManager.GetFocusScope(txtNomFormat); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(txtLibelle); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            scope = FocusManager.GetFocusScope(cbxActif); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            Keyboard.ClearFocus(); // remove keyboard focus

            // Si un des champs est vide, on informe l'utilisateur
            if (ViewModel.Format.Nom == "" || ViewModel.Format.Libelle == "")
            {
                erreur = true;
                messageErreur.Append("Tous les champs doivent être remplis afin d'enregistrer\n");
            }

            // Si le libellé n'est pas entre 1 et 3 caractères
            if (ViewModel.Format.Libelle.Length > 3 || ViewModel.Format.Libelle.Length < 1)
            {
                erreur = true;
                messageErreur.Append("Le libellé doit faire entre 1 et 3 caractères\n");
            }

            // S'il y a une erreur, on affiche un message
            if(erreur)
            {
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            else
            {
                // On tente d'enregistrer le format
                try
                {
                    ViewModel.EnregistrerUnFormat(ViewModel.Format);
                }
                // S'il y a eu un erreur dans l'enregistrement du format
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                    messageErreur.Append("Impossible d'enregistrer le format.\n");


                    // On vérifie si l'exception provient du libellé
                    if (Regex.IsMatch(exceptionMessage, @"'libelle'$"))
                    {
                        messageErreur.Append("Le libelle doit être unique");
                    }

                    // On vérifie si l'exception provient du nom
                    else if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
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

        // Fonction qui permet d'ajouter dans la base de données un format.
        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On crée un format en mémoire
            // On ne met pas de libellé par défault, puisqu'étant donné que le format n'est pas actif,
            // si l'utilisateur veut le rendre actif, celui-ci devra choisir un libellé
            Format formatTemp = new Format("Entrez le nom de votre format", "", false);

            StringBuilder messageErreur = new StringBuilder();

            // On tente de faire un ajout dans la base de données
            try
            {
                // On insert dans la base de donnée le nouveau format et on en retire l'id
                formatTemp.IdFormat = ViewModel.AjouterUnFormat(formatTemp);
            }
            catch (Exception exception)
            {
                string exceptionMessage = exception.InnerException.Message;
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                messageErreur.Append("Impossible d'ajouter un format.\n");

                // On vérifie si l'exception provient du nom
                if (Regex.IsMatch(exceptionMessage, @"'nom'$") || Regex.IsMatch(exceptionMessage, @"'libelle'$"))
                {
                    messageErreur.Append("Vous devez renommer votre format avant d'en ajouter un nouveau");
                }
                else 
                {
                    messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                }

                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suis
            }

            // On ajoute dans la liste le format créé
            listeFormat.Add(formatTemp);

            // On indique dans le view model le nouveau format créé
            ViewModel.Format = formatTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNomFormat.IsEnabled = true;
            txtLibelle.IsEnabled = true;
            cbxActif.IsEnabled = true;

            // On donne la nouvelle source à notre liste view
            listeBoutonFormats.ItemsSource = listeFormat;

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonFormats.ItemsSource);
            view.Refresh();
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Déconnexion : Nous devrions créer une fonction commune qu'on va appeler"); // TODO Non implémenté pour l'instant 
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
