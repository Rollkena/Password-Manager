﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager.AccountStuff
{
    public class AccountStructure
    {
        public string AccountName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string SecurityInfo { get; set; }

        //extrainfo

        public string ExtraInfo1 { get; set; }
        public string ExtraInfo2 { get; set; }
        public string ExtraInfo3 { get; set; }
        public string ExtraInfo4 { get; set; }
        public string Group { get; set; }

    }
    //структура данных аккаутна
}
