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

        public FormatView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();
            // On tente d'obtenir tous les formats
            try
            {
                DataContext = new FormatViewModel();
            }
            // Dans le cas d'une erreur
            catch(Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.ToString();

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des formats :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des formats : " + exceptionMessage);
            }
            listeBoutonFormats.ItemsSource = ViewModel.Formats;
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
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            // Si les champs ne sont pas activé, ça veut dire qu'aucun format n'est sélectionné
            if(!txtNomFormat.IsEnabled)
            {
                erreur = true;
                messageErreur.Append("Vous devez sélectionner un format avant d'enregistrer\n");
            }
            else 
            { 
                // Si un des champs est vide, on informe l'utilisateur
                if (ViewModel.Format.Nom == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez inscrire un nom\n");
                }
                if (ViewModel.Format.Libelle == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez inscrire un libellé\n");
                }

                // Si le libellé n'est pas entre 1 et 3 caractères
                if ( ViewModel.Format.Libelle.Length > 3 || ViewModel.Format.Libelle.Length < 1) 
                {
                    erreur = true;
                    messageErreur.Append("Le libellé doit faire entre 1 et 3 caractères\n");
                }

            }

            // S'il y a une erreur, on affiche un message
            if(erreur)
            {
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Les champs d'enregistrement d'un format ne sont pas valides");
                return; // On retourne afin d'évite de faire le code qui suit
            }
            else
            {
                // On tente d'enregistrer le format
                try
                {
                    ViewModel.EnregistrerUnFormat(ViewModel.Format);
                }
                // S'il y a eu un erreur dans l'enregistrement du format avec MySql
                catch (MySql.Data.MySqlClient.MySqlException mysqlException)
                {
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                    messageErreur.Append("Impossible d'enregistrer le format.\n");
                    string exceptionMessage = mysqlException.Message;

                    // S'il y a un erreur de doublon
                    if(mysqlException.Number == 1062)
                    {
                        // On vérifie si l'exception provient du libellé
                        if (Regex.IsMatch(exceptionMessage, @"'libelle'$"))
                        {
                            messageErreur.Append("Le libelle doit être unique");
                            Constante.LogErreur("Le libelle n'est pas unique lors de l'enregistrement d'un format");
                        }

                        // On vérifie si l'exception provient du nom
                        else if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                        {
                            messageErreur.Append("Le nom doit être unique");
                            Constante.LogErreur("Le nom n'est pas unique lors de l'enregistrement d'un format");
                        }
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                        Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un format");
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

                    messageErreur.Append("Impossible d'enregistrer le format.\n");


                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un format");


                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return; // On retourne afin d'évite de faire le code qui suit
                }
                Constante.LogNavigation("Enregistrement du format " + ViewModel.Format.Nom.ToString());
            }
            
        }

        // Fonction qui permet d'ajouter dans la base de données un format.
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
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
            // S'il y a eu un erreur dans l'enregistrement du format avec MySql
            catch (MySql.Data.MySqlClient.MySqlException mysqlException)
            {
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                messageErreur.Append("Impossible d'enregistrer le format\n");
                string exceptionMessage = mysqlException.Message;

                if (mysqlException.Number == 1062)
                {
                    // On vérifie s'il y a un doublon
                    if (Regex.IsMatch(exceptionMessage, @"'nom'$") || Regex.IsMatch(exceptionMessage, @"'libelle'$"))
                    {
                        messageErreur.Append("Un nouveau format a déjà été ajouté. Vous devez le renommer avant d'en ajouter un nouveau");
                        Constante.LogErreur("Tentative d'ajout d'un format sans avoir modifié le précédent");
                    }
                }
                else
                {
                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'ajout d'un format");
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
                messageErreur.Append("Impossible d'ajouter un format.\n");
                messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                Constante.LogErreur(messageErreur.ToString() + " lors de l'ajout d'un format");

                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suit
            }

            // On ajoute dans la liste le format créé
            ViewModel.Formats.Add(formatTemp);

            // On indique dans le view model le nouveau format créé
            ViewModel.Format = formatTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            txtNomFormat.IsEnabled = true;
            txtLibelle.IsEnabled = true;
            cbxActif.IsEnabled = true;

            // On donne la nouvelle source à notre liste view
            listeBoutonFormats.ItemsSource = ViewModel.Formats;

            // On rafraichie notre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonFormats.ItemsSource);
            view.Refresh();
            Constante.LogNavigation("Ajout d'un nouveau format");
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
