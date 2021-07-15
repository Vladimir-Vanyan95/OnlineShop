using System;
using System.Collections.Generic;
using System.Text;
using Data.Models.Interfaces;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> FindById(int Id);
        void Update(T entity);
        Task Delete(int Id);
        Task Add(T entity);
        Task Save();
    }
}
