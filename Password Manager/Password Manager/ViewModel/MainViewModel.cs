using Password_Manager.AccountStuff;
using Password_Manager.Controls;
using Password_Manager.Utilites;
using Password_Manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Password_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    { 
        private ObservableCollection<AccountListItem> _Items = new ObservableCollection<AccountListItem>();
        public ObservableCollection<AccountListItem> Accounts 
        {
            get => _Items;
            set 
            {
                _Items = value; RaisePropertyChanged(); 
            } 
        }
        //динамическая коллекция для аккаунтов

        private int _selectedIndex;
        public int SelectedIndex 
        {
            get => _selectedIndex; set 
            {
                _selectedIndex = value; RaisePropertyChanged(); 
            } 
        }
        
        public AccountListItem SelectedAccountItem 
        {
            get 
            { 
                try 
                {
                    return Accounts[SelectedIndex]; 
                } 
                catch
                {
                    return null; 
                }
            }
        }
        public AccountStructure SelectedAccountStructure 
        {
            get 
            {
                try 
                {
                    return Accounts[SelectedIndex].DataContext as AccountStructure; 
                }
                catch 
                { 
                    return null; 
                } 
            } 
        } 

        public AddAccountWindow NewAccountWindow { get; set; }
        public EditAccountWindow EditAccountWindow { get; set; }



        public ICommand AddAccountCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }


        public MainViewModel() 
        {
            AddAccountCommand = new Command(ShowAddAccountWindow);
            EditAccountCommand = new Command(ShowEditAccountWindow);
            DeleteAccountCommand = new Command(DeleteAccount);

            NewAccountWindow = new AddAccountWindow();
            EditAccountWindow = new EditAccountWindow();
            NewAccountWindow.AddAccountCallback = this.AddAccount;
        }

        private void AddAccount() { AddAccount(NewAccountWindow.AccountContext); }
        private void AddAccount(AccountStructure acc) 
        {
            AccountListItem all = new AccountListItem();
            all.DataContext = acc;
            Accounts.Add(all);
        }
        private void ShowAddAccountWindow() { NewAccountWindow.Show(); }

        private void EditAccount() { }
        private void EditAccount(AccountStructure acc) { }
        private void ShowEditAccountWindow() { SetEditWindowContext(); EditAccountWindow.Show(); }
        private void SetEditWindowContext() { EditAccountWindow.DataContext = SelectedAccountStructure; }
        private void DeleteAccount() { }
    }
}
