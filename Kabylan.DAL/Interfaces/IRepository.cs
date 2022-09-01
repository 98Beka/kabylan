namespace Kabylan.DAL.Interfaces {
    public interface IRepository<T> where T : class {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        void Update(T item);
        void Delete(int id);
    }
}
