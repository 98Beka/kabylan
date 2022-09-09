using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;


namespace Kabylan.DAL.Repository {
    internal class SaleRepository : IRepository<Sale> {
        private readonly ApplicationContext _db;
        public SaleRepository(ApplicationContext context) {
            _db = context;
        }

        public IQueryable<Sale> GetAll() {
            return _db.Sales;
        }

        public async Task<Sale> GetAsync(int id) {
            return await _db.Sales
                .Include(s => s.Apartment)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(Sale Sale) {
            await _db.Sales.AddAsync(Sale);
        }

        public void Update(Sale Sale) {
            _db.Entry(Sale).State = EntityState.Modified;
        }

        public IEnumerable<Sale> Find(Func<Sale, Boolean> predicate) {
            return _db.Sales.Where(predicate).ToList();
        }

        public void Delete(int id) {
            Sale Sale = _db.Sales.Find(id);
            if (Sale != null)
                _db.Sales.Remove(Sale);
        }
    }
}
