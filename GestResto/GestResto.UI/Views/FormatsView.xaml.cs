using GestResto.Logic.Model.Entities;
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
using System.Windows.Shapes;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FormatsView.xaml
    /// </summary>
    public partial class FormatsView : Window
    {
        public FormatItem formatItemChoisi;

        public FormatsView(IList<FormatItem> Formats)
        {
            InitializeComponent();

            lbxListeFormats.ItemsSource = Formats;
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            FormatItem format = (FormatItem)((sender as Button).CommandParameter);

            formatItemChoisi = format;

            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
