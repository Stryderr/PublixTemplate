using Service.Models;

namespace Service.Interfaces
{
    public interface IGenericService
    {
        Task<List<GenericDM>> GetAll();
        Task<GenericDM> GetById(long id);
        Task<GenericDM> Add(GenericDM entity);
        Task<GenericDM> Update(GenericDM entity);
        Task<bool> Delete(long id);
    }
}
