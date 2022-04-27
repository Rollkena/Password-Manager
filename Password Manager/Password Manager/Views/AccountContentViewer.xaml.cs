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
    /// Логика взаимодействия для AccountContentViewer.xaml
    /// </summary>
    public partial class AccountContentViewer : Window
    {
        public AccountStructure AccountContext { get => this.DataContext as AccountStructure; }
        public AccountContentViewer()
        {
            InitializeComponent();
        }

        private void CopyClipboardClicK(object sender, RoutedEventArgs e)
        {
            if (!AccountNull)
            {
                try
                {
                    switch (int.Parse(((Button)sender).Uid))
                    {
                        case 1: Clipboard.SetText(AccountContext.AccountName); break;
                        case 2: Clipboard.SetText(AccountContext.EmailAddress); break;
                        case 3: Clipboard.SetText(AccountContext.Username); break;
                        case 4: Clipboard.SetText(AccountContext.Password); break;
                        case 5: Clipboard.SetText(AccountContext.DateOfBirth); break;
                        case 6: Clipboard.SetText(AccountContext.SecurityInfo); break;
                        case 7: Clipboard.SetText(AccountContext.ExtraInfo1); break;
                        case 8: Clipboard.SetText(AccountContext.ExtraInfo2); break;
                        case 9: Clipboard.SetText(AccountContext.ExtraInfo3); break;
                        case 10: Clipboard.SetText(AccountContext.ExtraInfo4); break;
                        case 11: Clipboard.SetText(AccountContext.ExtraInfo5); break;
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to copy", "Err clipboard");
                }
                
            }
        }
        public bool AccountNull => AccountContext == null;

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
    }
}
