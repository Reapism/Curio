using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;
using Curio.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curio.Web.Endpoints.ToDoItems
{
    public class GetById : EndpointAsyncBase
        .WithRequest<Guid>
        .WithResponse<ToDoItemResponse>
    {
        private readonly IRepository _repository;

        public GetById(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/ToDoItems/{id:int}")]
        [SwaggerOperation(
            Summary = "Gets a single ToDoItem",
            Description = "Gets a single ToDoItem by Id",
            OperationId = "ToDoItem.GetById",
            Tags = new[] { "ToDoItemEndpoints" })
        ]
        public override async Task<ActionResult<ToDoItemResponse>> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync<ToDoItem>(id);

            var response = new ToDoItemResponse
            {
                Id = item.Id,
                Description = item.Description,
                IsDone = item.IsDone,
                Title = item.Title
            };
            return Ok(response);
        }
    }
}
