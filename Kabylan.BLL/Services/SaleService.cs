using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Infrastructure;
using Kabylan.BLL.Interfaces;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;
using Kabylan.DAL.Models;
using Kabylan.BLL.Profiles;

namespace Kabylan.BLL.Services {
    public class SaleService : IService<SaleDTO> {
        private readonly IMapper _mapper;
        public SaleService(string connectionString) {
            Database = new EFUnitOfWork(connectionString);
            _mapper = new MapperConfiguration(c => {
                c.AddProfile<MapperConfig>();
            }).CreateMapper();
        }
        IUnitOfWork Database { get; set; }

        public async Task<SaleDTO> GetAsync(int id) {
            var Sale = await Database.Sales.Get(id);
            if (Sale == null)
                throw new ValidationException("Sale not found", "");
            return _mapper.Map<SaleDTO>(Sale);
        }

        public IEnumerable<SaleDTO> GetAll() {
            return _mapper.Map<IEnumerable<Sale>, List<SaleDTO>>(Database.Sales.GetAll());
        }

        public  SaleDTO Create() {
            var customer = new Customer();
            Database.Customers.Create(customer);
            customer.FirstName = "*";
            customer.MiddleName = "*";
            customer.LastName = "*";
            var apartment = new Apartment();
            Database.Apartments.Create(apartment);
            Database.Save();
            var sale = new Sale() {
                Customer = customer,
                Apartment = apartment,
                PaydMonths = 1,
                SaleDate = DateTime.Today
            };
            Database.Sales.Create(sale);
            Database.Save();
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task EditAsync(SaleDTO sale) {
            if (sale == null)
                throw new ValidationException("Sale = null", "");
            var oldSale = await Database.Sales.Get(sale.Id);
            _mapper.Map(sale.Customer, oldSale.Customer);
            _mapper.Map(sale.Apartment, oldSale.Apartment);
            oldSale.SaleDate = sale.SaleDate;
            oldSale.PaydMonths = sale.PaydMonths;
            Database.Save();
        }

        public async Task AddPayment(PaymentDTO payment, int saleId) {
            if (payment == null)
                throw new ValidationException("Payment = null", "");
            var oldSale = await Database.Sales.Get(saleId);
            if (oldSale == null)
                return;
            int sum = 0;
            foreach (var item in oldSale.Payments)
                sum += item.MoneyCount;
            if (sum + payment.MoneyCount > oldSale.Apartment.Price)
                payment.MoneyCount = oldSale.Apartment.Price - sum;
            if (payment.MoneyCount > 0)
                oldSale.Payments.Add(_mapper.Map<Payment>(payment));

            Database.Save();
        }        
        
        public async Task RemovePayment(int paymentId, int saleId) {
            if (paymentId == 0 || saleId == 0)
                return;
            var oldSale = await Database.Sales.Get(saleId);
            oldSale.Payments.Remove(oldSale.Payments.FirstOrDefault(p => p.Id == paymentId));
            Database.Save();
        }
        public async Task DeleteAsync(int id) {
            if (id == 0)
                return;
            var sale = await Database.Sales.Get(id);
            if (sale == null)
                return;
            Database.Apartments.Delete(sale.Apartment.Id);
            Database.Customers.Delete(sale.Customer.Id);
            foreach (var payment in sale.Payments)
                Database.Payments.Delete(payment.Id);
            Database.Sales.Delete(id);
            Database.Save();
        }
    }
}
