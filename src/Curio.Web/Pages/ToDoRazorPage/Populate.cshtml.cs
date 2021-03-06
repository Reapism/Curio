using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.ToDoRazorPage
{
    public class PopulateModel : PageModel
    {
        private readonly IRepository _repository;

        public PopulateModel(IRepository repository)
        {
            _repository = repository;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            // Used to be DatabasePopulator call
            RecordsAdded = 0;
        }
    }
}
