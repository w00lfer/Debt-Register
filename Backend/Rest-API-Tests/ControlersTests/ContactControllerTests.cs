using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rest_API.Controllers;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_Tests.ControlersTests
{
    [TestFixture]
    class ContactControllerTests
    {
        [Test]
        public async Task GetAllContacts_ValidContacts_ListOfContactForTable()
        {
            //ARRANGE
            var contacts = new List<ContactForTable> 
            {
                    new ContactForTable{ Id = 1, FullName = "Pierwszy", PhoneNumber = "111111111" },
                    new ContactForTable{ Id = 2, FullName  = "Drugi", PhoneNumber = "321123321" }
            };
            var contactService = Mock.Of<IContactService>
                (x => x.GetAllContactsAsync() == Task.FromResult(contacts));            
            var contactController = new ContactController(contactService);

            //ACT
            var controllersContacts = await contactController.GetAllContactsAsync();

            //ASSERT
            Assert.AreEqual(contacts, controllersContacts);
        }
        
        [Test]
        public async Task GetAllContactsFullNames_ValidContacts_ListOfLenderorBorrowerForTable()
        {
            //ARRANGE
            var contactsNames = new List<LenderOrBorrowerForTable>
            {
                new LenderOrBorrowerForTable { Id = 1, FullName = "Test" },
                new LenderOrBorrowerForTable { Id = 3, FullName = "Konrad" },
                new LenderOrBorrowerForTable { Id = 7, FullName = "Maciej"}
            };
            var contactService = Mock.Of<IContactService>
                (x => x.GetAllContactsNamesAsync() == Task.FromResult(contactsNames));
            var contactController = new ContactController(contactService);

            //ACT
            var controllersContactsNames = await contactController.GetAllContactsNamesAsync();

            //ASSERT
            Assert.AreEqual(contactsNames, controllersContactsNames);

        }
        
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task GetContactById_IdOfExistingContact_Contact(int contactId)
        {
            //ARRANGE
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FullName = "Test", CreatorId = 1, PhoneNumber = "321321321" },
                new Contact { Id = 3, FullName = "Konrad", CreatorId = 4, PhoneNumber = "111111111" },
                new Contact { Id = 7, FullName = "Maciej", CreatorId = 4, PhoneNumber = "999999999" }
            };
            var contactService = Mock.Of<IContactService>
                (x => x.GetContactByIdAsync(contactId) == Task.FromResult(contacts.First(x => x.Id == contactId)));
            var contactController = new ContactController(contactService);

            //ACT
            var controllerContact = await contactController.GetContactByIdAsync(contactId);

            //ASSERT
            Assert.AreEqual(contacts.First(x => x.Id ==  contactId), controllerContact);
        }

        [Test]
        public async Task EditContact_ValidEditContact_OkObjectResult()
        {
            //ARRANGE
            var contactService = Mock.Of<IContactService>();
            var contactController = new ContactController(contactService);

            //ACT
            var respondData = await contactController.EditContactAsync(It.IsAny<EditContact>());

            //ARRANGE
            Assert.IsAssignableFrom<OkObjectResult>(respondData);
        }

        [Test]
        public async Task DeleteContact_IdOfExistingContact_OkObjectResult() // NI WIM JAK TUTAJ
        {
            //ARRANGE
            var contactService = Mock.Of<IContactService>();
            var contactController = new ContactController(contactService);

            //ACT
            var respondData = await contactController.DeleteContactAsync(It.IsAny<int>());

            //ARRANGE
            Assert.IsAssignableFrom<OkObjectResult>(respondData);
        }
    }
}
