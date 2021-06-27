using Curio.Domain.Entities;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.About
{
    public class IndexModel : PageModel
    {
        public IndexModel(IRepository<UserPost> userPostRepository)
        {
            UserPostRepository = userPostRepository;
        }

        public IRepository<UserPost> UserPostRepository { get; }

        public void OnGet()
        {
        }
    }
}
