using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    public interface ISqliteService<T> where T : class, new()
    {
        Task<T> Load();
        Task<T> Load(string id);
        Task InsertUpdate(T entity);
    }
}
