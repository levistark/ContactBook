using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using ContactBookLibrary.Services;
using System.Diagnostics;
using System;
using ContactBookLibrary.Enums;

namespace ContactBook_Tests;

public class FileServices_Tests
{
    [Fact]
    public void SaveContentToFile_ShouldSaveContenetToFile_ThenReturnServiceResult()
    {
        // Arrange 
        IFileService fileService = new FileService();
        string filePath = @"C:\test.txt";
        string content = "Test content";

        // Act
        var result = fileService.SaveContentToFile(filePath, content);

        // Assert
        Assert.True(result.Status == ServiceStatus.UPDATED);
    }
}
