using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Repository {
    internal class CustomerRepository : IRepository<Customer> {
        private readonly ApplicationContext _db;
        public CustomerRepository(ApplicationContext context) {
            _db = context;
        }

        public IEnumerable<Customer> GetAll() {
            return _db.Customers;
        }

        public async Task<Customer> Get(int id) {
            return await _db.Customers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(Customer Customer) {
            await _db.Customers.AddAsync(Customer);
        }

        public void Update(Customer Customer) {
            _db.Entry(Customer).State = EntityState.Modified;
        }

        public IEnumerable<Customer> Find(Func<Customer, Boolean> predicate) {
            return _db.Customers.Where(predicate).ToList();
        }

        public void Delete(int id) {
            Customer Customer = _db.Customers.Find(id);
            if (Customer != null)
                _db.Customers.Remove(Customer);
        }
    }
}
