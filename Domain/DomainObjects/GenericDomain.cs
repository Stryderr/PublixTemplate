using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Repository.Entities;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;

namespace Domain.DomainObjects
{
    public class GenericDomain : BaseDomain, IGenericDomain
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUtilityLogger _logger;

        public GenericDomain(IGenericRepository repo, IMapper mapper, IUtilityLogger logger) : base(mapper, logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<List<GenericDM>> GetAll()
        {
            _logger.LogInformation("Starting to fetch all items.");
            try
            {
                var result = await _repo.GetAll();
                _logger.LogInformation($"Successfully fetched {result.Count} items.");
                return _mapper.Map<List<GenericDM>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all items.");
                throw;
            }
        }

        public async Task<GenericDM> GetById(long id)
        {
            _logger.LogInformation($"Starting to fetch item with ID {id}.");
            try
            {
                var result = await _repo.GetById(id);
                if (result == null)
                {
                    _logger.LogInformation($"Item with ID {id} not found.");
                    return null;
                }
                _logger.LogInformation($"Successfully fetched item with ID {id}.");
                return _mapper.Map<GenericDM>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the item with ID {id}.");
                throw;
            }
        }

        public async Task<GenericDM> Add(GenericDM dm)
        {
            _logger.LogInformation("Starting to add a new item.");
            try
            {
                var entity = _mapper.Map<Generic>(dm);
                var result = await _repo.Add(entity);
                _logger.LogInformation($"Successfully added item with ID {result.Id}.");
                return _mapper.Map<GenericDM>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new item.");
                throw;
            }
        }

        public async Task<GenericDM> Update(GenericDM dm)
        {
            _logger.LogInformation($"Starting to update item with ID {dm.Id}.");
            try
            {
                var existingEntity = await _repo.GetById(dm.Id);
                if (existingEntity == null)
                {
                    _logger.LogInformation($"Item with ID {dm.Id} not found.");
                    return null;
                }

                var updatedEntity = _mapper.Map(dm, existingEntity);
                var result = await _repo.Update(updatedEntity);
                _logger.LogInformation($"Successfully updated item with ID {result.Id}.");
                return _mapper.Map<GenericDM>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the item with ID {dm.Id}.");
                throw;
            }
        }

        public async Task<bool> Delete(long id)
        {
            _logger.LogInformation($"Starting to delete item with ID {id}.");
            try
            {
                var entity = await _repo.GetById(id);
                if (entity == null)
                {
                    _logger.LogInformation($"Item with ID {id} not found.");
                    return true;
                }

                var result = await _repo.Delete(entity);
                _logger.LogInformation($"Successfully deleted item with ID {id}.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the item with ID {id}.");
                throw;
            }
        }
    }
}
