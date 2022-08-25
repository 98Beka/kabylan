using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.PL.ViewModels;

namespace Kabylan.PL.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<UserDTO, UserView>();
            CreateMap<UserView, UserDTO>();
            CreateMap<SaleDTO, SaleView>();
            CreateMap<SaleView, SaleDTO>();
            CreateMap<PersonDTO, PersonView>();
            CreateMap<PersonView, PersonDTO>();
            CreateMap<PaymentDTO, PaymentView>();
            CreateMap<PaymentView, PaymentDTO>().
                ForMember(p=>p.Id, y => y.Ignore());
            CreateMap<CustomerDTO, CustomerView>();
            CreateMap<CustomerView, CustomerDTO>();
            CreateMap<ApartmentDTO, ApartmentView>();
            CreateMap<ApartmentView, ApartmentDTO>();
        }
    }
}
