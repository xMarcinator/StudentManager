using System.Collections;
using System.Dynamic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Utils;

namespace StudentManagerTest;

public class StudentControllerTest
{
    [Fact]
    public void TestEdit()
    {
        Student testData = new Student {Id = 1, Name = "Test", Education = "Datamtiker", Semester = 2};
        
        var mock = new Mock<IRepository<Student,int>>();
        
        mock.Setup(m=>m.Select(1))
            .Returns(testData)
            .Verifiable(Times.Once);
        
        var controller = new StudentController(mock.Object);
        
        var model = (controller.Edit(1) as ViewResult)?.ViewData.Model as Student;
        Assert.NotNull(model);
        
        Assert.Equal(testData, model);
        
        mock.Verify();
    }
}