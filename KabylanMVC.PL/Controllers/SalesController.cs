using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Services;
using Kabylan.DAL.Models;
using KabylanMVC.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KabylanMVC.PL.Controllers {
    public class SalesController : Controller {
        private readonly SaleService _saleService;
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;
        public SalesController(SaleService saleService, CustomerService customerService, IMapper mapper) {
            _saleService = saleService;
            _customerService = customerService;
            _mapper = mapper;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            var customer = await _customerService.CreateAsync();
            var sale = await _saleService.CreateAsync();
            if (sale == null || customer == null)
                return Json(new { message = "Creating error" });
            var res = _mapper.Map<SaleViewModel>(sale);
            _mapper.Map(customer, res);

            return View("Edit", res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind]SaleViewModel sale) {
            await _saleService.EditAsync(_mapper.Map<SaleDTO>(sale));
            var customer = _mapper.Map<Customer>(sale);
            customer.SaleId = sale.Id;
            await _customerService.Edit(customer);
            return RedirectToAction(nameof(Index), "Sales");
        }

        [HttpGet]
        public async Task<IActionResult> AddPaymnet(int id, int value) {

            await _saleService.AddPayment(id, value);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> RemovePaymnetAsync(int saleId, int paymentId) {
            await _saleService.RemovePayment(saleId, paymentId);
            return Ok();
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
            if (id == 0) {
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
            var res =  await _saleService.GetAsync(id);
            return res;
        }
    }
}
