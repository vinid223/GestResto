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
        private string NoIdentification = null;
        private string MDPIdentification = null;
        public EmployeViewModel ViewModel = new EmployeViewModel();
        private Employe employe;

        public AuthentificationView()
        {
            InitializeComponent();
        }

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            if (NoIdentification == null)
            {
                NoIdentification = txtAuthentification.Text.ToString();
                lblTitreText.Content = "Mot de passe:";
                txtAuthentification.Text = "";
            }
            else
            {
                if (MDPIdentification == null)
                {
                    MDPIdentification = txtAuthentification.Text.ToString();
                    lblTitreText.Content = "Numéro d'employé:";
                    txtAuthentification.Text = "";
                }

                NoIdentification = NoIdentification.Replace(" ", string.Empty);
                MDPIdentification = MDPIdentification.Replace(" ", string.Empty);

                if (NoIdentification != null && MDPIdentification != null && NoIdentification.Length >= 2 && MDPIdentification.Length >= 2 && NoIdentification.Length <= 4 && MDPIdentification.Length <= 4)
                {
                    employe = ViewModel.ObtenirEmployeAuthentification(NoIdentification,MDPIdentification);
                    if (employe == null)
                    {
                        MessageBox.Show("Les informations d'authentifications ne sont pas valide, veuillez ressayer", "Employé inexistant", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        NoIdentification = null;
                        MDPIdentification = null;
                    }
                    else if (employe.TypeEmployes == null)
                    {
                        MessageBox.Show("L'employe que vous tentez de connecter n'est pas valide. Connectez un administrateur pour corriger le problème", "Employé non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    }
                    else if(employe.TypeEmployes.IdTypeEmploye == 1)
                    {
                        IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                        mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
                    }
                    else if(employe.TypeEmployes.IdTypeEmploye == 2)
                    {
                        MessageBox.Show("L'employe est un serveur");
                    }
                    else
                    {
                        MessageBox.Show("L'employé identifié comporte un type inconnu. Veuillez entrer les informations de connexion a nouveau", "Type employé non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        NoIdentification = null;
                        MDPIdentification = null;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Les informations que vous avez ne sont pas valide, veuillez entrer à nouveau les informations", "Champ non valide", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    NoIdentification = null;
                    MDPIdentification = null;
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (txtAuthentification.Text.Length < 7)
            {
                string content = (sender as Button).Content.ToString();
                if (txtAuthentification.Text.Length != 0)
                {
                    txtAuthentification.Text += " ";
                }
                txtAuthentification.Text += content;
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            string ancienText = txtAuthentification.Text;
            if (ancienText.Length > 0)
            {
                string nouveauText;
                if (ancienText.Length == 1)
                {
                    nouveauText = ancienText.Substring(0, ancienText.Length - 1);
                }
                else
                {
                    nouveauText = ancienText.Substring(0, ancienText.Length - 2);
                }
            
                txtAuthentification.Text = nouveauText;   
            }
        }
    }
}
