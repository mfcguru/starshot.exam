using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Features.EditUser;

namespace Starshot.UnitTests.Api
{
    [TestClass]
    public class EditUser
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
            var parameters = new EditUserParameters
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            };

            // act
            var command = new EditUserCommand(userId, parameters);
            var handler = new EditUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);

            // assert
            var updatedRecord = await context.Users.FindAsync(userId);
            Assert.AreEqual(updatedRecord.FirstName, parameters.FirstName);
            Assert.AreEqual(updatedRecord.LastName, parameters.LastName);
            Assert.AreEqual(updatedRecord.TimeIn, parameters.TimeIn);
            Assert.AreEqual(updatedRecord.TimeOut, parameters.TimeOut);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task ResourceNotFound()
        {
            var parameters = new EditUserParameters
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            };

            var command = new EditUserCommand(int.MaxValue, parameters);
            var handler = new EditUserCommand.Handler(context);
            await handler.Handle(command, CancellationToken.None);
        }
    }
}
