using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Services;
using Kabylan.DAL.Models;
using KabylanMVC.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace KabylanMVC.PL.Controllers {
    public class SalesController : Controller {
        private readonly SaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(SaleService saleService, IMapper mapper) {
            _saleService = saleService;
            _mapper = mapper;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            var sale = await _saleService.CreateAsync();
            sale.SaleDate = DateTime.Today;
            return RedirectToAction(nameof(Index), "Sales");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaleDTO sale) {
            if (sale.Id == 0)
                return BadRequest();
            sale.CustomerFirstName = sale.CustomerFirstName ?? string.Empty;
            sale.CustomerMiddleName = sale.CustomerMiddleName ?? string.Empty;
            sale.CustomerLastName = sale.CustomerLastName ?? string.Empty;
            await _saleService.EditAsync(sale);
            return RedirectToAction(nameof(Index), "Sales");
        }

        [HttpGet]
        public async Task<IActionResult> AddPaymnet(int id, int value) {
            if (id == 0 || value <= 0)
                return BadRequest();
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
            try {
                var totalResultsCount = _saleService.GetAllCustomers().Count();
                var _d = _saleService.GetAllCustomers().ToList();
                var _data = _saleService.GetAllCustomers()
                    .Skip(startRec)
                    .Take(pageSize)
                    .OrderByDescending(s => s.Id)
                    .ToList();

                return Json(new {
                    draw = dtdraw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = totalResultsCount,
                    data = _data
                });
            } catch (Exception ex) {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [Route("Sales/DeleteSale")]
        public async Task<IActionResult> DeleteSale(int id) {
            if (id == 0) {
                return NotFound();
            }
            var sale = await _saleService.GetAsync(id);

            if (sale == null) {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id) {
            var sale = await _saleService.GetAsync((int)id);
            if (sale != null) 
                await _saleService.DeleteAsync(sale.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<SaleViewModel> GetSale(int id, int customerId) {
            var sale =  await _saleService.GetAsync(id);

            var res = _mapper.Map<SaleViewModel>(sale);

            int totalPayment = 0;
            foreach (var payment in sale.Payments)
                totalPayment += payment.MoneyCount;
            res.TotalPayment = totalPayment;
            res.HaveToPay = res.Price - totalPayment;


            int difMonth = sale.PayingMonths - (Math.Abs((sale.SaleDate.Month - DateTime.Today.Month) + 12 * (sale.SaleDate.Year - DateTime.Today.Year)));
            if (difMonth <= 0)
                difMonth = 1;

            res.MonthPayment = res.HaveToPay / difMonth;
            return res;
        }
    }
}
