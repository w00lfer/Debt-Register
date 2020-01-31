using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Rest_API.Models;
using Rest_API.Repositories;
using Rest_API_Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_Tests.RepositoriesTests
{
    [TestFixture]
    class ContactRepositoryTests
    {
        private readonly ContactRepository _sut;
        private readonly Mock<AppDbContext> _appDbContextMock;

        public ContactRepositoryTests()
        {
            _appDbContextMock = DbContextMock.GetAppDbContextMock();
            _sut = new ContactRepository(_appDbContextMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllContacts_IfContactsExists()
        {
            // Arrange
            var contacts = new List<Contact> {
                new Contact { Id = 1, CreatorId = 1, FullName = "John doe", PhoneNumber = "123123123"},
                new Contact { Id = 2, CreatorId = 1, FullName = "doe John", PhoneNumber = "321321321"}
                };
            _appDbContextMock.Setup(x => x.Set<Contact>()).Returns(contacts.AsQueryable().BuildMockDbSet().Object);
            // Act

            var contactsFromDbContext = await _sut.GetAll().ToListAsync();

            // Assert
            Assert.AreEqual(contacts, contactsFromDbContext);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnContact_IfContactExists()
        {
            // Arrange
            var contact = new Contact { Id = 1, CreatorId = 1, FullName = "John doe", PhoneNumber = "123123123"};
            _appDbContextMock.Setup(x => x.Set<Contact>()).Returns(new List<Contact> { contact }.AsQueryable().BuildMockDbSet().Object);

            //Act
            var contactFromDbContext = await _sut.GetByIdAsync(contact.Id);

            // Assert

            Assert.AreEqual(contact, contactFromDbContext);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateContact_IfDataIsCorrect()
        {
            // Arrange
            var contact = new Contact { Id = 1, CreatorId = 1, FullName = "John doe", PhoneNumber = "123123123" };
            var dbSetMock = new Mock<DbSet<Contact>>();
            _appDbContextMock.Setup(x => x.Set<Contact>()).Returns(dbSetMock.Object);

            // Act
            await _sut.CreateAsync(contact);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Contact>().AddAsync(It.Is<Contact>(c => c == contact), default));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateContact_IfContactExists()
        {
            // Arrange
            var contact = new Contact { Id = 1, CreatorId = 1, FullName = "John doe", PhoneNumber = "123123123" };
            _appDbContextMock.Setup(x => x.Set<Contact>()).Returns(new List<Contact> { contact }.AsQueryable().BuildMockDbSet().Object);
            contact.FullName = "doe John";

            // Act
            await _sut.UpdateAsync(contact);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Contact>().Update(It.Is<Contact>(c => c == contact)));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteContact_IfContactExists()
        {
            // Arrange
            var contact = new Contact { Id = 1, CreatorId = 1, FullName = "John doe", PhoneNumber = "123123123" };
            _appDbContextMock.Setup(x => x.Set<Contact>()).Returns(new List<Contact> { contact }.AsQueryable().BuildMockDbSet().Object);


            // Act
            await _sut.DeleteAsync(contact.Id);

            // Assert
            _appDbContextMock.Verify(x => x.Set<Contact>().Remove(It.Is<Contact>(c => c == contact)));
            _appDbContextMock.Verify(x => x.SaveChangesAsync(default));
        }
    }
}
