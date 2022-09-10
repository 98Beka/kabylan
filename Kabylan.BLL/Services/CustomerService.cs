using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Infrastructure;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;
using Kabylan.DAL.Models;
using Kabylan.BLL.Profiles;

namespace Kabylan.BLL.Services {
    public class CustomerService {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        public CustomerService(string connectionString) {
            _database = new EFUnitOfWork(connectionString);
            _mapper = new MapperConfiguration(c => {
                c.AddProfile<MapperConfig>();
            }).CreateMapper();
        }
        public IQueryable<Customer> GetAll() {
            return _database.Customers.GetAll();
        }

        public async Task<Customer> GetAsync(int id) {
            var customer = await _database.Customers.GetAsync(id);
            if (customer == null)
                throw new ValidationException("customer not found", "");
            return customer;
        }


        public async Task<Customer> CreateAsync() {
            var customer = new Customer();
            await _database.Customers.CreateAsync(customer);
            _database.Save();
            return customer;
        }

        public async Task Edit(Customer customer) {
            if (customer == null)
                throw new ValidationException("customer = null", "");
            var oldCustomer = await _database.Customers.GetAsync(customer.Id);
            if (oldCustomer == null)
                throw new ValidationException("oldCustomer = null", "");
            _mapper.Map(customer, oldCustomer);
            _database.Save();
        }

        public void Delete(int id) {
            if (id == 0)
                return;
            _database.Customers.Delete(id);
            _database.Save();
        }
    }
}
