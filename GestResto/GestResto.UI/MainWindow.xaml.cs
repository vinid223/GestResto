using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
using GestResto.UI.ViewModel;
using GestResto.UI.Views;
using System;
using System.Collections.Generic;
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
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.Logic.Services.NHibernate;
using GestResto.Logic.Model.Entities;
using System.IO;

namespace GestResto.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

       public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            // On test ici si la connexion à la base de données est disponible
            try
            {
                Configure();
            }
            catch (Exception e)
            {
                // On écrit un message et on enregistre dans le fichier de log les erreurs
                MessageBox.Show("Impossible de se connecter à la base de données. Veuillez vérifier votre connexion et ressayez plus tard. Si l'erreur persiste, vérifier avec un administrateur système.", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                Constante.LogErreur("Erreur lors de la connexion à la base de données. Erreur 0: " + e.InnerException.ToString());

                // On quitte l'application puisque nous ne pouvons pkus rien faire
                System.Environment.Exit(0);
            }

            // On appel notre vue principal
            ViewModel.CurrentView = new AuthentificationView();
        }

        /// <summary>
        /// Fonction qui permet de configurer nos services pour les lier avec nos interfaces
        /// </summary>
        private void Configure()
        {
            ServiceFactory.Instance.Register<IRestaurantService, NHibernateRestaurantService>(new NHibernateRestaurantService());
            ServiceFactory.Instance.Register<ICategorieService, NHibernateCategorieService>(new NHibernateCategorieService());
            ServiceFactory.Instance.Register<IItemService, NHibernateItemService>(new NHibernateItemService());
            ServiceFactory.Instance.Register<IFormatService, NHibernateFormatService>(new NHibernateFormatService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
            ServiceFactory.Instance.Register<ITypeEmployeService, NHibernateTypeEmployeService>(new NHibernateTypeEmployeService());
            ServiceFactory.Instance.Register<ITableService, NHibernateTableService>(new NHibernateTableService());
            ServiceFactory.Instance.Register<ICommandeService, NHibernateCommandeService>(new NHibernateCommandeService());
            ServiceFactory.Instance.Register<IClientService, NHibernateClientService>(new NHibernateClientService());
            ServiceFactory.Instance.Register<IFactureService, NHibernateFactureService>(new NHibernateFactureService());
            ServiceFactory.Instance.Register<IFormatItemClientFactureService, NHibernateFormatItemClientFacture>(new NHibernateFormatItemClientFacture());
            
           
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
        }

        /// <summary>
        /// Fonction permettant de vérifier les droits de fermeture de l'application
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // On test si on a le droit de quitter
            if (!Constante.peutQuitter)
            {
                // On affiche un message si on a pas le droit
                MessageBox.Show("Vous n'avez pas l'authorisation de quitter l'application.", "Impossible de quitter", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                // On indique au programme de ne pas quitter
                e.Cancel = true;
            }
        }
    }

    /// <summary>
    /// Classe permettant d'accéder a des variables et des fonctions de base nécessaire au fonctionnement du programme
    /// </summary>
    public static class Constante
    {
        private static Employe _employe;
        // Propriété de l'employé pour pouvoir gérer les droits de fermeture de l'application
        public static Employe employe
        {
            get
            {
                return _employe ;
            }

            set
            {
                if (value != null)
                {
                    if (value.TypeEmployes.IdTypeEmploye == 1)
                    {
                        peutQuitter = true;
                    }
                    else
                    {
                        peutQuitter = false;
                    }
                }
                else
                {
                    peutQuitter = false;
                }
                

                _employe = value;
            }
        }
        public static Commande commande = new Commande();
        public static bool peutQuitter = false;

        /// <summary>
        /// Fonction permettant de déconnecter un utilisateur
        /// </summary>
        public static void Deconnexion()
        {
            employe = null;
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AuthentificationView>(new AuthentificationView());

            employe = null;
            commande = null;
        }

        /// <summary>
        /// Fonction permettant de logger les navigations des utilisateurs
        /// </summary>
        /// <param name="MessageDeNavigation">Message à écrire dans le log</param>
        public static void LogNavigation(string MessageDeNavigation)
        {
            string MessageHeader = String.Empty;

            // On définie la string avec le header et la string au complet
            if (employe == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDeNavigation;
            }
            else if (employe.IdEmploye == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDeNavigation;
            }
            else
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: " + employe.Nom + ", " + employe.Prenom + " ------ " + MessageDeNavigation;
            }
            
            // On enregistre dans le fichier de log la ligne
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(".\\Log\\LogNavigation.txt"));

            using (StreamWriter sw = File.AppendText(".\\Log\\LogNavigation.txt"))
            {
                sw.WriteLine(MessageHeader);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Fonction permettant de logger les messages d'erreurs
        /// </summary>
        /// <param name="MessageDErreur">String comportant le message d'erreur à écrire dans le fichier de log</param>
        public static void LogErreur(string MessageDErreur)
        {
            string MessageHeader = String.Empty;

            // On définie la string avec le header et la string au complet
            if (employe == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDErreur;
            }
            else if (employe.IdEmploye == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDErreur;
            }
            else
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: " + employe.Nom + ", " + employe.Prenom + " ------ " + MessageDErreur;
            }

            // On enregistre dans le fichier de log la ligne
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(".\\Log\\LogErreur.txt"));

            using (StreamWriter sw = File.AppendText(".\\Log\\LogErreur.txt"))
            {
                sw.WriteLine(MessageHeader);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Fonction permettant de changer le background des bouton lorsqu'ils sont pressés
        /// </summary>
        /// <param name="sender">L'objet qui a déclanché l'événement</param>
        /// <param name="e">Données pour les événements liés au boutons</param>
        public static void onPressButton(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (sender as Grid);
            Border border = (grid.Children[0] as Border);
            border.Background = new SolidColorBrush(Color.FromRgb(204, 204, 255));
            border.BorderThickness = new Thickness(1, 1, 1, 1.75);
            border.Margin = new Thickness(0,0,1.25,0);
            border.Height -= 1.25;
        }

        public static void onReleaseButton(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (sender as Grid);
            Border border = (grid.Children[0] as Border);
            border.Background = new SolidColorBrush(Colors.White);
            border.BorderThickness = new Thickness(1, 1, 1, 3);
            border.Margin = new Thickness(0);
            border.Height += 1.25;
        }

    }
}