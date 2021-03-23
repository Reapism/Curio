using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.Login
{
    public class LoginParametersModel : PageModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string IsPhoneLogin { get; set; }

        public void OnGet()
        {
        }
    }
}
