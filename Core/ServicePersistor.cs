using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage;
using Core.Storage.Entity;

namespace Core
{
    public class ServicePersistor
    {
        public static void StoreService(string masterPassword, PasswordRequirements req)
        {
            var reqInt = Convert.ToInt32(req);

        }

        public static string GetServiceName(string rawServiceName)
        {
            if (String.IsNullOrEmpty(rawServiceName))
            {
                // throw exception?
                return String.Empty;
            }

            var processedServiceName = rawServiceName.ToLower().Trim();
            return processedServiceName.Split('.')[0];
        }

        public static string GetEncryptedFileName(string rawFile)
        {
            PasswordGenerator generator = new PasswordGenerator(PasswordRequirements.NoNumber | PasswordRequirements.NoSymbol);
            return generator.GeneratePassword(rawFile, "secret", 10);
        }

        public static ServicesEntity GetEncryptedSettings(string service)
        {
            var processedServiceName = GetServiceName(service); 
            var entity = SqliteStorage.GetEntity<ServicesEntity>().FirstOrDefault(x => x.Service.Equals(processedServiceName));
            return entity;
        }

        public static void Persist(string service, PasswordRequirements req)
        {
            SqliteStorage.Store(GetServiceName(service), req);
        }
    }
}
