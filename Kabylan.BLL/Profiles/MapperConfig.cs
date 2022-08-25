using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.DAL.Models;

namespace Kabylan.BLL.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<Sale, SaleDTO>();
            CreateMap<PersonDTO, Person>();
            CreateMap<Person, PersonDTO>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<Payment, PaymentDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<ApartmentDTO, Apartment>();
            CreateMap<Apartment, ApartmentDTO>();
        }
    }
}
