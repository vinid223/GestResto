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
            listeBoutonEmploye.ItemsSource = ViewModel.Employes;
        }

        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Enregistrer");
        }
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Ajouter");
        }
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
            Constante.Deconnexion();
        }

        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }

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
            txtNom.IsEnabled = true;
            cbxType.IsEnabled = true;
            txtAdresse.IsEnabled = true;
            txtCP.IsEnabled = true;
            txtMDP.IsEnabled = true;
            txtNAS.IsEnabled = true;
            txtNum.IsEnabled = true;
            txtPrenom.IsEnabled = true;
            txtTauxHoraire.IsEnabled = true;
            txtTel.IsEnabled = true;
            txtVille.IsEnabled = true;
            chkActif.IsEnabled = true;
        }
    }
}
