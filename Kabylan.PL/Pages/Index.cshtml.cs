using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Interfaces;
using Kabylan.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kabylan.PL.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly IService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public IndexModel(ILogger<IndexModel> logger, IService<UserDTO> userService) {
            _userService = userService;
            _logger = logger;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserView>()).CreateMapper();
        }

        [BindProperty]
        public string Email { get; set; } = String.Empty;
        [BindProperty]
        public string Password { get; set; } = String.Empty;

        public void OnGet() {
            Response.Redirect("/Employee/MainEmployeePage");
        }
        
        public async Task<IActionResult> OnPost() {
            //var user = _userService.GetAll().FirstOrDefault(s => s.Email == Email);
            //if (user == null || user.Password != Password) {
            //    return Page();
            //}
            //if (user.Status == (int)UserStatus.Employee)
            //    return RedirectToPage("Employee/MainEmployeePage");
            //if (user.Status == (int)UserStatus.Admin)
            //    return RedirectToPage("Admin/MainAdminPage");

            return Page();
        }
    }
}