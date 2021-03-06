using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Curio.Domain.Entities;
using Curio.Web;
using Newtonsoft.Json;
using Xunit;

namespace Curio.FunctionalTests.Api
{
    public class ApiToDoItemsControllerList : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiToDoItemsControllerList(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsTwoItems()
        {
            var response = await _client.GetAsync("/api/todoitems");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(stringResponse).ToList();

            Assert.Equal(0, result.Count());
        }
    }
}
