using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Repository {
    internal class PaymentRepository : IRepository<Payment> {
        private readonly ApplicationContext _db;
        public PaymentRepository(ApplicationContext context) {
            _db = context;
        }

        public IQueryable<Payment> GetAll() {
            return _db.Payments;
        }

        public async Task<Payment> Get(int id) {
            return await _db.Payments.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(Payment Payment) {
            await _db.Payments.AddAsync(Payment);
        }

        public void Update(Payment Payment) {
            _db.Entry(Payment).State = EntityState.Modified;
        }

        public IEnumerable<Payment> Find(Func<Payment, Boolean> predicate) {
            return _db.Payments.Where(predicate).ToList();
        }

        public void Delete(int id) {
            Payment Payment = _db.Payments.Find(id);
            if (Payment != null)
                _db.Payments.Remove(Payment);
        }
    }
}
