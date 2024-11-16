namespace Coffee.Application.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task Delete(T entity);
        Task SaveChangesAsync();
    }
}
