
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
    [Fact]
    public void AddContact_ShouldAddContactToListAndFile_ThenReturnServiceResult()
    {
        // Arrange
        Contact contact = new Contact()
        {
            FirstName = "Levi",
            LastName = "Stark",
            Email = "test@domain.com",
            Phone = "0701234567",
            Address = "Helsingborg"
        };

        var mockFileService = new Mock<IFileService>();
        mockFileService
            .Setup(x => x.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()));

        IContactServices contactServices = new ContactServices(mockFileService.Object);

        // Act
        var result = contactServices.AddContact(contact);

        // Assert
        Assert.Equal(ServiceStatus.CREATED, result.Status);
    }

    [Fact]

    public void GetContacts_ShouldReturnListOfIContacts()
    {
        // Arrange
        string json = "[{\"$type\":\"ContactBookLibrary.Models.Contact, ContactBookLibrary\",\"Id\":\"1c84218f-ce61-423f-928b-e81dc8b14762\",\"FirstName\":\"test\",\"LastName\":\"asd\",\"Email\":\"asd\",\"Phone\":\"asd\",\"Address\":\"asd\"}]\r\n";
        IContact contact = new Contact()
        {
            FirstName = "test"
        };
        var mockFileService = new Mock<IFileService>();
        mockFileService
            .Setup(x => x.GetContentFromFile(It.IsAny<string>()))
            .Returns(new ServiceResult { Result = json });

        IContactServices contactServices = new ContactServices(mockFileService.Object);

        // Act
        var result = contactServices.GetContacts();

        // Assert
        Assert.Equal(ServiceStatus.SUCCESS, result.Status);
        Assert.True(result.Result is List<IContact>);

        var contactList = (List<IContact>)result.Result;    

        Assert.Contains("test", contactList.First().FirstName);   
    }
}
