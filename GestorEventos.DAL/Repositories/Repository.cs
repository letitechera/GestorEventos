using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorEventos.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> ret = _dbset;
            return filter == null ? ret : ret.Where(filter);
        }

        public int Add(T entity)
        {
            var e = _dbset.Add(entity);
            SaveChanges();

            return e.Entity.Id;
        }

        public void AddRange(ICollection<T> entities)
        {
            _dbset.AddRange(entities);
            SaveChanges();
        }

        private void Delete(T entity)
        {
            var e = _context.Entry(entity).State == EntityState.Detached ? FindById(entity.Id) : entity;
            _dbset.Remove(e);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(List(null).Where(e => entities.Select(c => c.Id).Contains(e.Id)));
            SaveChanges();
        }

        public T FindById(int Id)
        {
            return _dbset.FirstOrDefault(t => t.Id == Id);
        }

        public void Update(T entity)
        {
            var oldEntity = FindById(entity.Id);
            if (oldEntity != null)
            {
                entity.CreatedDate = oldEntity.CreatedDate;
                entity.CreatedByName = oldEntity.CreatedByName;
                entity.CreatedById = oldEntity.CreatedById;
            }

            _dbset.Update(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
