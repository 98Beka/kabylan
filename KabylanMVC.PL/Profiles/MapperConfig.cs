using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using KabylanMVC.PL.Models;

namespace KabylanMVC.PL.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<SaleDTO, SaleViewModel>();
            CreateMap<SaleViewModel, SaleDTO>();

        }
    }
}
