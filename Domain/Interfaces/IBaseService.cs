namespace Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task DeleteAsync(long id);
    }
}
