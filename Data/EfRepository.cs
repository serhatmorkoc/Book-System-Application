using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BookSystemDatabaseContext _context;
        private DbSet<T> _entities;

        #region Ctor

        public EfRepository(BookSystemDatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        #endregion

        #region Regular

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async

        public async Task<IEnumerable<T>> TableAsync()
        {
            return  await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
