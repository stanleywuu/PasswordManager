using PasswordManager.IoCContainers;
using PasswordManager.Storage;
using PasswordManager.UI;
using Xamarin.Forms;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App
            (
                IKnowStorage storageProvider,
                ICopyToClipboard clipboardProvider,
                IProvideNotifications notificationProvider
            )
        {
            InitializeComponent();

            IoCProviders.Register<IKnowStorage>(storageProvider);
            IoCProviders.Register<ICopyToClipboard>(clipboardProvider);
            IoCProviders.Register<IProvideNotifications>(notificationProvider);

            MainPage = new PasswordManager.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            ((PasswordManager.MainPage)MainPage).ClearEnteredData();
        }
    }
}
