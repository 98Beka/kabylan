using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Services;
using Kabylan.DAL.Interfaces;
using KabylanMVC.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace KabylanMVC.PL.Controllers {
    public class SalesController : Controller {
        private readonly SaleService _saleService;
        private readonly CustomerService _customerService;

        public SalesController(SaleService saleService, CustomerService customerService) {
            _saleService = saleService;
            _customerService = customerService;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult LoadTable([FromBody]DtParameters dtParameters) {
            int dtdraw = dtParameters.draw;
            int startRec = dtParameters.start;
            int pageSize = dtParameters.length;
            var totalResultsCount = _customerService.GetAll().Count();

            var _data = _customerService.GetAll()
                .Skip(startRec)
                .Take(pageSize)
                .ToList();
        
            return Json(new {
                draw = dtdraw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = _data
            });
        }

        [Route("Sales/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id) {
            if (id == null) {
                return NotFound();
            }
            var customer = await _customerService.GetAsync(id);

            if (customer == null) {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id) {
            _customerService.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public async Task<SaleDTO> GetSale(int id) {
            var customer = await _customerService.GetAsync(id);
            if (customer.Sale != null)
                return await _saleService.GetAsync(customer.Sale.Id);
            return null;
        }
    }
}
