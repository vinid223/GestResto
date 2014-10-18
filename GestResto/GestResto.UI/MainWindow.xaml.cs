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
            Configure();

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

    public static class Constante
    {
        public static Employe employe = new Employe();

        public static void Deconnexion()
        {
            employe = null;
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AuthentificationView>(new AuthentificationView());
        }

        public static void LogNavigation(string MessageDeNavigation)
        {
            string MessageHeader = String.Empty;
            // On définie la string avec le header
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

            StreamWriter file;
            file = File.AppendText(".\\Log\\LogNavigation.txt");
            file.WriteLine(MessageHeader);
            file.Flush();
            file.Close();

        }

        public static void LogErreur(string MessageDErreur)
        {
            string MessageHeader = String.Empty;
            // On définie la string avec le header
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
            StreamWriter file;
            file = File.AppendText(".\\Log\\LogErreur.txt");
            file.WriteLine(MessageHeader);
            file.Flush();
            file.Close();
        }
    }
}