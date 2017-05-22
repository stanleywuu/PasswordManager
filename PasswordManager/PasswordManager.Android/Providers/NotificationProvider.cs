using Android.Content;
using Android.Widget;
using PasswordManager.UI;

namespace PasswordManager.Droid.Providers
{
    public class NotificationProvider : IProvideNotifications
    {
        Context FormContext;
        public NotificationProvider(Context context)
        {
            FormContext = context;
        }

        public void Notify(string title, string message)
        {
            Toast.MakeText(FormContext, message, ToastLength.Short).Show();
        }
    }
}