namespace gawektree_back.Persistence.Interfaces
{
    public interface IAsyncRepository<T, ID>
    {
        Task<T> GetByIdAsync(ID id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
