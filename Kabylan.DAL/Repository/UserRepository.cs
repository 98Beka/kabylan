using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;


namespace Kabylan.DAL.Repository {
    internal class UserRepository : IRepository<User> {
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext context) {
            _db = context;
        }

        public IQueryable<User> GetAll() {
            return _db.Users;
        }

        public async Task<User> GetAsync(int id) {
            return await _db.Users.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(User User) {
            await _db.Users.AddAsync(User);
        }

        public void Update(User User) {
            _db.Entry(User).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate) {
            return _db.Users.Where(predicate).ToList();
        }

        public void Delete(int id) {
            User User = _db.Users.Find(id);
            if (User != null)
                _db.Users.Remove(User);
        }
    }
}
