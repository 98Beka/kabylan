using AutoMapper;
using Kabylan.BLL.Services;
using KabylanMVC.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace KabylanMVC.PL.Controllers {
    public class SalesController : Controller {
        private readonly IMapper _mapper;
        private readonly SaleService _saleService;
        public SalesController(IMapper mapper, SaleService saleService) {
            _mapper = mapper;
            _saleService = saleService;
        }
        public ActionResult Index() {
            return View();
        }
        [HttpPost]
        public List<SaleViewModel> LoadTable() {
            var res = _mapper.Map<List<SaleViewModel>>(_saleService.GetAll());
            return res;
        }
    }
}
