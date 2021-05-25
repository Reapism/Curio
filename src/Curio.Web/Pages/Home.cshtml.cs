using System.Threading.Tasks;
using Curio.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages
{
    public class HomeViewModel : PageModel
    {
        public ISessionUser SessionUser { get; }

        public HomeViewModel(ISessionUser sessionUser)
        {
            SessionUser = sessionUser;
        }

        public async Task OnGet()
        {
        }
    }
}
