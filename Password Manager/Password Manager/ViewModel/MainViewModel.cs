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
using System.Windows.Forms;
using System.Windows.Input;

namespace Password_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public static string RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string CentralFolderPath = RootFolder + @"\PasswordManager DataBase";
        public static string SearchFolderPath = RootFolder + @"\PasswordManager Search";


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

        public string ErrorMessege;

        private int _selectedIndex;
        public int SelectedIndex 
        {
            get => _selectedIndex; set 
            {
                _selectedIndex = value; RaisePropertyChanged(); 
            } 
        }
        public bool AccountIsSelected { get => SelectedIndex > -1; }


        private string _searchText;
        public string SearchText 
        {
            get => _searchText; set
            {
                _searchText = value; RaisePropertyChanged();
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
        public AccountContentViewer AccountViewer { get; set; }


        public ICommand AddAccountCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand ShowAccountInfoCommand { get; set; }

        public ICommand SaveAccountsCommand { get; set; }
        public ICommand LoadAccountsCommand { get; set; }
        public ICommand SearchForAccountsCommand { get; set; }
        public ICommand SaveResultCommand { get; set; }



        public MainViewModel() 
        {
            AddAccountCommand = new Command(ShowAddAccountWindow);
            EditAccountCommand = new Command(ShowEditAccountWindow);
            DeleteAccountCommand = new Command(DeleteAccount);
            ShowAccountInfoCommand = new Command(ShowAccountContentWindow);

            SaveAccountsCommand = new Command(SaveAccounts);
            LoadAccountsCommand = new Command(LoadAccounts);
            SearchForAccountsCommand = new Command(SearchForAccounts);
            SaveResultCommand = new Command(SaveResult);


            NewAccountWindow = new AddAccountWindow();
            EditAccountWindow = new EditAccountWindow();
            AccountViewer = new AccountContentViewer();

            AccountDatabase.CreateDirThinghs();
            AccountDatabase.CreateDirThinghs2();

            NewAccountWindow.AddAccountCallback = this.AddAccount;
        }

        public string saveSearch;
        private void SaveResult()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = SearchFolderPath;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveSearch = fbd.SelectedPath;
            }
            List<AccountStructure> accs = new List<AccountStructure>();
            foreach (AccountListItem acc in Accounts)
            {
                AccountStructure accStr = acc.DataContext as AccountStructure;
                accs.Add(accStr);
            }
            AccountDatabase.AccountSaver.SaveFiles(accs, saveSearch);
        }

        private void SaveAccounts()
        {
            List<AccountStructure> accs = new List<AccountStructure>();
            foreach (AccountListItem acc in Accounts)
            {
                AccountStructure accStr = acc.DataContext as AccountStructure;
                accs.Add(accStr);
            }
            AccountDatabase.AccountSaver.SaveFiles(accs, CentralFolderPath);
        }
        private void LoadAccounts()
        {
            Accounts.Clear();
            foreach (AccountStructure it in AccountDatabase.AccountLoadet.LoadFiles())
            {
                AddAccount(it);
            }
        }

        private void SearchForAccounts()
        {
            Accounts.Clear();
            if (SearchText != null)
            {
                foreach (AccountStructure accStr in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    if (accStr.AccountName.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.EmailAddress.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.Username.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.Password.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.DateOfBirth.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.SecurityInfo.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.ExtraInfo1.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.ExtraInfo2.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.ExtraInfo3.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.ExtraInfo4.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    else if (accStr.ExtraInfo5.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                }
            }
        }


        private void ShowAccountContentWindow() 
        {
            if (AccountIsSelected) 
            {
                SetAccountWindowContext(); AccountViewer.Show(); 
            } 
        }
        private void SetAccountWindowContext() 
        {
            AccountViewer.DataContext = SelectedAccountStructure; 
        }
        private void AddAccount() 
        {
            AddAccount(NewAccountWindow.AccountContext); 
        }
        private void AddAccount(AccountStructure acc) 
        {
            AccountListItem all = new AccountListItem();
            all.DataContext = acc;
            all.ShowContentWindowCallback = ShowAccountContentWindow;
            
            Accounts.Add(all);
        }
        private void ShowAddAccountWindow() { NewAccountWindow.Show(); }

        //private void EditAccount() { }
        //private void EditAccount(AccountStructure acc) { }
        private void ShowEditAccountWindow() { SetEditWindowContext(); EditAccountWindow.Show(); }
        private void SetEditWindowContext() { EditAccountWindow.DataContext = SelectedAccountStructure; }
        private void DeleteAccount() 
        {
            if (SelectedIndex > -1) 
            {
                Accounts.RemoveAt(SelectedIndex); 
            }
        }


        
    }
}
