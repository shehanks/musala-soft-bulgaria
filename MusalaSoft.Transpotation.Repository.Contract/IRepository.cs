using System.Linq.Expressions;

namespace MusalaSoft.Transpotation.Repository.Contract
{
    public interface IRepository<T> where T : class
    {
        bool Create(T obj);
        bool Delete(T obj);
        T Find(Expression<Func<T, bool>> predicate);
        List<T> FindList(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        int GetRecordsCount();
        bool Save();
        bool Update(T obj);
    }
}