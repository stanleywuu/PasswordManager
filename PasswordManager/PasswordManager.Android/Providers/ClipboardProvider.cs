using Android.Content;
using PasswordManager.Storage;

namespace PasswordManager.Droid.Providers
{
    public class ClipboardProvider : ICopyToClipboard
    {
        Context FormContext;

        public ClipboardProvider(Context context)
        {
            FormContext = context;
        }

        public void CopyToClipboard(string value)
        {            
            ClipboardManager clipboard = (ClipboardManager)FormContext.GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("Clipboard", value);
            clipboard.PrimaryClip = clip;
        }
    }
}