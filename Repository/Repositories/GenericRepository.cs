using AutoMapper;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;

namespace Repository.Repositories
{
    public class GenericRepository : BaseRepository<Generic>, IGenericRepository
    {
        public GenericRepository(GenericContext context, IMapper mapper, IUtilityLogger logger) : base(context, mapper, logger)
        {
        }
    }
}
