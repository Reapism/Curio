using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.Login
{
    public class LoginParametersModel : PageModel
    {
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
