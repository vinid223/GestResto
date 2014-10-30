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
    /// Logique d'interaction pour BottomView.xaml
    /// </summary>
    public partial class BottomView : UserControl
    {
        public BottomView()
        {
            InitializeComponent();


        }

        private void btnSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sélection");
        }

        private void btnJoindre_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Joindre");
        }

        private void btnDiviser_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Diviser");
        }

        private void btnImprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Imprimer");
        }

        private void btnSupprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Supprimer");
        }

        private void btnPayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Payer");
        }

        private void btnGenerer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Générer");
        }
        private void btnRapport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Rapport");
        }
        private void btnCreer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Créer");
        }
        private void btnEnregistrer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Enregistrer");
        }
        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ajouter");
        }
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Retour");
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

    }
}
