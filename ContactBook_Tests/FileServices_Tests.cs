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
        string filePath = @"C:\VSProjects\test.txt";
        string content = "Test content";

        // Act
        var result = fileService.SaveContentToFile(content, filePath);

        // Assert
        Assert.True(result.Status == ServiceStatus.UPDATED);
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnFalse_IfFilePathNotExists()
    {
        // Arrange 
        IFileService fileService = new FileService();
        string filePath = @$"C:\{Guid.NewGuid()}\test.txt";
        string content = "Test content";

        // Act
        var result = fileService.SaveContentToFile(content, filePath);

        // Assert
        Assert.False(result.Status == ServiceStatus.UPDATED);
    }
}
