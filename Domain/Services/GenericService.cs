using AutoMapper;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;
using Service.Interfaces;
using Service.Models;

namespace Service.Services
{
    public class GenericService : BaseService<GenericSM>, IGenericService
    {
        private readonly IGenericRepository _repo;

        public GenericService(IBaseRepository<GenericSM> baseRepo, IGenericRepository repo, IMapper mapper, IUtilityLogger logger) : base(baseRepo, mapper, logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
    }
}
