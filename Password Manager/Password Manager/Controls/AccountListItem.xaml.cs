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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager.Controls
{
    /// <summary>
    /// Логика взаимодействия для AccountListItem.xaml
    /// </summary>
    public partial class AccountListItem : UserControl
    {

        public Action ShowContentWindowCallback { get; set; }

        public AccountStructure AccountContext { get => this.DataContext as AccountStructure; }
        //получение релевнтного контекста

        public AccountListItem()
        {
            InitializeComponent();
        }
        //инициализация контрола

        private void ButtonsClicked(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((Button)sender).Uid))
                
            {
                case 1: if (!AccountNull) Clipboard.SetText(AccountContext.EmailAddress); break; //email
                case 2: if (!AccountNull) Clipboard.SetText(AccountContext.Password); break; //password
                case 3: ShowContentWindowCallback?.Invoke(); break; //view
            }
        }
        //обработчик событий

        public bool AccountNull => AccountContext == null;

    }
}
