using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Interfaces;
using Kabylan.BLL.Services;
using Kabylan.DAL.Models;
using Kabylan.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Kabylan.PL.Pages.Employee
{
    public class MainEmployeePageModel : PageModel
    {
        private readonly SaleService _saleService;
        private readonly IService<UserDTO> _userService;
        private readonly IService<PaymentDTO> _paymentService;
        private readonly IMapper _mapper;
        public List<SaleView> Sales { get; set; }

        [BindProperty]
        public SaleView? Sale { get; set; }
        [BindProperty]
        public CustomerView? Customer { get; set; }
        [BindProperty]
        public CustomerView? SerachCustomer { get; set; }
        [BindProperty]
        public PaymentView? Payment { get; set; }
        [BindProperty]
        public ApartmentView? Apartment { get; set; }

        public MainEmployeePageModel(IMapper mapper, SaleService saleService, IService<UserDTO> userService) {
            _mapper = mapper;
            _saleService = saleService;
            _userService = userService;
            GetInfo();

        }
        private void GetInfo() {
            Sales = _mapper.Map<IEnumerable<SaleDTO>, List<SaleView>>(_saleService.GetAll());
            Sale = Sales.FirstOrDefault();
            if (Sale != null) {
                Customer = Sale?.Customer;
                Apartment = Sale?.Apartment;
                Payment = Sale?.Payments.FirstOrDefault();
            }
        }
        private void GetInfo(int saleId) {
            Sales = _mapper.Map<IEnumerable<SaleDTO>, List<SaleView>>(_saleService.GetAll());
            Sale = Sales.FirstOrDefault(s => s.Id == saleId);
            if(Sale != null) {
                Customer = Sale.Customer;
                Apartment = Sale.Apartment;
                Payment = Sale.Payments.FirstOrDefault();
            }
        }
        private void GetInfo(int saleId, int paymentId) {
            Sales = _mapper.Map<IEnumerable<SaleDTO>, List<SaleView>>(_saleService.GetAll());
            Sale = Sales.FirstOrDefault(s => s.Id == saleId);
            if (Sale != null) {
                Customer = Sale.Customer;
                Apartment = Sale.Apartment;
                Payment = Sale.Payments.FirstOrDefault(p => p.Id == paymentId);
            }
        }
        public void OnGet() =>
            GetInfo();

        public void OnGetShow(int saleId) =>
            GetInfo(saleId);

        public void OnPostShow(int saleId) =>
            GetInfo(saleId);

        public void OnPostShowPayment(int saleId, int paymentId) =>
            GetInfo(saleId, paymentId);
        public void OnPostFind(int saleId) {
            
            if (SerachCustomer == null || ((SerachCustomer.FirstName == "") && (SerachCustomer.MiddleName == "") && (SerachCustomer.LastName == "")))
                Sales = _mapper.Map<IEnumerable<SaleDTO>, List<SaleView>>(_saleService.GetAll());
            else
                Sales = _mapper.Map<IEnumerable<SaleDTO>, List<SaleView>>(_saleService.GetAll().Where(p => p.Customer.FirstName == SerachCustomer.FirstName ||
            p.Customer.MiddleName == SerachCustomer.MiddleName || p.Customer.LastName == SerachCustomer.LastName));
            Sale = Sales.FirstOrDefault();
            if (Sale != null) {
                Customer = Sale.Customer;
                Apartment = Sale.Apartment;
                Payment = Sale.Payments.FirstOrDefault();
            } else
                GetInfo(saleId);
        }
        public async Task OnPostSaveChangesAsync(int saleId) {
            if (saleId == 0)
                return ;
            var oldSale = await _saleService.GetAsync(saleId);
            _mapper.Map(Customer, oldSale.Customer);
            _mapper.Map(Apartment, oldSale.Apartment);
            oldSale.PaydMonths = Sale.PaydMonths;
            await _saleService.EditAsync(oldSale);
            _mapper.Map(oldSale, Sales.FirstOrDefault(p => p.Id == saleId));
            GetInfo(saleId);
            Response.Redirect($"/Employee/MainEmployeePage?saleId={saleId}&handler=show");
        }

        public async Task<IActionResult> OnPostAddAsync() {
            var id = _saleService.Create().Id;
            GetInfo(id);
            Response.Redirect($"/Employee/MainEmployeePage?saleId={id}&handler=show");
            return Page();
        }

        public async Task OnPostRemoveAsync(int saleId) {
            if (saleId == 0)
                return ;
            await _saleService.DeleteAsync(saleId);
            GetInfo();
            Response.Redirect($"/Employee/MainEmployeePage");
        }

        public async Task OnPostAddPaymentAsync(int saleId) {
            if (saleId == 0)
                return ;
            if (Payment?.MoneyCount > 0) {
                var payment = new PaymentDTO() { Date = Payment.Date, MoneyCount = Payment.MoneyCount };
                await _saleService.AddPayment(payment, saleId);
                GetInfo(saleId);
            }
            Response.Redirect($"/Employee/MainEmployeePage?saleId={saleId}&handler=show");
        }

        public async Task OnPostRemovePaymentAsync(int saleId, int paymentId) {
            if (saleId == 0)
                return;
            await _saleService.RemovePayment(paymentId, saleId);
            GetInfo(saleId);
            Response.Redirect($"/Employee/MainEmployeePage?saleId={saleId}&handler=show");
        }


    }
}
