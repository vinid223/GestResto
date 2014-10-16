using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit.Services;
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
    /// Logique d'interaction pour AuthentificationView.xaml
    /// </summary>
    public partial class AuthentificationView : UserControl
    {
        private string NoIdentification = null;
        private string MDPIdentification = null;
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
                }
            }

            if (NoIdentification != null && MDPIdentification != null && NoIdentification != "" && MDPIdentification != "")
            {
                MessageBox.Show("On se connecte pour vérifier si l'employer existe");
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            if (txtAuthentification.Text.Length != 0)
            {
                txtAuthentification.Text += " ";
            }
            txtAuthentification.Text += content;
        }
    }
}
