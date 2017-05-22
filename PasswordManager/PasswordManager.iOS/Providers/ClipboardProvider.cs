using System;
using PasswordManager.Storage;

namespace PasswordManager.iOS.Providers
{
    public class ClipboardProvider : ICopyToClipboard
    {
        public void CopyToClipboard(string value)
        {
            throw new NotImplementedException();
        }
    }
}
