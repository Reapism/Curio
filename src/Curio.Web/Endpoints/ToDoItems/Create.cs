using System.Threading;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;
using Curio.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curio.Web.Endpoints.ToDoItems
{
    public class Create : EndpointBaseAsyncBuilder
        .WithRequest<NewToDoItemRequest>
        .WithResponse<ToDoItemResponse>
    {
        private readonly IRepository _repository;

        public Create(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("/ToDoItems")]
        [SwaggerOperation(
            Summary = "Creates a new ToDoItem",
            Description = "Creates a new ToDoItem",
            OperationId = "ToDoItem.Create",
            Tags = new[] { "ToDoItemEndpoints" })
        ]
        public override async Task<ActionResult<ToDoItemResponse>> HandleAsync(
            NewToDoItemRequest request,
            CancellationToken cancellationToken = default)
        {
            var item = new ToDoItem
            {
                Title = request.Title,
                Description = request.Description
            };

            var createdItem = await _repository.AddAsync(item);

            return Ok(createdItem);
        }
    }
}
