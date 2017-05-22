using System;
using PasswordManager.Storage;

namespace PasswordManager.iOS.Providers
{
    internal class StorageProvider : IKnowStorage
    {
        public string GetStoragePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}