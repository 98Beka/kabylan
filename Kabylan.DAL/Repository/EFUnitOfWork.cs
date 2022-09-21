using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Repository {
    public class EFUnitOfWork : IUnitOfWork {
        private readonly DbContextOptions<ApplicationContext> _dbContextOptions;
        public delegate int SaveDelegate();
        public event SaveDelegate SaveEvent;

        private ApplicationContext _db {
            get {
               var tmp =  new ApplicationContext(_dbContextOptions);
                SaveEvent += tmp.SaveChanges;
                return tmp;
            }
        }

        public EFUnitOfWork(string connectionString) {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _dbContextOptions = optionsBuilder.Options;
        }

        public IRepository<User> Users {
            get {
                return new UserRepository(_db);
            }
        }

        public IRepository<Apartment> Apartments {
            get {
                return new ApartmentRepository(_db); ;
            }
        }

        public IRepository<Sale> Sales {
            get {
                return new SaleRepository(_db); ;
            }
        }

        public IRepository<Payment> Payments {
            get {
                return new PaymentRepository(_db);
            }
        }
        
        public IRepository<Customer> Customers {
            get {
                return new CustomerRepository(_db); ;
            }
        } 

        public void Save() {
            SaveEvent?.Invoke();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
