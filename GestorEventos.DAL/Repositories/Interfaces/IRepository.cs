using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GestorEventos.Models.Entities;

namespace GestorEventos.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> List(Expression<Func<T, bool>> filter = null);

        int Add(T entity);

        void AddRange(ICollection<T> entities);

        void Delete(int id);

        void DeleteRange(IEnumerable<T> entities);

        T FindById(int Id);

        void Update(T entity);

        void SaveChanges();

    }
}
