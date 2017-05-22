using System;
using PasswordManager.Storage;

namespace PasswordManager.Droid.Providers
{
    internal class StorageProvider : IKnowStorage
    {
        public string GetStoragePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}