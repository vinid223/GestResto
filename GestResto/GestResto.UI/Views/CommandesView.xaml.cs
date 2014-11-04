using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour CommandesView.xaml
    /// </summary>
    public partial class CommandesView : UserControl
    {
        public CommandesViewModel ViewModel { get { return (CommandesViewModel)DataContext; } }
        private bool Erreur;

        public CommandesView()
        {
            InitializeComponent();

            try
            {
                DataContext = new CommandesViewModel();
            }
            catch (Exception e)
            {

                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des commandes :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des commandes: " + exceptionMessage);
                Erreur = true;
            }

            if (!Erreur)
            {
                // On boucle pour chaque commandes et on crée un bouton
                foreach (var item in ViewModel.Commandes)
                { 
                    // On se définie un nouveau bouton
                    Button bouton = new Button();
                    bouton.Click += btnDetail_Click;
                    bouton.Width = 115;
                    bouton.Height = 85;
                    bouton.Content = item.IdCommande;
                    bouton.Margin = new Thickness(5);

                    // On ajoute le bouton à notre wrappannel
                    listeBoutonCommandes.Children.Add(bouton);
                }
            }
        }

        private void btnRapport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Rapport");
        }

        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjoutCommandeView>(new AjoutCommandeView());
        }
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandeView>(new CommandeView());
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a test");
        }
    }
}
