using Ardalis.ApiEndpoints;
using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Curio.Web.Endpoints.ToDoItems
{
    public class Delete : BaseAsyncEndpoint<Guid, ToDoItemResponse>
    {
        private readonly IRepository _repository;

        public Delete(IRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("/ToDoItems/{id:int}")]
        [SwaggerOperation(
            Summary = "Deletes a ToDoItem",
            Description = "Deletes a ToDoItem",
            OperationId = "ToDoItem.Delete",
            Tags = new[] { "ToDoItemEndpoints" })
        ]
        public override async Task<ActionResult<ToDoItemResponse>> HandleAsync(Guid id)
        {
            var itemToDelete = await _repository.GetByIdAsync<ToDoItem>(id);
            if (itemToDelete == null) return NotFound();

            await _repository.DeleteAsync<ToDoItem>(itemToDelete);

            return NoContent();
        }
    }
}
