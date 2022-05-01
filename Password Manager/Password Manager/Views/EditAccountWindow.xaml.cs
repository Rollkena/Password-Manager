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
    /// Логика взаимодействия для EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow : Window
    {

        public AccountStructure AccountContext { get => this.DataContext as AccountStructure; }
        public Action SaveAccountCallback { get; set; }
        public Action LoadAccountCallback { get; set; }
        public EditAccountWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                if (AccountContext.AccountName != null && AccountContext.AccountName.Replace(" ", "") != "")
                {
                    SaveAccountCallback?.Invoke();
                    this.DataContext = null;
                    this.Hide();
                }
                else
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Пожалуйста, заполните имя аккаунта", "Warning");
                }

            }
            if (e.Key == Key.Escape) 
            {
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        
        {
            
            if (AccountContext.AccountName != null && AccountContext.AccountName.Replace(" ", "") != "")
            {
                SaveAccountCallback?.Invoke();
                this.DataContext = null;
                this.Hide();
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Пожалуйста, заполните имя аккаунта", "Warning");
            }
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
            LoadAccountCallback?.Invoke();
            this.Hide();
        }
    }
}
