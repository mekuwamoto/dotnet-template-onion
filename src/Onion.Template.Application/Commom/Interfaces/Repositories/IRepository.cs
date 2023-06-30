namespace Onion.Template.Application.Commom.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task<string> UpdateAsync(T entity);
    Task<string> DeleteAsync(Guid id);
}
