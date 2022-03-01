using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ValetCodingExercise.Controllers;
using ValetCodingExercise.Models;
using ValetCodingExercise.Entities;


namespace ValetCodingTests
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Data Source=DESKTOP-2NGTLCA;Initial Catalog=ValetDbExample;Integrated Security=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        context.AddRange(
                            new User { Username = "User1", Password = "test1", FirstName = "firstNameTest", LastName = "lastNameTest", Email = "user1@test.com" },
                            new User { Username = "User2", Password = "test2", FirstName = "firstNameTest", LastName = "lastNameTest", Email = "user2@test.com" });
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public ValetDbExampleContext CreateContext()
            => new ValetDbExampleContext(
                new DbContextOptionsBuilder<ValetDbExampleContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
