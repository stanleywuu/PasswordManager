using System;
using Core;
using Core.Storage;

using PasswordManager.IoCContainers;
using PasswordManager.Storage;
using PasswordManager.UI;
using PasswordManager.ViewModels;
using Xamarin.Forms;

namespace PasswordManager
{
    public partial class MainPage : ContentPage
    {
        PasswordRequirements DefaultRequirements;
        PasswordRequestViewModel ViewModel;

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new PasswordRequestViewModel();
            BindingContext = ViewModel;

            DefaultRequirements =
                PasswordRequirements.SymbolRequired
                | PasswordRequirements.UpperCaseRequired
                | PasswordRequirements.NumberRequired;
        }

        public void ClearEnteredData()
        {
            // Reset password for security reason
            ViewModel.MasterPassword = string.Empty;
            ViewModel.Password = string.Empty;
        }

        private void NewPassword_Clicked(object sender, EventArgs e)
        {
            var password = ViewModel.MasterPassword;
            var serviceName = ServicePersistor.GetServiceName(ViewModel.ServiceName);
            var requirement = PasswordRequirements.None;

            requirement |= ViewModel.IncludeNumbers ? PasswordRequirements.NumberRequired : PasswordRequirements.NoNumber;
            requirement |= ViewModel.IncludeSymbols ? PasswordRequirements.SymbolRequired : PasswordRequirements.NoSymbol;
            requirement |= ViewModel.IncludeUppercase ? PasswordRequirements.UpperCaseRequired : PasswordRequirements.None;

            GeneratePassword(password, serviceName, requirement);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var password = ViewModel.MasterPassword;
            var serviceName = ServicePersistor.GetServiceName(ViewModel.ServiceName);

            if (!String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(serviceName))
            {
                // Use one set of database vs a new database for a different master password
                var encryptedMaster = ServicePersistor.GetEncryptedFileName("DFtIETN"); // ServicePersistor.GetEncryptedFileName(password);                
                var requirement = PasswordRequirements.None;

                SqliteStorage.InitializeStorage(IoCProviders.GetProvider<IKnowStorage>().GetStoragePath(), encryptedMaster);

                var serviceEntity = ServicePersistor.GetPasswordSettings(serviceName);

                if (serviceEntity != null)
                {
                    requirement = (PasswordRequirements)serviceEntity.Requirements;

                    ViewModel.IncludeNumbers = (requirement & PasswordRequirements.NumberRequired) > 0;
                    ViewModel.IncludeSymbols = (requirement & PasswordRequirements.SymbolRequired) > 0;
                    ViewModel.IncludeUppercase = (requirement & PasswordRequirements.UpperCaseRequired) > 0;
                }
                else
                {
                    requirement |= ViewModel.IncludeNumbers ? PasswordRequirements.NumberRequired : PasswordRequirements.NoNumber;
                    requirement |= ViewModel.IncludeSymbols ? PasswordRequirements.SymbolRequired : PasswordRequirements.NoSymbol;
                    requirement |= ViewModel.IncludeUppercase ? PasswordRequirements.UpperCaseRequired : PasswordRequirements.None;
                }

                GeneratePassword(password, serviceName, requirement);                
            }
        }

        private void GeneratePassword(string password, string serviceName, PasswordRequirements requirement)
        {
            PasswordGenerator generator = new PasswordGenerator(requirement);

            ViewModel.Password = generator.GeneratePassword(password, serviceName, 10);

            // store settings
            ServicePersistor.Persist(serviceName, requirement);
            ViewModel.PasswordVisible = true;
            ViewModel.CreateNewPassword = true;

            IoCProviders.GetProvider<ICopyToClipboard>().CopyToClipboard(ViewModel.Password);
            IoCProviders.GetProvider<IProvideNotifications>().Notify(null, "Password has been copied to clipboard");
        }

        private void Password_Changed(object sender, TextChangedEventArgs e)
        {
            ResetUI();
        }
        private void Service_Changed(object sender, TextChangedEventArgs e)
        {
            ResetUI();
        }

        private void ResetUI()
        {
            ViewModel.PasswordVisible = false;
            ViewModel.CreateNewPassword = false;
        }

        private void ShowHide_Clicked(object sender, EventArgs e)
        {
            ViewModel.HideMasterPassword = !ViewModel.HideMasterPassword;
        }

        private void Requirement_Change(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.CommandParameter.ToString())
            {
                case "Number":
                    ViewModel.IncludeNumbers = !ViewModel.IncludeNumbers;
                    break;
                case "Symbol":
                    ViewModel.IncludeSymbols = !ViewModel.IncludeSymbols;
                    break;
                case "Uppercase":
                    ViewModel.IncludeUppercase = !ViewModel.IncludeUppercase;
                    break;
                default:
                    break;
            }
        }
    }
}
