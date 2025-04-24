
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.Repositories
{


    public class GenericRepository : BaseRepository, IGenericRepository
    {
        public GenericRepository(GenericContext context) : base(context)
        {
        }

        public async Task<List<Generic>> GetAll()
        {
            return await _context.Generics.ToListAsync();
        }

        public async Task<Generic> GetById(long id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            var entity = await _context.Generics.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            return entity;
        }

        public async Task<Generic> Add(Generic entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Generics.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<Generic> Update(Generic entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = _context.Generics.Find(entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entity with id {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existingEntity;
        }

        public async Task<bool> Delete(Generic entity)
        {
            _context.Generics.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
