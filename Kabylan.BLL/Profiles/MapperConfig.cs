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
                .ForMember(m => m.RoomCount, s => s.MapFrom(p => p.Apartment.RoomCount));

            CreateMap<SaleDTO, Sale>();

            CreateMap<Customer, Customer>();
            CreateMap<SaleDTO, Customer>()
               .ForMember(c => c.Id, s => s.Ignore());
            CreateMap<SaleDTO, Apartment>()
               .ForMember(a => a.Id, s => s.Ignore());
        }
    }
}
