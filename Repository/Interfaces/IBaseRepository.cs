namespace Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task DeleteAsync(long id);
        Task SaveChangesAsync();
    }
}
