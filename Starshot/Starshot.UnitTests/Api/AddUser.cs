using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Features.AddUser;

namespace Starshot.UnitTests.Api
{
    [TestClass]
    public class AddUser
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        [TestInitialize]
        public void TestInitialize()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new DataContext(options);
        }

        [TestMethod]
        public async Task Sucess()
        {
            // arrange
            var userCount = await context.Users.CountAsync();
            var parameters = new AddUserParameters
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            };
            
            // act
            var command = new AddUserCommand(parameters);
            var handler = new AddUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);

            // assert
            var expected = userCount + 1;
            var actual = await context.Users.CountAsync();
            Assert.AreEqual(expected, actual);
        }
    }
}
