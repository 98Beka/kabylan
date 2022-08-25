namespace Kabylan.BLL.Interfaces {
    public interface IService<T> {
        Task<T> GetAsync(int id);
        IEnumerable<T> GetAll();
        Task DeleteAsync(int id);
        Task EditAsync(T entity);
        T Create();
    }
}
