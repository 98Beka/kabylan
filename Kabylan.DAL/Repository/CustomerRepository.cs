using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Repository {
    internal class CustomerRepository : IRepository<Customer> {
        private readonly ApplicationContext _db;
        public CustomerRepository(ApplicationContext context) {
            _db = context;
        }

        public IQueryable<Customer> GetAll() {
            return _db.Customers;
        }

        public async Task<Customer> GetAsync(int id) {
            return await _db.Customers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(Customer customer) {
            await _db.Customers.AddAsync(customer);
        }

        public void Update(Customer customer) {
            _db.Entry(customer).State = EntityState.Modified;
        }

        public IEnumerable<Customer> Find(Func<Customer, Boolean> predicate) {
            return _db.Customers.Where(predicate).ToList();
        }

        public void Delete(int id) {
            Customer customer = _db.Customers.Find(id);
            if (customer != null)
                _db.Customers.Remove(customer);
        }
    }
}
