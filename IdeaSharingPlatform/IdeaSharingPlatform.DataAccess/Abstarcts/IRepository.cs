using System.Collections.Generic;

namespace IdeaSharingPlatform.DataAccess.Abstarcts
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetByID(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool DeleteByID(int id);
    }
}
