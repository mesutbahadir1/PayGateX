namespace PayGateX.Service.Contracts;

public interface IService<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task<T> Create(T entity);
    Task<T> Update(int id, T entity);
    Task<T> Delete(int id);
}