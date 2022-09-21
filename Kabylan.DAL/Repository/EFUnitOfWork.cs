﻿using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Repository {
    public class EFUnitOfWork : IUnitOfWork {

        private UserRepository _userRepository;
        private SaleRepository _saleRepository;
        private PaymentRepository _paymentRepository;
        private CustomerRepository _customerRepository;
        private ApartmentRepository _apartmentRepository;
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
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);
                return _userRepository;
            }
        }

        public IRepository<Apartment> Apartments {
            get {
                if (_apartmentRepository == null)
                    _apartmentRepository = new ApartmentRepository(_db);
                return _apartmentRepository;
            }
        }

        public IRepository<Sale> Sales {
            get {
                if (_saleRepository == null)
                    _saleRepository = new SaleRepository(_db);
                return _saleRepository;
            }
        }

        public IRepository<Payment> Payments {
            get {
                if (_paymentRepository == null)
                    _paymentRepository = new PaymentRepository(_db);
                return _paymentRepository;
            }
        }
        
        public IRepository<Customer> Customers {
            get {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_db);
                return _customerRepository;
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
