using Microsoft.EntityFrameworkCore;
using ParkingLot.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entityDbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entityDbSet = context.Set<T>();
        }

        public async Task AddAsync(T item)
        {
            await _entityDbSet.AddAsync(item);
        }
        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _entityDbSet.AddRangeAsync(items);
        }

        public void Delete(T entity)
        {
            _entityDbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _entityDbSet.RemoveRange(entities);
        }

        public void Update(T item)
        {
            _context.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

    }
}
