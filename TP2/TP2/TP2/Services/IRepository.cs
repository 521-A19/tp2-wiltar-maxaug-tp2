using System.Collections.Generic;
using TP2.Models.Entities;

namespace TP2.Services
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
        void DeleteAll();
        bool IsExisting(string login);
    }
}