using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.DAL.Models;
using KabylanMVC.PL.ViewModels;

namespace KabylanMVC.PL.Profiles {
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<SaleViewModel, SaleDTO>();
            CreateMap<SaleDTO, SaleViewModel>();

            CreateMap<SaleViewModel, Customer>()
                .ForMember(c => c.Id, s => s.Ignore())
                .ForMember(c => c.Id, s => s.MapFrom(c => c.CustomerId))
                .ForMember(c => c.FirstName, s => s.MapFrom(c => c.CustomerFirstName))
                .ForMember(c => c.MiddleName, s => s.MapFrom(c => c.CustomerMiddleName))
                .ForMember(c => c.LastName, s => s.MapFrom(c => c.CustomerLastName));
            CreateMap<Customer, SaleViewModel>()
                .ForMember(s => s.Id, c => c.Ignore())
                .ForMember(s => s.CustomerId, c => c.MapFrom(s => s.Id))
                .ForMember(s => s.CustomerFirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(s => s.CustomerMiddleName, c => c.MapFrom(s => s.MiddleName))
                .ForMember(s => s.CustomerLastName, c => c.MapFrom(s => s.LastName));
        }
    }
}
