using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.DAL.Models;

namespace Kabylan.BLL.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<Sale, SaleDTO>()
                .ForMember(m => m.CustomerFirstName, s => s.MapFrom(p => p.Customer.FirstName))
                .ForMember(m => m.CustomerMiddleName, s => s.MapFrom(p => p.Customer.MiddleName))
                .ForMember(m => m.CustomerLastName, s => s.MapFrom(p => p.Customer.LastName))
                .ForMember(m => m.Square, s => s.MapFrom(p => p.Apartment.Square))
                .ForMember(m => m.Price, s => s.MapFrom(p => p.Apartment.Price))
                .ForMember(m => m.RoomCount, s => s.MapFrom(p => p.Apartment.RoomCount));

            CreateMap<SaleDTO, Sale>();

            CreateMap<SaleDTO, Customer>()
                .ForMember(m => m.FirstName, s => s.MapFrom(p => p.CustomerFirstName))
                .ForMember(m => m.MiddleName, s => s.MapFrom(p => p.CustomerMiddleName))
                .ForMember(m => m.LastName, s => s.MapFrom(p => p.CustomerLastName));
            CreateMap<SaleDTO, Apartment>();
        }
    }
}
