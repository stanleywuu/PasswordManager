namespace PasswordManager.Security
{
    interface IStoreSecureData
    {
        void Store(object data);
        object Get(string key);
    }
}
