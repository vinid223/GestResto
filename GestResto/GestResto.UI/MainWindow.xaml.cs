using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
using GestResto.UI.ViewModel;
using GestResto.UI.Views;
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
            ServiceFactory.Instance.Register<ICategorieService, NHibertateCategorieService>(new NHibertateCategorieService());
            ServiceFactory.Instance.Register<IRestaurantService, NHibernateRestaurantService>(new NHibernateRestaurantService());
            ServiceFactory.Instance.Register<IItemService, NHibernateItemService>(new NHibernateItemService());
            ServiceFactory.Instance.Register<IFormatService, NHibernateFormatService>(new NHibernateFormatService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
            ServiceFactory.Instance.Register<ITypeEmployeService, NHibernateTypeEmployeService>(new NHibernateTypeEmployeService());
            
           
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
        }
    }

    /// <summary>
    /// Classe permettant d'accéder a des variables et des fonctions de base nécessaire au fonctionnement du programme
    /// </summary>
    public static class Constante
    {
        public static Employe employe = new Employe();

        /// <summary>
        /// Fonction permettant de déconnecter un utilisateur
        /// </summary>
        public static void Deconnexion()
        {
            employe = null;
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AuthentificationView>(new AuthentificationView());
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
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDeNavigation + "\n";
            }
            else if (employe.IdEmploye == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDeNavigation + "\n";
            }
            else
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: " + employe.Nom + ", " + employe.Prenom + " ------ " + MessageDeNavigation + "\n";
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
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDErreur + "\n";
            }
            else if (employe.IdEmploye == null)
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: Indéfinie ----- " + MessageDErreur + "\n";
            }
            else
            {
                MessageHeader = "-------" + DateTime.Now + "----- Utilisateur: " + employe.Nom + ", " + employe.Prenom + " ------ " + MessageDErreur + "\n";
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
    }
}