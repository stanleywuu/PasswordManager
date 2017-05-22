using System.ComponentModel;

namespace PasswordManager.ViewModels
{
    public class PasswordRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PasswordRequestViewModel()
        {
            HideMasterPassword = true;
            IncludeUppercase = IncludeNumbers = IncludeSymbols = true;
        }

        private string masterPassword;

        public string MasterPassword
        {
            get
            {
                return masterPassword;
            }
            set
            {
                if (masterPassword != value)
                {
                    masterPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MasterPassword"));
                }
            }
        }

        private string serviceName;
        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                if (serviceName != value)
                {
                    serviceName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ServiceName"));
                }
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }

        private bool passwordVisible;
        public bool PasswordVisible
        {
            get
            {
                return passwordVisible;
            }
            set
            {
                if (passwordVisible != value)
                {
                    passwordVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PasswordVisible"));
                }
            }
        }

        private bool hideMasterPassword;
        public bool HideMasterPassword
        {
            get
            {
                return hideMasterPassword;
            }
            set
            {
                if (hideMasterPassword != value)
                {
                    hideMasterPassword = value;
                    ShowOrHideMasterPasswordTitle = !HideMasterPassword ? "Hide master password" : "Show master password";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HideMasterPassword"));
                }
            }
        }

        private string showOrHideMasterPasswordTitle;
        public string ShowOrHideMasterPasswordTitle
        {
            get
            {
                return showOrHideMasterPasswordTitle;
            }
            set
            {
                if (showOrHideMasterPasswordTitle != value)
                {
                    showOrHideMasterPasswordTitle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowOrHideMasterPasswordTitle"));
                }
            }
        }

        public string includeUppercaseString;
        public string IncludeUppercaseString
        {
            get { return includeUppercaseString; }
            set
            {
                if (includeUppercaseString != value)
                {
                    includeUppercaseString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IncludeUppercaseString"));
                }
            }
        }

        public string includeSymbolsString;
        public string IncludeSymbolsString
        {
            get { return includeSymbolsString; }
            set
            {
                if (includeSymbolsString != value)
                {
                    includeSymbolsString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IncludeSymbolsString"));
                }
            }
        }

        public string includeNumbersString;
        public string IncludeNumbersString
        {
            get { return includeNumbersString; }
            set
            {
                if (includeNumbersString != value)
                {
                    includeNumbersString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IncludeNumbersString"));
                }
            }
        }

        private bool includeUppercase;
        public bool IncludeUppercase
        {
            get { return includeUppercase; }
            set
            {
                if (includeUppercase != value)
                {
                    includeUppercase = value;
                    IncludeUppercaseString = value ? "[x] Include Upper case" : "[ ] Include Upper case";
                }
            }
        }

        private bool includeSymbols;
        public bool IncludeSymbols
        {
            get { return includeSymbols; }
            set
            {
                if (includeSymbols != value)
                {
                    includeSymbols = value;
                    IncludeSymbolsString = value ? "[x] Include Symbols" : "[ ] Include Symbols";
                }
            }
        }

        private bool includeNumbers;
        public bool IncludeNumbers
        {
            get { return includeNumbers; }
            set
            {
                if (includeNumbers != value)
                {
                    includeNumbers = value;
                    IncludeNumbersString = value ? "[x] Include Numbers" : "[ ] Include Numbers";
                }
            }
        }

        public bool createNewPassword;
        public bool CreateNewPassword
        {
            get { return createNewPassword; }
            set
            {
                if (createNewPassword != value)
                {
                    createNewPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CreateNewPassword"));
                }
            }
        }
    }
}
