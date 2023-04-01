using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Enums;
using Starshot.Api.Source.Domain.Features.GetUsers;

namespace Starshot.UnitTests.Api
{
    [TestClass]
    public class GetUsers
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

            context.Add(new User
            {
                FirstName = "First",
                LastName = "User",
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            });

            context.Add(new User
            {
                FirstName = "Second",
                LastName = "User",
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            });

            context.Add(new User
            {
                FirstName = "Third",
                LastName = "User",
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now,
                Active = false
            });

            context.Add(new User
            {
                FirstName = "Fourth",
                LastName = "User",
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now
            });

            context.SaveChanges();
        }

        [TestMethod]
        public async Task Sucess()
        {
            // act
            var command = new GetUsersCommand("", FilterActive.None);
            var handler = new GetUsersCommand.Handler(context);
            var actual = await handler.Handle(command, CancellationToken.None);

            // assert
            var expected = await context.Users.ToListAsync();
            Assert.AreEqual(actual.Count, expected.Count);
        }

        [TestMethod]
        public async Task FilterByName()
        {
            // arrange
            var filterKey = "Third";

            // act
            var command = new GetUsersCommand(filterKey, FilterActive.None);
            var handler = new GetUsersCommand.Handler(context);
            var actual = await handler.Handle(command, CancellationToken.None);

            // assert
            var expected = await context.Users
                .Where(o => 
                    o.FirstName.ToLower().Contains(filterKey.ToLower()) || 
                    o.LastName.ToLower().Contains(filterKey.ToLower()))
                .ToListAsync();

            Assert.AreEqual(actual.Count, expected.Count);
        }

        [TestMethod]
        public async Task FilterByActiveIsTrue()
        {
            // act
            var command = new GetUsersCommand("", FilterActive.True);
            var handler = new GetUsersCommand.Handler(context);
            var actual = await handler.Handle(command, CancellationToken.None);

            // assert
            var expected = await context.Users
                .Where(o => o.Active)
                .ToListAsync();

            Assert.AreEqual(actual.Count, expected.Count);
        }

        [TestMethod]
        public async Task FilterByActiveIsFalse()
        {
            // act
            var command = new GetUsersCommand("", FilterActive.False);
            var handler = new GetUsersCommand.Handler(context);
            var actual = await handler.Handle(command, CancellationToken.None);

            // assert
            var expected = await context.Users
                .Where(o => !o.Active)
                .ToListAsync();

            Assert.AreEqual(actual.Count, expected.Count);
        }
    }
}
