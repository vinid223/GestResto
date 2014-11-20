using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
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
using System.Text.RegularExpressions;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : UserControl
    {
        public RestaurantViewModel ViewModel { get { return (RestaurantViewModel)DataContext; } }

        public RestaurantView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();
            try
            {
                DataContext = new RestaurantViewModel();
            }
            // Dans le cas d'une erreur
            catch(Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher les informations du restaurant :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher les informations du restaurant : " + exceptionMessage);
            }
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // On test si les informations du restaurant ont été modifié
            if (TesterSiModifie())
            {
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());   
            }
        }

        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            // On valide si le nom est vide
            if(ViewModel.Restaurant.Nom == "")
            {
                erreur = true;
                messageErreur.Append("Vous devez inscrire un nom\n");
                Constante.LogErreur("Aucun nom n'est indiqué pour le restaurant");
            }
            // On valide si l'adresse est vide
            if (ViewModel.Restaurant.Adresse == "")
            {
                erreur = true;
                messageErreur.Append("Vous devez inscrire une adresse\n");
                Constante.LogErreur("Aucune adresse n'est indiquée pour le restaurant");
            }
            // On valide si le nom de la ville est vide
            if (ViewModel.Restaurant.Ville == "")
            {
                erreur = true;
                messageErreur.Append("Vous devez inscrire une ville\n");
                Constante.LogErreur("Aucune vile n'est indiquée pour le restaurant");
            }
            // On valide si le code postal est vide
            if (ViewModel.Restaurant.CodePostal == "")
            {
                erreur = true;
                messageErreur.Append("Vous devez inscrire un code postal\n");
                Constante.LogErreur("Aucun code postal n'est indiqué pour le restaurant");
            }
            // On valide le code postal avec un regex
            if (!Regex.IsMatch(ViewModel.Restaurant.CodePostal, @"^([a-zA-Z][0-9]){3}$"))
            {
                erreur = true;
                messageErreur.Append("Le code postal doit être sous le format A1A1A1\n");
                Constante.LogErreur("Le code postal du restaurant n'est pas sous le format A1A1A1");
            }
            // On valide le numéro de téléphone avec un regex
            if (!Regex.IsMatch(ViewModel.Restaurant.Telephone, @"^([0-9]{10}|)$"))
            {
                erreur = true;
                messageErreur.Append("Le numéro de téléphone doit contenir 10 chiffres\n");
                Constante.LogErreur("Le numéro de téléphone du restaurant ne contient pas 10 chiffres");
            }
            // On valide le numéro de fax avec un regex
            if (!Regex.IsMatch(ViewModel.Restaurant.Fax, @"^([0-9]{10}|)$"))
            {
                erreur = true;
                messageErreur.Append("Le numéro de fax doit contenir 10 chiffres\n");
                Constante.LogErreur("Le numéro de fax du restaurant ne contient pas 10 chiffres");
            }
            // S'il y a une erreur, on affiche un message
            if (erreur)
            {
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suit
            }
            else
            {
                try
                {
                    ViewModel.EnregistrerRestaurant(ViewModel.Restaurant);
                }
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                    messageErreur.Append("Impossible d'enregistrer le restaurant.\n");


                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement du restaurant");

                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return; // On retourne afin d'évite de faire le code qui suit
                }
                Constante.LogNavigation("Enregistrement du restaurant " + ViewModel.Restaurant.Nom.ToString());
            }
            ViewModel.Restaurant.EstModifie = false;
            
        }

        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // On s'assure si notre restaurant à été modifié ou non
            if (TesterSiModifie())
            {
                // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
                Constante.Deconnexion();
            }
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        /// <summary>
        /// Fonction permettant de tester tous les objets et vérifier s'ils sont modifié
        /// </summary>
        /// <returns>Retourne vrai pour continuer, Retourne faux pour ne pas continuer</returns>
        private bool TesterSiModifie()
        {
            // Bool global de la fonction qui permet d'afficher un message ou non s'il y a des enregistrement
            // non sauvegardé.
            bool RestaurantModifie = ViewModel.Restaurant.EstModifie;

            // Variable de message box qui permet de tester le résultat de la demande à l'utilisateur
            MessageBoxResult messageBoxResult = new MessageBoxResult();

            // Définition de notre string builder pour le message
            StringBuilder message = new StringBuilder();

            // S'il y a des objets non sauvegardé
            if (RestaurantModifie)
            {
                // On affiche un messagebox à l'utilisateur pour lui demander s'il veut continuer ou non
                message.Append("Voulez-vous continuer sans sauvegarder?");
                messageBoxResult = MessageBox.Show(message.ToString(), "Restaurant non sauvegardé", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
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
    }
}
