using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Features.DeleteUser;

namespace Starshot.UnitTests.Api
{
    [TestClass]
    public class DeleteUser
    {
        private int userId;
        private DbContextOptions<DataContext> options;
        private DataContext context;

        [TestInitialize]
        public void TestInitialize()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new DataContext(options);
            var user = new User
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            };
            context.Users.Add(user);
            context.SaveChanges();

            userId = user.UserID;
        }

        [TestMethod]
        public async Task Sucess()
        {
            // arrange
            var expected = (await context.Users.Where(o => o.Active).CountAsync()) - 1;

            // act
            var command = new DeleteUserCommand(userId);
            var handler = new DeleteUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);

            // assert
            var actual = await context.Users.Where(o => o.Active).CountAsync();
            Assert.AreEqual(expected, actual);

            var user = await context.Users.FindAsync(userId);
            Assert.IsFalse(user.Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task ResourceNotFound()
        {
            var command = new DeleteUserCommand(int.MaxValue);
            var handler = new DeleteUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);
        }
    }
}
