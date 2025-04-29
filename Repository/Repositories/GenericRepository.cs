using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;

namespace Repository.Repositories
{
    public class GenericRepository : BaseRepository, IGenericRepository
    {
        public GenericRepository(IUtilityLogger logger, GenericContext context) : base(context, logger)
        {
        }

        public async Task<List<Generic>> GetAll()
        {
            _logger.LogInformation("Starting to fetch all entities.");

            return await ExecuteWithLoggingAsync(async () =>
            {
                var result = await _context.Generics.ToListAsync();
                _logger.LogInformation($"Successfully fetched {result.Count} entities.");
                return result;
            }, "An error occurred while fetching all entities.");
        }

        public async Task<Generic?> GetById(long id)
        {
            return await ExecuteWithLoggingAsync(async () =>
            {
                _logger.LogInformation($"Starting to fetch entity with ID {id}.");

                if (id <= 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

                var entity = await _context.Generics.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogInformation($"Entity with ID {id} not found.");
                    return null;
                }

                _logger.LogInformation($"Successfully fetched entity with ID {id}.");
                return entity;
            }, $"An error occurred while fetching the entity with ID {id}.");
        }

        public async Task<Generic?> Add(Generic entity)
        {
            return await ExecuteWithLoggingAsync(async () =>
            {
                _logger.LogInformation("Starting to add a new entity.");

                if (entity == null)
                    return null;

                _context.Generics.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully added entity with ID {entity.Id}.");
                return entity;
            }, "An error occurred while adding a new entity.");
        }

        public async Task<Generic?> Update(Generic entity)
        {
            return await ExecuteWithLoggingAsync(async () =>
            {
                _logger.LogInformation($"Starting to update entity with ID {entity.Id}.");

                if (entity == null)
                    return null;

                var existingEntity = await _context.Generics.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    _logger.LogInformation($"Entity with ID {entity.Id} not found.");
                    return null;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully updated entity with ID {entity.Id}.");
                return existingEntity;
            }, $"An error occurred while updating the entity with ID {entity.Id}.");
        }

        public async Task<bool> Delete(Generic entity)
        {
            return await ExecuteWithLoggingAsync(async () =>
            {
                _logger.LogInformation($"Starting to delete entity with ID {entity.Id}.");

                if (entity == null)
                    return true;

                _context.Generics.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully deleted entity with ID {entity.Id}.");
                return true;
            }, $"An error occurred while deleting the entity with ID {entity.Id}.");
        }
    }
}
