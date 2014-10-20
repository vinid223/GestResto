﻿using GestResto.MvvmToolkit.Services;
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
    /// Logique d'interaction pour EmployeView.xaml
    /// </summary>
    public partial class EmployeView : UserControl
    {
        public EmployeView()
        {
            InitializeComponent();
        }

        private void btnEnregistrer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Enregistrer");
        }
        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ajouter");
        }
        private void btnDeconnexion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cours
            Constante.Deconnexion();
        }

        private void btnRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<OptionsAdministrationView>(new OptionsAdministrationView());
        }
    
    
public  object licationService { get; set; }}
}
