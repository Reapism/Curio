using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Curio.Web.Pages.ToDoRazorPage
{
    public class ZongModel : PageModel
    {
        private readonly IRepository repository;

        public IEnumerable<ToDoItem> Items { get; set; }

        public ZongModel(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task OnGetAsync()
        {
            Items = await repository.ListAsync<ToDoItem>();
        }
    }
}
