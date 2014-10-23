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
    /// Logique d'interaction pour EmployeView.xaml
    /// </summary>
    public partial class EmployeView : UserControl
    {
        public EmployeViewModel ViewModel { get { return (EmployeViewModel)DataContext; } }
        public EmployeView()
        {
            InitializeComponent();
            DataContext = new EmployeViewModel();

            // On va charger la source de notre liste d'employé
            listeBoutonEmploye.ItemsSource = ViewModel.Employes;
        }

        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton presséConstante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // On test si un des champs sont désactivé pour éviter de créer une erreur lors de la sauvegarde
            if (!txtNom.IsEnabled)
            {
                MessageBox.Show("Vous devez avoir choisi un Employer pour sauvegarder", "Erreur de sauvegarde", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // On définie les variables nécessaires pour le test d'erreur
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            // Si un des champs est vide, on informe l'utilisateur
            if (ViewModel.employe.Nom           == "" ||
                ViewModel.employe.Adresse       == "" ||
                ViewModel.employe.CodePostal    == "" ||
                ViewModel.employe.MotDePasse    == "" ||
                ViewModel.employe.NAS           == "" ||
                ViewModel.employe.NoEmploye     == "" ||
                ViewModel.employe.Prenom        == "" ||
                ViewModel.employe.Salaire       == null ||
                ViewModel.employe.Telephone     == "" ||
                ViewModel.employe.Ville         == "")
            {
                erreur = true;
                messageErreur.Append("Tous les champs doivent être remplis afin d'enregistrer\n");
            }

            // S'il y a une erreur, on affiche un message
            if (erreur)
            {
                // On affiche un message d'erreur à l'utilisateur et on le log
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Les champs d'enregistrement d'un Employé ne sont pas valides: " + messageErreur.ToString());
            }
            else
            {
                // On essait d'enregistrer un employer
                try
                {
                    ViewModel.EnregistrerUnEmployer(ViewModel.employe);
                }
                    // S'il y a une erreur on va chercher le message et on le log
                catch (Exception exception)
                {
                    string exceptionMessage = exception.InnerException.Message;
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide

                    messageErreur.Append("Impossible d'enregistrer l'employer.\n");

                    // On vérifie si l'exception provient du nom
                    if (Regex.IsMatch(exceptionMessage, @"'nom'$"))
                    {
                        messageErreur.Append("Le nom doit être unique");
                        Constante.LogErreur("Le nom n'est pas unique lors de l'enregistrement d'un employer");
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                        Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un employer");
                    }

                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                }

            }
        }

        /// <summary>
        /// Fonction permettand d'ajouter un employé
        /// </summary>
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Ajouter");
        }

        /// <summary>
        /// Bouton servant à déconnecter un utilisateur
        /// </summary>
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
            Constante.Deconnexion();
        }

        /// <summary>
        /// Fonction permettant de revenir à la vue précédente
        /// </summary>
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }

        /// <summary>
        /// Fonction permettant de rendre nos boutons plus réel lors de la pression
        /// </summary>
        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        /// <summary>
        /// Bouton permettant d'afficher les détails d'un employé
        /// </summary>
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Employe employe= (Employe)((sender as Button).CommandParameter);
            ViewModel.employe = employe;

            // On active les champs pour permettre la modification et l'ajout d'informations
            ActiverDesactiverChamp(true, false);
        }

        /// <summary>
        /// Fonction permettant d'activer ou de désactiver les champs de la fenêtre
        /// </summary>
        /// <param name="typeActivation">Paramêtre d'activation True/False</param>
        private void ActiverDesactiverChamp(bool typeActivation, bool ifBouton)
        {
            txtNom.IsEnabled = typeActivation;
            cbxType.IsEnabled = typeActivation;
            txtAdresse.IsEnabled = typeActivation;
            txtCP.IsEnabled = typeActivation;
            txtMDP.IsEnabled = typeActivation;
            txtNAS.IsEnabled = typeActivation;
            txtNum.IsEnabled = typeActivation;
            txtPrenom.IsEnabled = typeActivation;
            txtTauxHoraire.IsEnabled = typeActivation;
            txtTel.IsEnabled = typeActivation;
            txtVille.IsEnabled = typeActivation;
            chkActif.IsEnabled = typeActivation;

            // On vérifie si on veut aussi modifier les boutons.
            if (ifBouton)
            {
                btnEnregistrer.IsEnabled = typeActivation;
                btnAjouter.IsEnabled = typeActivation;   
            }
        }
    }
}
