using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.Model;

namespace TaskManager.Data.Abstract {

    /*
     * Base Interface 
     * Describes the basic methods that will be useful for work with entities
     */

    public interface IEntityBaseRepo<T> where T: class, IEntityBase, new() {

        IEnumerable<T> AllIncluding(
          params Expression<Func<T, object>>[] includeProperties
        );

        IEnumerable<T> GetAll();

        int Count();

        T GetSingle(string id);

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingle(
          Expression<Func<T, bool>> predicate,
          params Expression<Func<T, object>>[] includeProperties
        );

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();

    }
}
