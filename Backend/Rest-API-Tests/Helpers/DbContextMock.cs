using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Rest_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rest_API_Tests.Helpers
{
    public static class DbContextMock
    {
        public static Mock<AppDbContext> GetAppDbContextMock()
        {
            var appDbContextMock = new Mock<AppDbContext>(GetDbContextOptions());
            return appDbContextMock;
        }

        public static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> items = null) where T : class
        {
            if (items == null)
            {
                items = new T[0];
            }

            var dbSetMock = new Mock<DbSet<T>>();
            var q = dbSetMock.As<IQueryable<T>>();

            q.Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator);

            return dbSetMock;
        }

        private static DbContextOptions<AppDbContext> GetDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabseName");
            return optionsBuilder.Options;
        }
        public static Mock<DbSet<T>> Create<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mock = new Mock<DbSet<T>>();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mock;
        }   
    }

}
