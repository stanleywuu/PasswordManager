using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Storage
{
    public interface IKnowStorage
    {
        string GetStoragePath();
    }
}
