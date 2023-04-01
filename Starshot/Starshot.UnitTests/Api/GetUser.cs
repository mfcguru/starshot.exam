using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Features.GetUser;

namespace Starshot.UnitTests.Api
{
    [TestClass]
    public class GetUser
    {
        private int userId;
        private User expected;
        private DbContextOptions<DataContext> options;
        private DataContext context;

        [TestInitialize]
        public void TestInitialize()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new DataContext(options);
            expected = new User
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            };
            context.Users.Add(expected);
            context.SaveChanges();

            userId = expected.UserID;
        }

        [TestMethod]
        public async Task Success()
        {
            // act
            var command = new GetUserCommand(userId);
            var handler = new GetUserCommand.Handler(context);
            var actual = await handler.Handle(command, CancellationToken.None);

            // assert
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.TimeIn, actual.TimeIn);
            Assert.AreEqual(expected.TimeOut, actual.TimeOut);
            Assert.AreEqual(expected.Active, actual.Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task ResourceNotFound()
        {
            var command = new GetUserCommand(int.MaxValue);
            var handler = new GetUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);
        }
    }
}
