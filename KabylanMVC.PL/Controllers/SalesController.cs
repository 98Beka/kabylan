using AutoMapper;
using Kabylan.BLL.Services;
using Kabylan.DAL.Interfaces;
using KabylanMVC.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace KabylanMVC.PL.Controllers {
    public class SalesController : Controller {
        private readonly IMapper _mapper;
        private readonly SaleService _saleService;
        private readonly IUnitOfWork _unitOfWork;

        public SalesController(IMapper mapper, SaleService saleService, IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _saleService = saleService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult LoadTable([FromBody]DtParameters dtParameters) {
            int dtdraw = dtParameters.draw;
            int startRec = dtParameters.start;
            int pageSize = dtParameters.length;
            var totalResultsCount = _unitOfWork.Customers

            var _data = _mapper.Map<List<SaleViewModel>>(
                _context.Users
                    .Skip(startRec)
                    .Take(pageSize)
                    .ToList()
            );

            return Json(new {
                draw = dtdraw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = _data
            });
        }

        [HttpGet]
        public async Task<SaleViewModel> GetSale(int id) {
            var customer = await _context.Customers.FindAsync(id);
            return _mapper.Map<SaleViewModel>( await _saleService.GetAsync(customer.Sale.Id));
        }
    }
}
