using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.DAL.Models;

namespace Kabylan.BLL.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<Sale, SaleDTO>()
                .ForMember(m => m.Square, s => s.MapFrom(p => p.Apartment.Square))
                .ForMember(m => m.Price, s => s.MapFrom(p => p.Apartment.Price))
                .ForMember(m => m.RoomCount, s => s.MapFrom(p => p.Apartment.RoomCount))
                .ForMember(m => m.CustomerFirstName, s => s.MapFrom(p => p.Customer.FirstName))
                .ForMember(m => m.CustomerMiddleName, s => s.MapFrom(p => p.Customer.MiddleName))
                .ForMember(m => m.CustomerLastName, s => s.MapFrom(p => p.Customer.LastName));

            CreateMap<SaleDTO, Sale>();

            CreateMap<Customer, Customer>();
            CreateMap<SaleDTO, Customer>()
                .ForMember(c => c.Id, s => s.Ignore())
                .ForMember(c => c.FirstName, s => s.MapFrom(c => c.CustomerFirstName))
                .ForMember(c => c.MiddleName, s => s.MapFrom(c => c.CustomerMiddleName))
                .ForMember(c => c.LastName, s => s.MapFrom(c => c.CustomerLastName));
            CreateMap<SaleDTO, Apartment>()
               .ForMember(a => a.Id, s => s.Ignore());
        }
    }
}
