using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValetCodingExercise.Controllers;
using ValetCodingExercise.Entities;
using Xunit;

namespace ValetCodingTests
{
    public class DevicesControllerTests : IClassFixture<TestDatabaseFixture>
    {
        public DevicesControllerTests(TestDatabaseFixture fixture)
            => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }

        [Fact]
        public async void GetUsersTest()
        {
            using var context = Fixture.CreateContext();
            var controller = new UsersController(context, null);
            var users = await controller.GetUsers();
            Assert.NotNull(users);
            Assert.Equal(2, users?.Value?.Count());
        }

        [Fact]
        public async void GetUserTest()
        {
            using var context = Fixture.CreateContext();
            var controller = new UsersController(context, null);
            var users = await controller.GetUser(1);
            Assert.NotNull(users.Value);
            Assert.Equal(1, users?.Value?.UserId);
            Assert.Equal("firstNameTest", users?.Value?.FirstName);
        }

        [Fact]
        public async void AddUserTest()
        {
            using var context = Fixture.CreateContext();
            var controller = new UsersController(context, null);
            var result = await controller.RegisterUser(new User { UserId = 3, Username = "User3", Password = "test3", FirstName = "firstNameTest", LastName = "lastNameTest", Email = "user3@test.com" });
            
            Assert.NotNull(result);
            var user = await controller.GetUser(3);
            Assert.Equal(3, user?.Value?.UserId);

            var fail = await controller.RegisterUser(new User { UserId = 3, Username = "User3", Password = "test3", FirstName = "firstNameTest", LastName = "lastNameTest", Email = "user3@test.com" });
            Assert.True("Username is already in use" != fail.Result.ToString());
        }
    }
}
