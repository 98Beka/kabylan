using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Interfaces;
using Kabylan.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kabylan.PL.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IService<UserDTO> _userService;
        private readonly IMapper _mapper;
        [BindProperty]
        public UserView User { get; set; }

        public int Number { get; set; }
        public CreateModel(IService<UserDTO> userService) {
            _userService = userService;
            _mapper = new MapperConfiguration(cnf => cnf.CreateMap<UserView, UserDTO>()).CreateMapper();
        }
        public void OnGet() {
        }
        public async Task<IActionResult> OnPostAsync() {
            if (ModelState.IsValid) {
                _userService.Create();
                //if (User.Status ==  (int)UserStatus.Employee)
                //    return RedirectToPage("Employee/MainEmployeePage");
                //if (User.Status == (int)UserStatus.Admin)
                //    return RedirectToPage("Admin/MainAdminPage");
            }
            return Page();
        }
    }
}
