using System.Collections;
using System.Dynamic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManagerTest;

public class StudentControllerTest
{
    [Fact]
    public void TestEdit()
    {
        Student testData = new Student {Id = 1, Name = "Test", Education = "Datamtiker", Semester = 2};
        
        var mock = new Mock<IModelRepository<Student>>();
        
        var dummyData = (new List<Student>() { 
            new Student {Id = 1, Name = "***REMOVED*** doe", Education = "Datamtiker", Semester = 2},
            new Student {Id = 2, Name = "***REMOVED*** doe", Education = "Datamtiker", Semester = 2} 
        });
        
        mock.Setup(x => x.Models)
            .Returns(dummyData.AsQueryable())
            .Verifiable(Times.Once);
        
        var controller = new StudentController(mock.Object);
        
        var model = (controller.Edit(1) as ViewResult)?.ViewData.Model as Student;
        Assert.NotNull(model);
        
        Assert.Equal(dummyData[0], model);
        
        mock.Verify();
    }
}