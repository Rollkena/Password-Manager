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
        //считывает индекс выбранного аккаунта


        private string _searchText;
        public string SearchText 
        {
            get => _searchText; set
            {
                _searchText = value; RaisePropertyChanged();
            }
        }
        //считывает текст по которому производится поиск


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
        //возвращает выбранный аккаунт
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
        //возвращает информацию выбранного аккаунта

        public AddAccountWindow NewAccountWindow { get; set; }
        public EditAccountWindow EditAccountWindow { get; set; }
        public AccountContentViewer AccountViewer { get; set; }
        //вызов окон

        public ICommand AddAccountCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand ShowAccountInfoCommand { get; set; }

        public ICommand SaveAccountsCommand { get; set; }
        public ICommand LoadAccountsCommand { get; set; }
        public ICommand SearchForAccountsCommand { get; set; }
        public ICommand SaveResultCommand { get; set; }

        public ICommand FilterNoGroup { get; set; }
        public ICommand FilterFavorites { get; set; }
        public ICommand FilterImportant { get; set; }
        public ICommand FilterNeedles { get; set; }
        
        //определение комманд



        public MainViewModel() 
        {
            LoadAccounts();
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

            FilterNoGroup = new Command(FNoGroup);
            FilterFavorites = new Command(FFavorites);
            FilterImportant = new Command(FImportant);
            FilterNeedles = new Command(FNeedless);
            //создание комманд через функции

            AccountDatabase.CreateDirThinghs();
            AccountDatabase.CreateDirThinghs2();
            //создание файлов 

            NewAccountWindow.AddAccountCallback = this.AddAccount;
            EditAccountWindow.SaveAccountCallback = this.SaveAccounts;
            EditAccountWindow.LoadAccountCallback = this.LoadAccounts;
        }


        private int isFilterOn = 0;
        private void FNoGroup()
        {
            try
            {
                Accounts.Clear();
                foreach (AccountStructure it in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    AddAccount(it);
                }
                isFilterOn = 0;
            }
            catch
            {
                MainWindow.Warning();
            }
        }
        private void FFavorites()
        {
            try
            {
                Accounts.Clear();
                foreach (AccountStructure accStr in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    if (accStr.Group.Contains("0")) { AddAccount(accStr); }
                }
                isFilterOn = 1;
            }
            catch
            {
                MainWindow.Warning();
            }
        }
        private void FImportant()
        {
            
            try
            {
                Accounts.Clear();
                foreach (AccountStructure accStr in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    if (accStr.Group.Contains("1")) { AddAccount(accStr); }
                }
                isFilterOn = 1;
            }
            catch
            {
                MainWindow.Warning();
            }
        }
        private void FNeedless()
        {
            
            try
            {
                Accounts.Clear();
                foreach (AccountStructure accStr in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    if (accStr.Group.Contains("2")) { AddAccount(accStr); }
                }
                isFilterOn = 1;
            }
            catch
            {
                MainWindow.Warning();
            }
        }

        //для фильтрации



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
        //сохранение результата поиска в папку, выбранную пользоватеелм

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
        //сохранение аккаунтов

        private void LoadAccounts()
        {
            try
            {

                Accounts.Clear();
                foreach (AccountStructure it in AccountDatabase.AccountLoadet.LoadFiles())
                {
                    AddAccount(it);
                }
            }
            catch
            {
                MainWindow.Warning();
            }
        }
        //загрузка аккаунтов из базы данных

        private void SearchForAccounts()
        {
            try
            {
                Accounts.Clear();
                if (SearchText != null)
                {
                    foreach (AccountStructure accStr in AccountDatabase.AccountLoadet.LoadFiles())
                    {
                        if (accStr.AccountName.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.EmailAddress.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.Username.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        //else if (accStr.Password.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.DateOfBirth.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.SecurityInfo.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.ExtraInfo1.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.ExtraInfo2.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.ExtraInfo3.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                        else if (accStr.ExtraInfo4.ToLower().Contains(SearchText)) { AddAccount(accStr); }
                    }
                }
            }
            catch
            {
                MainWindow.Warning();
            }
        }
        //работа поиску



        private void ShowAccountContentWindow() 
        {
            if (AccountIsSelected) 
            {
                SetAccountWindowContext(); AccountViewer.Show(); 
            } 
        }
        //отображение окна контекста выбранного аккаунта
        private void SetAccountWindowContext() 
        {
            AccountViewer.DataContext = SelectedAccountStructure; 
        }
        //считать инфомрацию в структуру


        private void AddAccount() 
        {
            LoadAccounts();
            AddAccount(NewAccountWindow.AccountContext);
            SaveAccounts();
        }
        private void AddAccount(AccountStructure acc) 
        {
            
            AccountListItem all = new AccountListItem();
            all.DataContext = acc;
            all.ShowContentWindowCallback = ShowAccountContentWindow;
            
            Accounts.Add(all);
            //SaveAccounts();
        }
        private void ShowAddAccountWindow() { NewAccountWindow.Show(); }
        //показ окна и последующее добавление аккаунта


        private void ShowEditAccountWindow() 
        {
            if ((SearchText == null || SearchText == "") && isFilterOn == 0)
            {
                SetEditWindowContext();
                EditAccountWindow.Show();
            }
            else
            {
                MainWindow.WarningAllowDelete();
            }
        }
        private void SetEditWindowContext() { EditAccountWindow.DataContext = SelectedAccountStructure; }
        //показ окна изменения. изменения происодят в реальном времени за счет динамической коллекции

        private void DeleteAccount() 
        {
            if ((SearchText == null || SearchText == "" ) && isFilterOn == 0)
            {
                MainWindow.Warning_Delete();
                if (MainWindow.SwitchDel == 1)
                {
                    if (SelectedIndex > -1)
                    {
                        Accounts.RemoveAt(SelectedIndex);
                    }
                    MainWindow.SwitchDel = 0;
                    SaveAccounts();
                }
            }
            else
            {
                MainWindow.WarningAllowDelete();
            }
        }
        //удаление аккаунтов с подтверждением от пользователя и сохранением в базу

        
    }
}
