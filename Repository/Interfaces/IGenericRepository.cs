using Repository.Entities;
using Repository.Repositories;

namespace Repository.Interfaces
{
    public interface IGenericRepository : IBaseRepository
    {
        Task<List<Generic>> GetAll();
        Task<Generic> GetById(long id);
        Task<Generic> Add(Generic entity);
        Task<Generic> Update(Generic entity);
        Task<bool> Delete(Generic entity);
    }
}
