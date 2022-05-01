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

namespace Password_Manager.Views
{
    /// <summary>
    /// Логика взаимодействия для EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow : Window
    {
        public EditAccountWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                this.Hide();

            }
            if (e.Key == Key.Escape) 
            {
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.DataContext = null;
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            this.Hide();
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Binding binding = new Binding;
        //    binding.ElementName
        //}
    }
}
