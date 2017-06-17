using System;
using PasswordManager.Storage;

namespace PasswordManager.Droid.Providers
{
    public class StorageProvider : IKnowStorage
    {
        public string GetStoragePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}