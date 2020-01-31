using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Rest_API.Models;
using Rest_API.Repositories;
using Rest_API_Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_Tests.RepositoriesTests
{
    [TestFixture]
    class DebtRepositoryTests
    {
        private readonly DebtRepository _sut;
        private readonly Mock<AppDbContext> _appDbContextMock;

        public DebtRepositoryTests()
        {
            _appDbContextMock = DbContextMock.GetAppDbContextMock();
            _sut = new DebtRepository(_appDbContextMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllDebts_IfDebtsExists()
        {
            // Arrange
            var debts = new List<Debt> {
                new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true},
                new Debt { Id = 2, Name = "second debt", Value = 150.00M, Description = "Random senond debt description",  DebtStartDate = DateTime.Now, LenderId = 2, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},

                };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(debts.AsQueryable().BuildMockDbSet().Object);
            // Act

            var debtsFromDbContext = await _sut.GetAll().ToListAsync();

            // Assert
            Assert.AreEqual(debts, debtsFromDbContext);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnDebt_IfDebtExists()
        {
            // Arrange
            var debt = new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description", DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(new List<Debt> { debt }.AsQueryable().BuildMockDbSet().Object);

            //Act
            var debtFromDbContext = await _sut.GetByIdAsync(debt.Id);

            // Assert

            Assert.AreEqual(debt, debtFromDbContext);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateDebt_IfDataIsCorrect()
        {
            // Arrange
            var debt = new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description", DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true };
            var dbSetMock = new Mock<DbSet<Debt>>();
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(dbSetMock.Object);

            // Act
            await _sut.CreateAsync(debt);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Debt>().AddAsync(It.Is<Debt>(c => c == debt), default));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateContact_IfContactExists()
        {
            // Arrange
            var debt = new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description", DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(new List<Debt> { debt }.AsQueryable().BuildMockDbSet().Object);
            debt.Name = "second debt";

            // Act
            await _sut.UpdateAsync(debt);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Debt>().Update(It.Is<Debt>(c => c == debt)));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteContact_IfContactExists()
        {
            // Arrange
            var debt = new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description", DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(new List<Debt> { debt }.AsQueryable().BuildMockDbSet().Object);


            // Act
            await _sut.DeleteAsync(debt.Id);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Debt>().Remove(It.Is<Debt>(c => c == debt)));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }

        [Test]
        public async Task GetAllBorrowedDebtsAsync_ShouldReturnedAllBorrowedDebtsForUser_IfUserHasBorrowedDebts()
        {
            // Arrange
            int userId = 1;
            var debts = new List<Debt> {
                new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true},
                new Debt { Id = 2, Name = "second debt", Value = 150.00M, Description = "Random senond debt description",  DebtStartDate = DateTime.Now, LenderId = 2, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 3, Name = "third debt", Value = 200.00M, Description = "Random third debt description",  DebtStartDate = DateTime.Now, LenderId = 2, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = true},
            };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(debts.AsQueryable().BuildMockDbSet().Object);
            var userAllBorrowedDebts = debts.Where(b => b.BorrowerId == userId && b.IsBorrowerLocal == false);

            // Act
            var userAllBorrowedDebtsFromRepo = await _sut.GetAllBorrowedDebtsAsync(userId);

            // Assert
            Assert.AreEqual(userAllBorrowedDebts, userAllBorrowedDebtsFromRepo);
        }

        [Test]
        public async Task GetLastBorrowedDebtsAsync_ShouldReturnedLastBorrowedDebtsForUser_IfUserHasBorrowedDebts()
        {
            // Arrange
            int userId = 1;
            var debts = new List<Debt> {
                new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true},
                new Debt { Id = 2, Name = "second debt", Value = 150.00M, Description = "Random senond debt description",  DebtStartDate = DateTime.Now.AddDays(1), LenderId = 2, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 3, Name = "third debt", Value = 200.00M, Description = "Random third debt description",  DebtStartDate = DateTime.Now.AddDays(2), LenderId = 2, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = true},
                new Debt { Id = 4, Name = "fourth debt", Value = 300.00M, Description = "Random fourth debt description",  DebtStartDate = DateTime.Now.AddDays(3), LenderId = 5, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = true},
                new Debt { Id = 5, Name = "fifth debt", Value = 350.00M, Description = "Random fifth debt description",  DebtStartDate = DateTime.Now.AddDays(4), LenderId = 4, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 6, Name = "sixth debt", Value = 500.00M, Description = "Random sixth debt description",  DebtStartDate = DateTime.Now.AddDays(5), LenderId = 3, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 7, Name = "seventh debt", Value = 550.00M, Description = "Random seventh debt description",  DebtStartDate = DateTime.Now.AddDays(6), LenderId = 3, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false}

            };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(debts.AsQueryable().BuildMockDbSet().Object);
            var userLastBorrowedDebts = debts.Where(b => b.BorrowerId == userId && b.IsBorrowerLocal == false).OrderByDescending(d => d.DebtStartDate).Take(5);

            // Act
            var userLastBorrowedDebtsFromRepo = await _sut.GetLastBorrowedDebtsAsync(userId);

            // Assert
            Assert.AreEqual(userLastBorrowedDebts, userLastBorrowedDebtsFromRepo);
        }

        [Test]
        public async Task GetAllLentDebtsAsync_ShouldReturnedAllLentDebtsForUser_IfUserHasLentDebts()
        {
            // Arrange
            int userId = 1;
            var debts = new List<Debt> {
                new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 2, IsBorrowerLocal = true, IsPayed = true},
                new Debt { Id = 2, Name = "second debt", Value = 150.00M, Description = "Random senond debt description",  DebtStartDate = DateTime.Now, LenderId = 2, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 3, Name = "third debt", Value = 200.00M, Description = "Random third debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true},
            };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(debts.AsQueryable().BuildMockDbSet().Object);
            var userAllLentDebts = debts.Where(l => l.LenderId == userId && l.IsLenderLocal == false);

            // Act
            var userAllLentDebtsFromRepo = await _sut.GetAllLentDebtsAsync(userId);

            // Assert
            Assert.AreEqual(userAllLentDebts, userAllLentDebtsFromRepo);
        }

        [Test]
        public async Task GetLastLentDebtsAsync_ShouldReturnedLastLentDebtsForUser_IfUserHasLentDebts()
        {
            // Arrange
            int userId = 1;
            var debts = new List<Debt> {
                new Debt { Id = 1, Name = "first debt", Value = 100.00M, Description = "Random first debt description",  DebtStartDate = DateTime.Now, LenderId = 1, IsLenderLocal = true, BorrowerId = 1, IsBorrowerLocal = false, IsPayed = true},
                new Debt { Id = 2, Name = "second debt", Value = 150.00M, Description = "Random senond debt description",  DebtStartDate = DateTime.Now.AddDays(1), LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = false},
                new Debt { Id = 3, Name = "third debt", Value = 200.00M, Description = "Random third debt description",  DebtStartDate = DateTime.Now.AddDays(2), LenderId = 1, IsLenderLocal = false, BorrowerId = 1, IsBorrowerLocal = true, IsPayed = true},
                new Debt { Id = 4, Name = "fourth debt", Value = 300.00M, Description = "Random fourth debt description",  DebtStartDate = DateTime.Now.AddDays(3), LenderId = 1, IsLenderLocal = false, BorrowerId = 2, IsBorrowerLocal = false, IsPayed = true},
                new Debt { Id = 5, Name = "fifth debt", Value = 350.00M, Description = "Random fifth debt description",  DebtStartDate = DateTime.Now.AddDays(4), LenderId = 1, IsLenderLocal = false, BorrowerId = 3, IsBorrowerLocal = true, IsPayed = false},
                new Debt { Id = 6, Name = "sixth debt", Value = 500.00M, Description = "Random sixth debt description",  DebtStartDate = DateTime.Now.AddDays(5), LenderId = 1, IsLenderLocal = false, BorrowerId = 2, IsBorrowerLocal = false, IsPayed = false},
                new Debt { Id = 7, Name = "seventh debt", Value = 550.00M, Description = "Random seventh debt description",  DebtStartDate = DateTime.Now.AddDays(6), LenderId = 1, IsLenderLocal = false, BorrowerId = 4, IsBorrowerLocal = false, IsPayed = false}

            };
            _appDbContextMock.Setup(x => x.Set<Debt>()).Returns(debts.AsQueryable().BuildMockDbSet().Object);
            var userLastLentDebts = debts.Where(l => l.LenderId == userId && l.IsLenderLocal == false).OrderByDescending(d => d.DebtStartDate).Take(5);

            // Act
            var userLastLentDebtsFromRepo = await _sut.GetLastLentDebtsAsync(userId);

            // Assert
            Assert.AreEqual(userLastLentDebts, userLastLentDebtsFromRepo);
        }
    }
}
