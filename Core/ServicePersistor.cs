using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage;
using Core.Storage.Entity;

namespace Core
{   
    /// <summary>
    /// Responsible for storing and retrieving service info
    /// </summary>
    public class ServicePersistor
    {
        /// <summary>
        /// Get a processed service name without extensions
        /// </summary>
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

        /// <summary>
        /// Encrypt filename
        /// </summary>
        public static string GetEncryptedFileName(string rawFile)
        {
            PasswordGenerator generator = new PasswordGenerator(PasswordRequirements.NoNumber | PasswordRequirements.NoSymbol);
            return generator.GeneratePassword(rawFile, "s3cr3td@t@b@535@lt", 10);
        }

        /// <summary>
        /// Retrieve ServiceEntity based on service name
        /// </summary>        
        public static ServicesEntity GetEncryptedSettings(string service)
        {
            var processedServiceName = GetServiceName(service); 
            var entity = SqliteStorage.GetEntity<ServicesEntity>().FirstOrDefault(x => x.Service.Equals(processedServiceName));
            return entity;
        }

        /// <summary>
        /// Store password requirement for a given service
        /// </summary>        
        public static void Persist(string service, PasswordRequirements req)
        {
            SqliteStorage.Store(GetServiceName(service), req);
        }
    }
}
