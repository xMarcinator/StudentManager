using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Models.Fakers;

namespace StudentManagerTest;

public class StudentControllerTest
{
    [Fact]
    public void TestEdit()
    {
        //prepare
        var studentMock = new Mock<IModelRepository<Student>>();
        var educationMock = new Mock<IModelRepository<Education>>();

        var dummyStudents = new StudentFaker(true).Generate(3);

        studentMock.Setup(x => x.Models)
            .Returns(dummyStudents.AsQueryable())
            .Verifiable(Times.Once);

        var controller = new StudentController(studentMock.Object);

        //execute
        var model = (controller.Edit(dummyStudents[0].Id) as ViewResult)?.ViewData.Model as Student;

        //test
        Assert.NotNull(model);

        Assert.Equal(dummyStudents[0], model);

        studentMock.Verify();
        educationMock.Verify();
    }
}