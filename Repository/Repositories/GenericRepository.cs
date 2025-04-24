
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;

namespace Repository.Repositories
{


    public class GenericRepository : BaseRepository, IGenericRepository
    {
        private readonly IUtilityLogger _logger;

        public GenericRepository(IUtilityLogger logger, GenericContext context) : base(context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Generic>> GetAll()
        {
            _logger.LogInformation("Starting to fetch all entities.");
            try
            {
                var result = await _context.Generics.ToListAsync();
                _logger.LogInformation($"Successfully fetched {result.Count} entities.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all entities.");
                throw;
            }
        }

        public async Task<Generic> GetById(long id)
        {
            _logger.LogInformation($"Starting to fetch entity with ID {id}.");
            try
            {
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the entity with ID {id}.");
                throw;
            }
        }

        public async Task<Generic> Add(Generic entity)
        {
            _logger.LogInformation("Starting to add a new entity.");
            try
            {
                if (entity == null)
                    return null;

                _context.Generics.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully added entity with ID {entity.Id}.");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new entity.");
                throw;
            }
        }

        public async Task<Generic> Update(Generic entity)
        {
            _logger.LogInformation($"Starting to update entity with ID {entity.Id}.");
            try
            {
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the entity with ID {entity.Id}.");
                throw;
            }
        }

        public async Task<bool> Delete(Generic entity)
        {
            _logger.LogInformation($"Starting to delete entity with ID {entity.Id}.");
            try
            {
                if (entity == null)
                    return true;

                _context.Generics.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully deleted entity with ID {entity.Id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the entity with ID {entity.Id}.");
                throw;
            }
        }
    }
}
