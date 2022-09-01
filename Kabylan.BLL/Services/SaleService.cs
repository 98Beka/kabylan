using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Infrastructure;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;
using Kabylan.DAL.Models;
using Kabylan.BLL.Profiles;

namespace Kabylan.BLL.Services {
    public class SaleService {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        public SaleService(string connectionString) {
            _database = new EFUnitOfWork(connectionString);
            _mapper = new MapperConfiguration(c => {
                c.AddProfile<MapperConfig>();
            }).CreateMapper();
        }
        public IEnumerable<SaleDTO> GetAll() {
            return _mapper.Map<IEnumerable<Sale>, List<SaleDTO>>(_database.Sales.GetAll());
        }

        public async Task<SaleDTO> GetAsync(int id) {
            var Sale = await _database.Sales.GetAsync(id);
            if (Sale == null)
                throw new ValidationException("Sale not found", "");
            return _mapper.Map<SaleDTO>(Sale);
        }


        public async Task<SaleDTO> Create() {
            var customer = new Customer();
            await _database.Customers.CreateAsync(customer);
            var apartment = new Apartment();
            await _database.Apartments.CreateAsync(apartment);
            _database.Save();
            var sale = new Sale() {
                Customer = customer,
                Apartment = apartment,
                PaydMonths = 1,
                SaleDate = DateTime.Today
            };
            await _database.Sales.CreateAsync(sale);
            _database.Save();
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task EditAsync(SaleDTO sale) {
            if (sale == null)
                throw new ValidationException("Sale = null", "");
            var oldSale = await _database.Sales.GetAsync(sale.Id);
            _mapper.Map(sale, oldSale.Customer);
            _mapper.Map(sale, oldSale.Apartment);
            _mapper.Map(sale, oldSale);
            _database.Save();
        }

        public async Task AddPayment(int moneyCount, int saleId) {
            var payment = new Payment() { MoneyCount = moneyCount, Date = DateTime.Today };
            if (payment == null)
                throw new ValidationException("Payment = null", "");
            var oldSale = await _database.Sales.GetAsync(saleId);
            if (oldSale == null)
                return;
            int sum = 0;
            foreach (var item in oldSale.Payments)
                sum += item.MoneyCount;
            if (sum + payment.MoneyCount > oldSale.Apartment.Price)
                payment.MoneyCount = oldSale.Apartment.Price - sum;
            if (payment.MoneyCount > 0)
                oldSale.Payments.Add(payment);
            _database.Save();
        }        
        
        public async Task RemovePayment(int paymentId, int saleId) {
            if (paymentId == 0 || saleId == 0)
                return;
            var oldSale = await _database.Sales.GetAsync(saleId);
            oldSale.Payments.Remove(oldSale.Payments.FirstOrDefault(p => p.Id == paymentId));
            _database.Save();
        }
        public async Task DeleteAsync(int id) {
            if (id == 0)
                return;
            var sale = await _database.Sales.GetAsync(id);
            if (sale == null)
                return;
            _database.Customers.Delete(sale.Customer.Id);
            _database.Save();
        }
    }
}
