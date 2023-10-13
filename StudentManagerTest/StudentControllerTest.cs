using System.Collections;
using System.Dynamic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Models.Fakers;
using StudentManager.Utils;

namespace StudentManagerTest;

public class StudentControllerTest
{
    [Fact]
    public void TestEdit()
    {
        var mock = new Mock<IModelRepository<Student>>();

        var dummyData = StudentFaker.FakeTest(3);
        
        
        mock.Setup(x => x.Models)
            .Returns(dummyData.AsQueryable())
            .Verifiable(Times.Once);
        
        var controller = new StudentController(mock.Object);
        
        var model = (controller.Edit(0) as ViewResult)?.ViewData.Model as Student;
        Assert.NotNull(model);
        
        Assert.Equal(dummyData[0], model);
        
        mock.Verify();
    }
}