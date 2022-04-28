using Password_Manager.AccountStuff;
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
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public Action AddAccountCallback { get; set; }
        // callback скажет главной модели получить данный контекст и добавить новый аккаунт
        public AccountStructure AccountContext { get => this.DataContext as AccountStructure; } 
        public AddAccountWindow()
        {
            InitializeComponent();
            DataContext = new AccountStructure();
            ComboBox.SelectedItem = ComboBox.FindName("No Group");
        }

        public void Reset()
        {
            DataContext = new AccountStructure();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddAccountCallback?.Invoke();
                this.Close();
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.DataContext = new AccountStructure();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAccountCallback?.Invoke();
            this.Close();
        }
    }
}
