using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.Login
{
    public class LoginParametersModel : PageModel
    {
        private readonly ILoginService loginService;

        public LoginParametersModel(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [Required]
        [BindProperty]
        public string Username { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPhoneLogin { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Username.Contains("reap") && Password.Length > 0)
            {
                HttpContext.Session.SetString("username", Username);
                await HttpContext.Session.CommitAsync();
                return RedirectToPage("Welcome");
            }

            Message = "Invalid login! ";
            return Page();
        }
    }
}
