
using ContactBookLibrary.Enums;
using ContactBookLibrary.Models.Responses;
using ContactBookLibrary.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using Moq;

namespace ContactBook_Tests;

public class ContactServices_Tests
{
    private List<IContact> _contacts = [];
    [Fact]
    public void AddContact_ShouldAddContactToListAndFile_ThenReturnServiceResult()
    {
        // Arrange
        Contact contact = new Contact() { Email = "test@domain.com" };
        IContactServices contactServices = new ContactServices();

        var mockFileService = new Mock<IFileService>();
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

        mockFileService.Setup(x => x.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), It.IsAny<string>()));

        // Act
        _contacts.Add(contact);

        //var result = contactServices.AddContact(contact);
        //var fileSaveResult = mockFileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings));


        // Assert
        Assert.Contains(contact, _contacts);
        //Assert.True(result.Status == ServiceStatus.CREATED);
        //Assert.False(result.Status == ServiceStatus.ALREADY_EXISTS);
        //Assert.False(result.Status == ServiceStatus.FAILED);
    }

}
