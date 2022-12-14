using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kabylan.DAL.Models;

namespace Kabylan.DAL.Interfaces {
    public interface IUnitOfWork : IDisposable {
        IRepository<Sale> Sales { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Payment> Payments { get; }
        IRepository<Apartment> Apartments { get; }
        void Save();
    }
}
