namespace Application.Interface
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> All();
    }
}
