using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using Rest_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Rest_API_Tests.ServicesTests
{
    [TestFixture]
    class ContactServiceTests
    {
        private readonly ContactService _sut;
        private readonly Mock<IContactRepository> _contactRepositoryMock = new Mock<IContactRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IUserStore<User>> _userStoreMock = new Mock<IUserStore<User>>();
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IHttpContextAccessor> _httpContectAccessorMock = new Mock<IHttpContextAccessor>();

        public ContactServiceTests()
        {
            _userManagerMock = new Mock<UserManager<User>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _sut = new ContactService(_mapperMock.Object, _contactRepositoryMock.Object, _userManagerMock.Object, _httpContectAccessorMock.Object);
        }

        [Test]
        public async Task GetContactByIdAsync_ShouldReturnContact_WhenContactExists()
        {
            // Arrange
            var contactId = 1;
            var creatorId = 1;
            var contactFullName = "John Doe";
            var contactPhoneNumber = "123123123";
            var contact = new Contact
            {
                Id = contactId,
                CreatorId = creatorId,
                FullName = contactFullName,
                PhoneNumber = contactPhoneNumber
            };
            _contactRepositoryMock.Setup(x => x.GetByIdAsync(contactId))
                .ReturnsAsync(contact);

            // Act
            var contactFromRepository = await _sut.GetContactByIdAsync(contactId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(contactId, contact.Id);
                Assert.AreEqual(creatorId, contact.CreatorId);
                Assert.AreEqual(contactFullName, contact.FullName);
                Assert.AreEqual(contactPhoneNumber, contact.PhoneNumber);
            });
        }

        [Test]
        public async Task GetAllContactsAsync_ShouldReturnAllContactsForCurrentUser_WhenContactsExists()
        {
            // Arrange
            SetUpUserManagerToGetCurrentUser();
            var currentUserId = 1;
            var contactId = 1;
            var firstContactName = "John Doe";
            var secondContactName = "Foo Bar";
            var thirdContactName = "Bar Foo";
            var firstContactPhoneNumber = "123123123";
            var secondContactPhoneNumber = "321321321";
            var thirdContactPhoneNumber = "222111333";
            var contacts = new List<Contact>()
                {
                    new Contact() {Id = contactId, CreatorId = currentUserId, FullName = firstContactName, PhoneNumber = firstContactPhoneNumber},
                    new Contact() {Id = contactId + 1, CreatorId = currentUserId + 1, FullName = secondContactName, PhoneNumber = secondContactPhoneNumber},
                    new Contact() {Id = contactId + 2, CreatorId = currentUserId, FullName = thirdContactName, PhoneNumber = thirdContactPhoneNumber}
                }.AsQueryable();
            _mapperMock.Setup(x => x.Map<List<ContactForTable>>(It.IsAny<IQueryable<Contact>>())).Returns(new List<ContactForTable>
            {
                new ContactForTable() {Id = contactId, FullName = firstContactName, PhoneNumber = firstContactPhoneNumber},
                new ContactForTable() {Id = contactId + 2, FullName = thirdContactName, PhoneNumber = thirdContactPhoneNumber}
            });
            _contactRepositoryMock.Setup(x => x.GetAll())
                .Returns(contacts);

            // Act
            var contactsFromRepositoryForCurrentUser = await _sut.GetAllContactsAsync();

            // Assert
            Assert.AreEqual(_mapperMock.Object.Map<List<ContactForTable>>(contacts), contactsFromRepositoryForCurrentUser);
        }

        [Test]
        public async Task GetAllContactsNamesAsync_ShouldReturnAllContactsNamesForCurrentUser_WhenContactsExists()
        {
            // Arrange
            SetUpUserManagerToGetCurrentUser();
            var currentUserId = 1;
            var contactId = 1;
            var firstContactName = "John Doe";
            var secondContactName = "Foo Bar";
            var thirdContactName = "Bar Foo";
            var firstContactPhoneNumber = "123123123";
            var secondContactPhoneNumber = "321321321";
            var thirdContactPhoneNumber = "222111333";
            var contacts = new List<Contact>()
                {
                    new Contact() {Id = contactId, CreatorId = currentUserId, FullName = firstContactName, PhoneNumber = firstContactPhoneNumber},
                    new Contact() {Id = contactId + 1, CreatorId = currentUserId + 1, FullName = secondContactName, PhoneNumber = secondContactPhoneNumber},
                    new Contact() {Id = contactId + 2, CreatorId = currentUserId, FullName = thirdContactName, PhoneNumber = thirdContactPhoneNumber}
                }.AsQueryable();
            _mapperMock.Setup(x => x.Map<List<LenderOrBorrowerForTable>>(It.IsAny<IQueryable<Contact>>())).Returns(new List<LenderOrBorrowerForTable>
            {
                new LenderOrBorrowerForTable() {Id = contactId, FullName = firstContactName},
                new LenderOrBorrowerForTable() {Id = contactId + 2, FullName = thirdContactName}
            });
            _contactRepositoryMock.Setup(x => x.GetAll())
                .Returns(contacts);

            // Act
            var contactsFromRepositoryForCurrentUser = await _sut.GetAllContactsAsync();

            // Assert
            Assert.AreEqual(_mapperMock.Object.Map<List<LenderOrBorrowerForTable>>(contacts), contactsFromRepositoryForCurrentUser);
        }

        private void SetUpUserManagerToGetCurrentUser()
        {
            _httpContectAccessorMock.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal(new GenericIdentity("some name", "test")));
            _userManagerMock.Setup(x => x.GetUserAsync(_httpContectAccessorMock.Object.HttpContext.User)).ReturnsAsync(new User() { Id = 1 });
        }
    }
}
