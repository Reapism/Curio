using System;
using System.Linq;
using System.Threading.Tasks;
using Curio.Domain.Entities;
using Curio.UnitTests;
using Xunit;

namespace Curio.IntegrationTests.Data
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task AddsItemAndSetsId()
        {
            var repository = GetRepository();
            var item = new ToDoItemBuilder().Build();

            await repository.AddAsync(item);

            var newItem = (await repository.ListAsync<ToDoItem>())
                            .FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id != Guid.Empty);
        }
    }
}
