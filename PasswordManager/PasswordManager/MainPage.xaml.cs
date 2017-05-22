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
        PasswordRequestViewModel data;

        public MainPage()
        {
            InitializeComponent();
            data = new PasswordRequestViewModel();
            BindingContext = data;
            DefaultRequirements =
                PasswordRequirements.SymbolRequired
                | PasswordRequirements.UpperCaseRequired
                | PasswordRequirements.NumberRequired;
        }

        public void ClearEnteredData()
        {
            // Reset password for security reason
            data.MasterPassword = string.Empty;
            data.Password = string.Empty;
        }

        private void NewPassword_Clicked(object sender, EventArgs e)
        {
            var password = data.MasterPassword;
            var serviceName = ServicePersistor.GetServiceName(data.ServiceName);
            var requirement = PasswordRequirements.None;

            requirement |= data.IncludeNumbers ? PasswordRequirements.NumberRequired : PasswordRequirements.NoNumber;
            requirement |= data.IncludeSymbols ? PasswordRequirements.SymbolRequired : PasswordRequirements.NoSymbol;
            requirement |= data.IncludeUppercase ? PasswordRequirements.UpperCaseRequired : PasswordRequirements.None;

            GeneratePassword(password, serviceName, requirement);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var password = data.MasterPassword;
            var serviceName = ServicePersistor.GetServiceName(data.ServiceName);

            if (!String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(serviceName))
            {

                var encryptedMaster = ServicePersistor.GetEncryptedFileName(password);                
                var requirement = PasswordRequirements.None;

                SqliteStorage.InitializeStorage(IoCProviders.GetProvider<IKnowStorage>().GetStoragePath(), encryptedMaster);

                var serviceEntity = ServicePersistor.GetEncryptedSettings(serviceName);

                if (serviceEntity != null)
                {
                    requirement = (PasswordRequirements)serviceEntity.Requirements;

                    data.IncludeNumbers = (requirement & PasswordRequirements.NumberRequired) > 0;
                    data.IncludeSymbols = (requirement & PasswordRequirements.SymbolRequired) > 0;
                    data.IncludeUppercase = (requirement & PasswordRequirements.UpperCaseRequired) > 0;
                }
                else
                {
                    requirement |= data.IncludeNumbers ? PasswordRequirements.NumberRequired : PasswordRequirements.NoNumber;
                    requirement |= data.IncludeSymbols ? PasswordRequirements.SymbolRequired : PasswordRequirements.NoSymbol;
                    requirement |= data.IncludeUppercase ? PasswordRequirements.UpperCaseRequired : PasswordRequirements.None;
                }

                GeneratePassword(password, serviceName, requirement);                
            }
        }

        private void GeneratePassword(string password, string serviceName, PasswordRequirements requirement)
        {
            PasswordGenerator generator = new PasswordGenerator(requirement);

            data.Password = generator.GeneratePassword(password, serviceName, 10);

            // store settings
            ServicePersistor.Persist(serviceName, requirement);
            data.PasswordVisible = true;
            data.CreateNewPassword = true;

            IoCProviders.GetProvider<ICopyToClipboard>().CopyToClipboard(data.Password);
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
            data.PasswordVisible = false;
            data.CreateNewPassword = false;
        }

        private void ShowHide_Clicked(object sender, EventArgs e)
        {
            data.HideMasterPassword = !data.HideMasterPassword;
        }

        private void Requirement_Change(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.CommandParameter.ToString())
            {
                case "Number":
                    data.IncludeNumbers = !data.IncludeNumbers;
                    break;
                case "Symbol":
                    data.IncludeSymbols = !data.IncludeSymbols;
                    break;
                case "Uppercase":
                    data.IncludeUppercase = !data.IncludeUppercase;
                    break;
                default:
                    break;
            }
        }
    }
}
