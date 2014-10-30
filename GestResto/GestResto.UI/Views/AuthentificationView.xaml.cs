using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// Logique d'interaction pour AuthentificationView.xaml
    /// </summary>
    public partial class AuthentificationView : UserControl
    {
        // Variables permettant de sauvegarder les informations d'authentification pour envoyer la requête
        private string NoIdentification = null;
        private string MDPIdentification = null;

        // On définie notre view model
        public EmployeViewModel ViewModel = new EmployeViewModel();

        public AuthentificationView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton permettant de confirmet l'entré sélectionné
        /// </summary>
        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            // On vien ici si on a pas donné de numéro d'employé
            if (NoIdentification == null)
            {
                // On sauvegarde en mémoire le numéro d'authentification et on change la valeur des boites de textes et des label
                NoIdentification = txtAuthentification.Text.ToString();
                lblTitreText.Content = "Mot de passe:";
                txtAuthentification.Text = "";
            }
            else
            {
                // Sinon, on vient ici pour donner la valeur du mot de passe
                if (MDPIdentification == null)
                {
                    // On change la valeur des boites de textes et des label
                    lblTitreText.Content = "Numéro d'employé:";
                    txtAuthentification.Text = "";
                }

                // On teste si nos champs ne sont pas vide et qu'ils sont valide avec la taille minimal et maximal
                if (NoIdentification != null && MDPIdentification != null && NoIdentification.Length >= 3 && MDPIdentification.Length >= 3 && NoIdentification.Length <= 7 && MDPIdentification.Length <= 7)
                {
                    // On supprime les espaces pour avoir que les valeurs nécessaires
                    NoIdentification = NoIdentification.Replace(" ", string.Empty);
                    MDPIdentification = MDPIdentification.Replace(" ", string.Empty);

                    // On va chercher dans la abse de données l'employé qu'on tente d'authentifier
                    Constante.employe = ViewModel.ObtenirEmployeAuthentification(NoIdentification,MDPIdentification);

                    // Si la réponse à la requête est null c'est que l'employé est inexistant
                    if (Constante.employe == null)
                    {
                        Constante.LogErreur("Informations de connexion ne sont pas valide");
                        // On affiche un message et on réinitialise les variables
                        MessageBox.Show("Les informations d'authentifications ne sont pas valide, veuillez ressayer", "Employé inexistant", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        NoIdentification = null;
                        MDPIdentification = null;

                        // On change la valeur des boites de textes et des label
                        lblTitreText.Content = "Numéro d'employé:";
                        txtAuthentification.Text = "";
                    }
                        // Sinon, si le type d'employé est null c'est que l'employé n'a pas reçu de type lors de sa création et il n'est pas valide
                    else if (Constante.employe.TypeEmployes == null)
                    {
                        Constante.LogErreur("Le TypeEmploye n'est pas valide = null");
                        MessageBox.Show("L'employe que vous tentez de connecter n'est pas valide. Connectez un administrateur pour corriger le problème", "Employé non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                        // On change la valeur des boites de textes et des label
                        lblTitreText.Content = "Numéro d'employé:";
                        txtAuthentification.Text = "";
                        NoIdentification = null;
                        MDPIdentification = null;
                    }
                        // On test si l'employé n'est pas actif.
                    else if (!Constante.employe.EstActif)
                    {
                        Constante.LogErreur("L'employé est inactif.");
                        MessageBox.Show("L'employé que vous venez d'authentifier est inactif. Veuillez tenter un autre login ou contactez un administrateur.", "Employé inactif", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                        // On change la valeur des boites de textes et des label
                        lblTitreText.Content = "Numéro d'employé:";
                        txtAuthentification.Text = "";
                        NoIdentification = null;
                        MDPIdentification = null;
                    }
                        // Sinon, si le type employé à un id de 1 ça veut donc dire que c'est un administrateur donc on affiche la fenêtre d'option administrateur
                    else if (Constante.employe.TypeEmployes.IdTypeEmploye == 1)
                    {
                        Constante.LogNavigation("L'administrateur s'est connecté correctement");
                        IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                        mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
                    }
                        // Sinon, si le type employé a un id de 2 ça veut donc dire que c'est un serveur et on lui affiche donc la fenêtre de gestion des commandes
                    else if (Constante.employe.TypeEmployes.IdTypeEmploye == 2)
                    {
                        Constante.LogNavigation("Le serveur s'est connecté correctement");
                        IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                        mainVM.ChangeView<CommandesView>(new CommandesView());
                    }
                        // Sinon, l'employé a un type inconnu et non géré par le système donc on affiche un message personnalisé.
                    else
                    {
                        Constante.LogErreur("Le TypeEmploye n'est pas géré par le système. Type non géré: " + Constante.employe.TypeEmployes.NomType);
                        MessageBox.Show("L'employé identifié comporte un type inconnu. Veuillez entrer les informations de connexion a nouveau", "Type employé non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        NoIdentification = null;
                        MDPIdentification = null;

                        // On change la valeur des boites de textes et des label
                        lblTitreText.Content = "Numéro d'employé:";
                        txtAuthentification.Text = "";
                    }
                    
                }
                else
                {
                    Constante.LogErreur("L'utilisateur entre des champs non valide");
                    MessageBox.Show("Les informations que vous avez ne sont pas valide, veuillez entrer à nouveau les informations", "Champ non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    NoIdentification = null;
                    MDPIdentification = null;
                    
                    // On change la valeur des boites de textes et des label
                    lblTitreText.Content = "Numéro d'employé:";
                    txtAuthentification.Text = "";
                }
            }
        }

        /// <summary>
        /// Cette fonction permet au bouton sélectionner d'ajouter le chiffre dans la boite de texte
        /// </summary>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // On teste si on a atteint le nombre maximal de caractère pour le champ d'authentification
            if (txtAuthentification.Text.Length < 7)
            {
                // On définie une string avec le contenu du bouton qui est donc un chiffre. Ce qui permet d'ajouter le bon numéro à la chaine
                string content = (sender as Button).Content.ToString();

                // On test si ce n'est pas le premier champs de la chaine
                // Si c'est le cas on ajoute un espace pour espacer les chiffres pour que ça soit plus lisible
                if (txtAuthentification.Text.Length != 0)
                {
                    txtAuthentification.Text += " ";
                }


                // Si nous avons déjà entré notre numéro d'identification on remplit le mot de passe
                if (NoIdentification != null)
                {
                    // On ajoute un espace seulement si on est avec le mot de passe
                    if (txtAuthentification.Text.Length != 0)
                    {
                        MDPIdentification += " ";
                    }

                    // On définie le nouveau contenu
                    MDPIdentification += content;
                    txtAuthentification.Text += "•";
                }
                else
                {
                    // On ajoute au champ du texte le numéro en cour
                    txtAuthentification.Text += content;
                }
            }
        }

        /// <summary>
        /// Cette fonction permet de supprimer dans la chaine de texte un chiffre
        /// </summary>
        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            // On va enregistrer l'ancien code
            string ancienText = txtAuthentification.Text;
            string ancienMDP = MDPIdentification;

            // On test si l'ancien text a au moins un chiffre
            if (ancienText.Length > 0)
            {
                // On définie une variable pour la nouvelle chaine
                string nouveauText;
                string nouveauMDP = null;

                // Si l'ancienne chaine a un seul chiffre on supprime qu'un seul caractère
                if (ancienText.Length == 1)
                {
                    nouveauText = ancienText.Substring(0, ancienText.Length - 1);

                    // On s'assure que le champ mot de passe à bien ue valeur
                    if (ancienMDP != null)
                    {
                        nouveauMDP = ancienMDP.Substring(0, ancienText.Length - 1);
                    }
                }
                    // Sinon, on supprime 2 caractères puisque nous avons ajouté des espaces entre les chiffres
                else
                {
                    nouveauText = ancienText.Substring(0, ancienText.Length - 2);

                    // On s'assure que le champ mot de passe à bien ue valeur
                    if (ancienMDP != null)
                    {
                        nouveauMDP = ancienMDP.Substring(0, ancienText.Length - 2);
                    }
                }

                // On définie le champ texte avec la nouvelle chaine
                txtAuthentification.Text = nouveauText;
                MDPIdentification = nouveauMDP;   
            }
        }
    }
}
