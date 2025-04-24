using AutoMapper;
using Domain.Models;
using Repository.Models;
using Repository.Repositories;

namespace Domain
{
    public interface IGenericDomain
    {
        Task<List<GenericDM>> GetAll();
        Task<GenericDM> GetById(long id);
        Task<GenericDM> Add(GenericDM entity);
        Task<GenericDM> Update(GenericDM entity);
        Task<bool> Delete(long id);
    }

    public class GenericDomain : IGenericDomain
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;

        public GenericDomain(IGenericRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }


        public async Task<List<GenericDM>> GetAll()
        {
            var result = await _repo.GetAll();
            return _mapper.Map<List<GenericDM>>(result);
        }

        public async Task<GenericDM> GetById(long id)
        {
            var result = await _repo.GetById(id);
            return _mapper.Map<GenericDM>(result);
        }

        public async Task<GenericDM> Add(GenericDM dm)
        {
            var entity = _mapper.Map<Generic>(dm);
            var result = await _repo.Add(entity);
            return _mapper.Map<GenericDM>(result);
        }

        public async Task<GenericDM> Update(GenericDM dm)
        {
            var existingEntity = await _repo.GetById(dm.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entity with id {dm.Id} not found.");

            var updatedEntity = _mapper.Map(dm, existingEntity);
            var result = await _repo.Update(updatedEntity);
            return _mapper.Map<GenericDM>(result);
        }

        public async Task<bool> Delete(long id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            var result = await _repo.Delete(entity);
            return true;
        }
    }
}
