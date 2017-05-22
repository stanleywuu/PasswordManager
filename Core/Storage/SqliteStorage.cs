using System;
using System.IO;
using Core.Storage.Entity;
using SQLite;

namespace Core.Storage
{
    public static class SqliteStorage
    {
        private static string StoragePath;
        private static SQLiteConnection conn;
        private static object locker = new object();

        public static void InitializeStorage(string folderPath, string filePath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            StoragePath = Path.Combine(folderPath, filePath);

            conn = new SQLiteConnection(StoragePath);
            conn.CreateTable<ServicesEntity>();
        }

        // TODO: Separate repository
        public static void Store(string service, PasswordRequirements req)
        {
            lock (locker)
            {
                InsertOrUpdate(service, req);
            }
        }

        public static TableQuery<TEntity> GetEntity<TEntity>()
            where TEntity: new()
        {
            return conn.Table<TEntity>();
        }

        private static void InsertOrUpdate(string service, PasswordRequirements req)
        {
            var requirementInt = Convert.ToInt32(req);

            var existingEntity = conn.Table<ServicesEntity>().FirstOrDefault(e => e.Service == service);

            if (existingEntity == null)
            {
                var newEntity = new ServicesEntity()
                {
                    Service = service,
                    Requirements = requirementInt
                };

                conn.Insert(newEntity);
            }
            else
            {
                existingEntity.Requirements = requirementInt;
                conn.Update(existingEntity);
            }
        }
    }
}
