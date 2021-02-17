using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManager.Data.Abstract;
using TaskManager.Model;

namespace TaskManager.Data.Repositories {
    public class EntityBaseRepo<T> : IEntityBaseRepo<T> where T: class, IEntityBase, new() {

        private readonly TaskManagerContext _context;

        public EntityBaseRepo(TaskManagerContext context) {
            _context = context;
        }


        public void Add(T entity) {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public void Commit() {
            _context.SaveChanges();
        }

        public int Count() {
            return _context.Set<T>().Count();
        }

        public void Delete(T entity) {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate) {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities) {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate) {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll() {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetSingle(string id) {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate) {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public void Update(T entity) {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
