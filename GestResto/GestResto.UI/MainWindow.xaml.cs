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


            //ViewModel.CurrentView = new OptionsAdministrationView();
            //ViewModel.CurrentView = new CommandeView();
            //ViewModel.CurrentView = new CategorieView();
            //ViewModel.CurrentView = new BottomView();
            //ViewModel.CurrentView = new CommandeView();
            //ViewModel.CurrentView = new AuthentificationView();
           ViewModel.CurrentView = new AuthentificationView();
        }

        private void Configure()
        {
            //ServiceFactory.Instance.Register<IAuthentificationService, NHibernateAuthentificationService>(new NHibernateAuthentificationService());
            ServiceFactory.Instance.Register<ICategorieService, NHibertateCategorieService>(new NHibertateCategorieService());
            ServiceFactory.Instance.Register<IRestaurantService, NHibernateRestaurantService>(new NHibernateRestaurantService());
            ServiceFactory.Instance.Register<IItemService, NHibernateItemService>(new NHibernateItemService());
            ServiceFactory.Instance.Register<IFormatService, NHibernateFormatService>(new NHibernateFormatService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
            ServiceFactory.Instance.Register<ITypeEmployeService, NHibernateTypeEmployeService>(new NHibernateTypeEmployeService());
            //ServiceFactory.Instance.Register<IFormatItemService, NHibernateFormatItemService>(new NHibernateFormatItemService());
            
           
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
        }

    }
}