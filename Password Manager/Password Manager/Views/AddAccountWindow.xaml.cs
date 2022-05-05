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
        }

        public void Reset()
        {
            DataContext = new AccountStructure();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AccountContext.AccountName != null && AccountContext.AccountName.Replace(" ", "") != "")
                {
                    AddAccountCallback?.Invoke();
                    this.DataContext = null;
                    this.Close();
                }
                else
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Пожалуйста, заполните имя аккаунта", "Warning");
                }
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PassBox.FontFamily = new FontFamily("Segoe UI");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PassBox.FontFamily = new FontFamily("Segoe MDL2 Assets");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AccountContext.AccountName != null && AccountContext.AccountName.Replace(" ", "") != "")
            {
                AddAccountCallback?.Invoke();
                this.DataContext = null;
                this.Close();
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Пожалуйста, заполните имя аккаунта", "Warning");
            }
        }
    }
}
