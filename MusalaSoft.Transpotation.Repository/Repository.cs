using Microsoft.EntityFrameworkCore;
using MusalaSoft.Transpotation.DataAccess;
using MusalaSoft.Transpotation.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public bool Create(T obj)
        {
            try
            {
                _context.Set<T>().Add(obj);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T obj)
        {
            try
            {
                _context.Remove(obj);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int GetRecordsCount()
        {
            return _context.Set<T>().Count();
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T obj)
        {
            try
            {
                var edbstate = _context.Entry(obj);
                _context.Set<T>().Attach(obj);
                edbstate.State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
