using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;


namespace Kabylan.DAL.Repository {
    internal class ApartmentRepository : IRepository<Apartment> {
        private readonly ApplicationContext _db;
        public ApartmentRepository(ApplicationContext context) {
            _db = context;
        }

        public IQueryable<Apartment> GetAll() {
            return _db.Apartments;
        }

        public async Task<Apartment> GetAsync(int id) {
            return await _db.Apartments.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(Apartment Apartment) {
            await _db.Apartments.AddAsync(Apartment);
        }

        public void Update(Apartment Apartment) {
            _db.Entry(Apartment).State = EntityState.Modified;
        }

        public IEnumerable<Apartment> Find(Func<Apartment, Boolean> predicate) {
            return _db.Apartments.Where(predicate).ToList();
        }

        public void Delete(int id) {
            Apartment Apartment = _db.Apartments.Find(id);
            if (Apartment != null)
                _db.Apartments.Remove(Apartment);
        }
    }
}
