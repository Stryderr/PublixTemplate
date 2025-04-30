using AutoMapper;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _baseRepo;
        protected readonly IMapper _mapper;
        protected readonly IUtilityLogger _logger;

        public BaseService(IBaseRepository<T> repo, IMapper mapper, IUtilityLogger logger)
        {
            _baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappingProfile>()).CreateMapper();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<List<T>> GetAllAsync() => _baseRepo.GetAllAsync();
        public Task<T> GetByIdAsync(long id) => _baseRepo.GetByIdAsync(id);
        public Task AddAsync(T entity) => _baseRepo.AddAsync(entity);
        public Task DeleteAsync(long id)
        {
            _logger.LogInformation($"Deleting entity with ID: {id}");
            return _baseRepo.DeleteAsync(id);
        }
    }
}
