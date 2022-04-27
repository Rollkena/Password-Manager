using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager.AccountStuff
{
    public static class AccountDatabase
    {

        //public static string CentralFolderPatch = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string CentralFolderPath = RootFolder + @"\PasswordManager DataBase";
        public static string SearchFolderPath = RootFolder + @"\PasswordManager Search";

        //public static string CentralFolderPatch = @"D:\PasswordManager";

        public static string AccNameName     = "AccName.txt";
        public static string EmailName       = "Email.txt";
        public static string UsernameName    = "Username.txt";
        public static string PasswordName    = "Password.txt";
        public static string DateOfbirthName = "DateOfBirth.txt";
        public static string SecurityName    = "SecurityInfo.txt";
        public static string ExtraIn1Name    = "ExtraInfo1.txt";
        public static string ExtraIn2Name    = "ExtraInfo2.txt";
        public static string ExtraIn3Name    = "ExtraInfo3.txt";
        public static string ExtraIn4Name    = "ExtraInfo4.txt";
        public static string ExtraIn5Name    = "ExtraInfo5.txt";


        public static class AccountSaver
        {
            //public static void SaveFiles(List<AccountStructure> accounts) { SaveFiles(accounts, CentralFolderPath); }
            public static void SaveFiles(List<AccountStructure> accounts, string directory) 
            {
                List<string> NewAccName     = new List<string>();
                List<string> NewEmail       = new List<string>();
                List<string> NewUsername    = new List<string>();
                List<string> NewPassword    = new List<string>();
                List<string> NewDateOfBirth = new List<string>();
                List<string> NewSecurityInf = new List<string>();
                List<string> NewExtraInfo1  = new List<string>();
                List<string> NewExtraInfo2  = new List<string>();
                List<string> NewExtraInfo3  = new List<string>();
                List<string> NewExtraInfo4  = new List<string>();
                List<string> NewExtraInfo5  = new List<string>();

                for (int i = 0; i < accounts.Count; i++)
                {
                        NewAccName.Add(accounts[i].AccountName);
                          NewEmail.Add(accounts[i].EmailAddress);
                       NewUsername.Add(accounts[i].Username);
                       NewPassword.Add(accounts[i].Password);
                    NewDateOfBirth.Add(accounts[i].DateOfBirth);
                    NewSecurityInf.Add(accounts[i].SecurityInfo);
                     NewExtraInfo1.Add(accounts[i].ExtraInfo1);
                     NewExtraInfo2.Add(accounts[i].ExtraInfo2);
                     NewExtraInfo3.Add(accounts[i].ExtraInfo3);
                     NewExtraInfo4.Add(accounts[i].ExtraInfo4);
                     NewExtraInfo5.Add(accounts[i].ExtraInfo5);
                }
                File.WriteAllLines(Path.Combine(directory, AccNameName), NewAccName);
                File.WriteAllLines(Path.Combine(directory, EmailName), NewEmail);
                File.WriteAllLines(Path.Combine(directory, UsernameName), NewUsername);
                File.WriteAllLines(Path.Combine(directory, PasswordName), NewPassword);
                File.WriteAllLines(Path.Combine(directory, DateOfbirthName), NewDateOfBirth);
                File.WriteAllLines(Path.Combine(directory, SecurityName), NewSecurityInf);
                File.WriteAllLines(Path.Combine(directory, ExtraIn1Name), NewExtraInfo1);
                File.WriteAllLines(Path.Combine(directory, ExtraIn2Name), NewExtraInfo2);
                File.WriteAllLines(Path.Combine(directory, ExtraIn3Name), NewExtraInfo3);
                File.WriteAllLines(Path.Combine(directory, ExtraIn4Name), NewExtraInfo4);
                File.WriteAllLines(Path.Combine(directory, ExtraIn5Name), NewExtraInfo5);
            }

        }

        public static class AccountLoadet
        {
            public static List<AccountStructure> LoadFiles() { return LoadFiles(CentralFolderPath); }
            public static List<AccountStructure> LoadFiles(string directory)
            {
                List<string> AccName     = File.ReadAllLines(Path.Combine(directory, AccNameName    )).ToList();
                List<string> Emil       = File.ReadAllLines(Path.Combine(directory, EmailName      )).ToList();
                List<string> Usrnm    = File.ReadAllLines(Path.Combine(directory, UsernameName   )).ToList();
                List<string> Psswrd    = File.ReadAllLines(Path.Combine(directory, PasswordName   )).ToList();
                List<string> DtOfBrth = File.ReadAllLines(Path.Combine(directory, DateOfbirthName)).ToList();
                List<string> SecrtyInf = File.ReadAllLines(Path.Combine(directory, SecurityName   )).ToList();
                List<string> Extrnf1   = File.ReadAllLines(Path.Combine(directory, ExtraIn1Name   )).ToList();
                List<string> Extrnf2   = File.ReadAllLines(Path.Combine(directory, ExtraIn2Name   )).ToList();
                List<string> Extrnf3   = File.ReadAllLines(Path.Combine(directory, ExtraIn3Name   )).ToList();
                List<string> Extrnf4   = File.ReadAllLines(Path.Combine(directory, ExtraIn4Name   )).ToList();
                List<string> Extrnf5   = File.ReadAllLines(Path.Combine(directory, ExtraIn5Name   )).ToList();

                List<AccountStructure> accounts = new List<AccountStructure>();

                for (int i = 0;i < AccName.Count; i++)
                {
                    AccountStructure am = new AccountStructure()
                    {
                        AccountName = AccName[i],
                        EmailAddress = Emil[i],
                        Username = Usrnm[i],
                        Password = Psswrd[i],
                        DateOfBirth = DtOfBrth[i],
                        SecurityInfo = SecrtyInf[i],
                        ExtraInfo1 = Extrnf1[i],
                        ExtraInfo2 = Extrnf2[i],
                        ExtraInfo3 = Extrnf3[i],
                        ExtraInfo4 = Extrnf4[i],
                        ExtraInfo5 = Extrnf5[i]
                    };
                    accounts.Add(am);
                }

                return accounts;
            }
        }



        public static void CreateDirThinghs()
        {
            if (!Directory.Exists(CentralFolderPath))
            {
                Directory.CreateDirectory(CentralFolderPath);
            }


            if(!File.Exists((Path.Combine(CentralFolderPath, AccNameName))))
            {
                File.Create(Path.Combine(CentralFolderPath, AccNameName));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, EmailName))))
            {
                File.Create(Path.Combine(CentralFolderPath, EmailName));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, UsernameName))))
            {
                File.Create(Path.Combine(CentralFolderPath, UsernameName));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, PasswordName))))
            {
                File.Create(Path.Combine(CentralFolderPath, PasswordName));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, DateOfbirthName))))
            {
                File.Create(Path.Combine(CentralFolderPath, DateOfbirthName));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, SecurityName))))
            {
                File.Create(Path.Combine(CentralFolderPath, SecurityName));
            }

            if (!File.Exists((Path.Combine(CentralFolderPath, ExtraIn1Name))))
            {
                File.Create(Path.Combine(CentralFolderPath, ExtraIn1Name));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, ExtraIn2Name))))
            {
                File.Create(Path.Combine(CentralFolderPath, ExtraIn2Name));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, ExtraIn3Name))))
            {
                File.Create(Path.Combine(CentralFolderPath, ExtraIn3Name));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, ExtraIn4Name))))
            {
                File.Create(Path.Combine(CentralFolderPath, ExtraIn4Name));
            }
            if (!File.Exists((Path.Combine(CentralFolderPath, ExtraIn5Name))))
            {
                File.Create(Path.Combine(CentralFolderPath, ExtraIn5Name));
            }
        }

        public static void CreateDirThinghs2()
        {
            if (!Directory.Exists(SearchFolderPath))
            {
                Directory.CreateDirectory(SearchFolderPath);
            }


            if (!File.Exists((Path.Combine(SearchFolderPath, AccNameName))))
            {
                File.Create(Path.Combine(SearchFolderPath, AccNameName));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, EmailName))))
            {
                File.Create(Path.Combine(SearchFolderPath, EmailName));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, UsernameName))))
            {
                File.Create(Path.Combine(SearchFolderPath, UsernameName));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, PasswordName))))
            {
                File.Create(Path.Combine(SearchFolderPath, PasswordName));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, DateOfbirthName))))
            {
                File.Create(Path.Combine(SearchFolderPath, DateOfbirthName));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, SecurityName))))
            {
                File.Create(Path.Combine(SearchFolderPath, SecurityName));
            }

            if (!File.Exists((Path.Combine(SearchFolderPath, ExtraIn1Name))))
            {
                File.Create(Path.Combine(SearchFolderPath, ExtraIn1Name));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, ExtraIn2Name))))
            {
                File.Create(Path.Combine(SearchFolderPath, ExtraIn2Name));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, ExtraIn3Name))))
            {
                File.Create(Path.Combine(SearchFolderPath, ExtraIn3Name));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, ExtraIn4Name))))
            {
                File.Create(Path.Combine(SearchFolderPath, ExtraIn4Name));
            }
            if (!File.Exists((Path.Combine(SearchFolderPath, ExtraIn5Name))))
            {
                File.Create(Path.Combine(SearchFolderPath, ExtraIn5Name));
            }
        }
    }
}
