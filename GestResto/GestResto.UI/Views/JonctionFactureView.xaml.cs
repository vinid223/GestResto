using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
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
    /// Logique d'interaction pour JonctionFactureView.xaml
    /// </summary>
    public partial class JonctionFactureView : UserControl
    {
        public JonctionFactureView()
        {
            InitializeComponent();
        }

        private void btnCreer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Créer");
        }

        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btnRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO : Attention, il faut ici s'assurer qu'on retourne à la commande en cours
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<DivisionFactureView>(new DivisionFactureView());
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Joindre la client");
        }
    }
}
