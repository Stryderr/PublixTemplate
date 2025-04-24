using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGenericDomain
    {
        Task<List<GenericDM>> GetAll();
        Task<GenericDM> GetById(long id);
        Task<GenericDM> Add(GenericDM entity);
        Task<GenericDM> Update(GenericDM entity);
        Task<bool> Delete(long id);
    }
}
