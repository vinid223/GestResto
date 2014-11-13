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
        // On définie notre viewmodel
        public EmployeViewModel ViewModel { get { return (EmployeViewModel)DataContext; } }

        // Définition du constructeur de viewModel
        public EmployeView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();
            // On tente d'obtenir tous le employés
            try
            {
                DataContext = new EmployeViewModel();
            }
            // Dans le cas d'une erreur
            catch (Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des employés :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des employés : " + exceptionMessage);
            }
            // On va charger la source de notre liste d'employé
            listeBoutonEmploye.ItemsSource = ViewModel.Employes;
        }

        /// <summary>
        /// Fonction permettant d'enregistrer l'employé sélectionné
        /// </summary>
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            bool erreur = false;
            StringBuilder messageErreur = new StringBuilder();

            // On test si un des champs sont désactivé pour éviter de créer une erreur lors de la sauvegarde
            if (!txtNom.IsEnabled)
            {
                erreur = true; 
                messageErreur.Append("Vous devez sélectionner un employé avant d'enregistrer\n");
            }
            // On peut valider les champs
            else 
            {
                // Si le numéro de l'emploé n'est pas valide
                if (ViewModel.employe.NoEmploye == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez choisir un numéro d'employé\n");
                    Constante.LogErreur("Aucun numéro d'employé n'est indiquée pour l'employé");
                }
                if (!Regex.IsMatch(ViewModel.employe.NoEmploye, @"^([0-9]{1,4})$"))
                {
                    erreur = true;
                    messageErreur.Append("Le numéro d'employé doit être entre 1 et 4 chiffre inclusivement\n");
                    Constante.LogErreur("Le numéro d'employé n'est pas entre 1 et 4 chiffre inclusivement");
                }
                // Si le prénom est vide
                if (ViewModel.employe.Prenom == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez inscrire un prénom\n");
                    Constante.LogErreur("Aucun prénom n'est indiquée pour l'employé");
                }
                // Si le nom est vide
                if (ViewModel.employe.Nom == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez inscrire un nom\n");
                    Constante.LogErreur("Aucun nom n'est indiquée pour l'employé");
                }
                // Si le numéro de téléphone n'est pas valide | Peut ête nul
                if (!Regex.IsMatch(ViewModel.employe.Telephone, @"^([0-9]{10}|)$"))
                {
                    erreur = true;
                    messageErreur.Append("Le numéro de téléphone doit contenir 10 chiffres\n");
                    Constante.LogErreur("Le numéro de téléphone de l'employé ne contient pas 10 chiffres");
                }
                // Si le mot de passe n'est pas valide
                if (ViewModel.employe.MotDePasse == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez choisir un mot de passe\n");
                    Constante.LogErreur("Aucun mot de passe n'est indiquée pour l'employé");
                }
                if (!Regex.IsMatch(ViewModel.employe.MotDePasse, @"^([0-9]{2,4})$"))
                {
                    erreur = true;
                    messageErreur.Append("Le mot de passe doit être entre 2 et 4 chiffre inclusivement\n");
                    Constante.LogErreur("Le mot de passe n'est pas entre 2 et 4 chiffre inclusivement");
                }
                // Si le code postal n'est pas valide | Peut ête nul
                if (!Regex.IsMatch(ViewModel.employe.CodePostal, @"^([a-zA-Z][0-9]){3}|$"))
                {
                    erreur = true;
                    messageErreur.Append("Le code postal doit être sous le format A1A1A1\n");
                    Constante.LogErreur("Le code postal de l'employé n'est pas sous le format A1A1A1");
                }
                if (ViewModel.employe.NAS == "")
                {
                    erreur = true;
                    messageErreur.Append("Vous devez choisir un numéro d'assurance social (NAS)\n");
                    Constante.LogErreur("Aucun numéro d'assurance social (NAS) n'est indiquée pour l'employé");
                }
                // Si le NAS n'est pas valide
                if (!Regex.IsMatch(ViewModel.employe.NAS, @"^([0-9]{9})$"))
                {
                    erreur = true;
                    messageErreur.Append("Le numéro d'assurance social (NAS) doit contenir 9 chiffres\n");
                    Constante.LogErreur("Le numéro d'assurance social (NAS) de l'employé ne contient pas 9 chiffres");
                }
                // On vérifie dans le champ du taux horaire
                if (!Regex.IsMatch(txtTauxHoraire.Text, @"^\d{1,6}(?:\.\d{0,2})?$"))
                {
                    erreur = true;
                    messageErreur.Append("Le salaire de l'employé doit être un nombre numérique d'au maximum 6 chiffres et maximum 2 décimales\n");
                    Constante.LogErreur("Le salaire de l'employé n'est pas valide");
                }
            }
            
            // S'il y a une erreur, on affiche un message
            if (erreur)
            {
                // On affiche un message d'erreur à l'utilisateur et on le log
                MessageBox.Show(messageErreur.ToString(), "Informations incomplètes", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suit
            }
            else
            {
                // On essait d'enregistrer un employer
                try
                {
                    ViewModel.EnregistrerUnEmployer(ViewModel.employe);
                }
                // S'il y a eu un erreur dans l'enregistrement du format avec MySql
                catch (MySql.Data.MySqlClient.MySqlException mysqlException)
                {
                    messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                    messageErreur.Append("Impossible d'enregistrer l'employé\n");

                    // On décrypte le mot de passe de l'employé s'il y a eu un problème
                    ViewModel.employe.MotDePasse = Employe.Decrypt(ViewModel.employe.MotDePasse.ToString());

                    string exceptionMessage = mysqlException.Message;

                    // S'il y a un erreur de doublon
                    if(mysqlException.Number == 1062)
                    {
                        // On vérifie si l'exception provient du libellé
                        if (Regex.IsMatch(exceptionMessage, @"'noEmploye'$"))
                        {
                            messageErreur.Append("Le numéro d'employé doit être unique");
                            Constante.LogErreur("Le numéro d'employé n'est pas unique lors de l'enregistrement d'un employé");
                        }

                        // On vérifie si l'exception provient du nom
                        else if (Regex.IsMatch(exceptionMessage, @"'NAS'$"))
                        {
                            messageErreur.Append("Le numéro d'assurance sociale (NAS) doit être unique");
                            Constante.LogErreur("e numéro d'assurance sociale (NAS) n'est pas unique lors de l'enregistrement d'un employé");
                        }
                    }
                    else
                    {
                        messageErreur.Append("Erreur inconnue : ");
                        messageErreur.Append(exceptionMessage);
                        Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un employé");
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

                    messageErreur.Append("Impossible d'enregistrer l'employé\n");

                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un employé");


                    // On affiche le mmessage d'erreur
                    MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return; // On retourne afin d'évite de faire le code qui suit
                }
                Constante.LogNavigation("Enregistrement de l'employé " + ViewModel.employe.Nom.ToString());
                ViewModel.employe.EstModifie = false;
            }
        }

        /// <summary>
        /// Fonction permettand d'ajouter un employé
        /// </summary>
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On crée une catégorie en mémoire
            Employe employeTemp = new Employe("Veuillez entrer les informations de votre employé","","","","","","","",0,"",false,ViewModel.TypesEmployes[0]);

            StringBuilder messageErreur = new StringBuilder();


            // On tente de faire un ajout dans la base de données
            try
            {
                // On insert dans la base de donnée la nouvelle catégorie et on en retire l'id
                employeTemp.IdEmploye = ViewModel.AjouterUnEmployer(employeTemp);
            }
            catch (MySql.Data.MySqlClient.MySqlException mysqlException)
            {
                messageErreur.Clear();  // On s'assure que le message d'erreur soit vide
                messageErreur.Append("Impossible d'enregistrer l'employé\n");
                string exceptionMessage = mysqlException.Message;

                if (mysqlException.Number == 1062)
                {
                    // On vérifie s'il y a un doublon
                    if (Regex.IsMatch(exceptionMessage, @"'NAS'$") || Regex.IsMatch(exceptionMessage, @"'noEmploye'$"))
                    {
                        messageErreur.Append("Un nouvel employé a déjà été ajouté. Vous devez le renommer avant d'en ajouter un nouveau");
                        Constante.LogErreur("Tentative d'ajout d'un employé sans avoir modifié le précédent");
                    }
                }
                else
                {
                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'ajout d'un employé");
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
                messageErreur.Append("Impossible d'ajouter un employé.\n");
                messageErreur.Append("Erreur inconnue : " + exceptionMessage);
                Constante.LogErreur(messageErreur.ToString() + " lors de l'ajout d'un employé");
                
                
                // On vérifie si l'exception provient du nom
                if (Regex.IsMatch(exceptionMessage, @"'noEmploye'$"))
                {
                    messageErreur.Append("Le numéro d'employé que vous avez entré est déjà utilisé. Veuillez en utiliser un autre");
                    Constante.LogErreur("Tentative d'ajout d'un employé avec un numéro déjà utilisé.");
                }
                else
                {
                    messageErreur.Append("Erreur inconnue : ");
                    messageErreur.Append(exceptionMessage);
                    Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'ajout d'un employé");
                }

                // On affiche le mmessage d'erreur
                MessageBox.Show(messageErreur.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return; // On retourne afin d'évite de faire le code qui suis

            }
            // On ajoute dans la liste la catégorie créé
            ViewModel.Employes.Add(employeTemp);

            // On indique dans le view model la nouvelle catégorie créé
            ViewModel.employe = employeTemp;

            // On active les champs pour permettre la modification et l'ajout d'information
            ActiverDesactiverChamp(true, true);

            // On rafraichie nottre liste view pour afficher le bouton ajouté
            ICollectionView view = CollectionViewSource.GetDefaultView(listeBoutonEmploye.ItemsSource);
            view.Refresh();
            Constante.LogNavigation("Ajout d'un nouvel employé");
        }

        /// <summary>
        /// Bouton servant à déconnecter un utilisateur
        /// </summary>
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e);

            // On appel la fonction pour tester si on à un employé non sauvegardé
            if (TesterSiModifie())
            {
                // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
                Constante.Deconnexion();
            }
        }

        /// <summary>
        /// Fonction permettant de revenir à la vue précédente
        /// </summary>
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e);

            // On appel la fonction pour tester si on à un employé non sauvegardé
            if (TesterSiModifie())
            {
                // On redirige vers la fenêtre d'option d'administration
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
            }
        }

        /// <summary>
        /// Fonction permettant de rendre nos boutons plus réel lors de la pression
        /// </summary>
        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        /// <summary>
        /// Bouton permettant d'afficher les détails d'un employé
        /// </summary>
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            // Définition de l'employé par le bouton
            Employe employe= (Employe)((sender as Button).CommandParameter);

            // On test si l'employé est le même, si on clique sur le même bouton
            if (ViewModel.employe != employe)
            {
                // On test s'il n'est pas modifié
                if (employe.EstModifie == false)
                {
                    // Si c'est le cas on procède à la modification et on redonne sa valeure fausse
                    ViewModel.employe = employe;
                    ViewModel.employe.EstModifie = false;
                }
                else
                {
                    // On définie l'employé en cour d'utilisation dans le view model
                    ViewModel.employe = employe;
                }
            }

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

        /// <summary>
        /// Fonction permettant de tester tous les objets et vérifier s'ils sont modifié
        /// </summary>
        /// <returns>Retourne vrai pour continuer, Retourne faux pour ne pas continuer</returns>
        private bool TesterSiModifie()
        {
            // Bool global de la fonction qui permet d'afficher un message ou non s'il y a des enregistrement
            // non sauvegardé.
            bool CategorieModifie = false;

            // Variable de message box qui permet de tester le résultat de la demande à l'utilisateur
            MessageBoxResult messageBoxResult = new MessageBoxResult();

            // Définition de notre string builder pour le message
            StringBuilder message = new StringBuilder();

            // Boucle parcourant la liste de catégorie
            foreach (var item in ViewModel.Employes)
            {
                // On test si la catégorie a été modifié
                if (item.EstModifie)
                {
                    // Si c'est le cas on indique qu'on a des items de modifié et on écrit un message
                    CategorieModifie = true;
                    message.Append("L'employé ").Append(item.Nom).Append(" n'a pas été enregistré.\n");
                }
            }

            // S'il y a des objets non sauvegardé
            if (CategorieModifie)
            {
                // On affiche un messagebox à l'utilisateur pour lui demander s'il veut continuer ou non
                message.Append("\n\nVoulez-vous continuer sans sauvegarder?");
                messageBoxResult = MessageBox.Show(message.ToString(), "Employé non sauvegardé", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
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
