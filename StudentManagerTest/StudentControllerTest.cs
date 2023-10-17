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
using StudentManager.Models.ViewModel;
using StudentManager.Utils;

namespace StudentManagerTest;

public class StudentControllerTest
{
    [Fact]
    public void TestEdit()
    {
        //prepare
        var studentMock = new Mock<IModelRepository<Student>>();
        var educationMock = new Mock<IModelRepository<Education>>();
        
        var dummyEducations = new EducationFaker(true).Generate(3);
        var dummyClasses = new ClassFaker(true).Generate(dummyEducations.Count());
        var dummyStudents = new StudentFaker(true).Generate(dummyEducations.Count*3);
        var dummyCourse = new CourseFaker(true).Generate(dummyEducations.Count*2);
        
        //prepare studentMock
        for (int i = dummyStudents.Count - 1; i >= 0; i--)
        {
            dummyStudents[i].Class = dummyClasses[i / 3];
        }
        
        for (int i = dummyClasses.Count - 1; i >= 0; i--)
        {
            dummyClasses[i].ClassCourses = dummyCourse.GetRange(i*2,2);
            dummyClasses[i].Education = dummyEducations[i / 2];
        }
        
        
        //prepare educationMock
        for (int i = dummyEducations.Count - 1; i >= 0; i-=2)
        {
            dummyEducations[i].Courses = dummyCourse.GetRange(i, 2);
        }
        
        
        /*.Where(model => model.Id == id)
            .Include(o => o.Class)
            .ThenInclude(o => o.Education)
            .Include(o => o.Class)
            .ThenInclude(o => o.ClassCourses)
            .FirstOrDefault();
        */
        
        studentMock.Setup(x => x.Models)
            .Returns(dummyStudents.AsQueryable())
            .Verifiable(Times.Once);
        
        educationMock.Setup(x => x.Models)
            .Returns(dummyEducations.AsQueryable())
            .Verifiable(Times.Once);
        
        var controller = new StudentController(studentMock.Object,educationMock.Object);
        
        //execute
        var model = (controller.Edit(dummyStudents[0].Id) as ViewResult)?.ViewData.Model as StudentEditVM;
        
        //test
        Assert.NotNull(model);
        
        Assert.Equal(dummyStudents[0], model.student);
        
        studentMock.Verify();
        educationMock.Verify();
    }
}