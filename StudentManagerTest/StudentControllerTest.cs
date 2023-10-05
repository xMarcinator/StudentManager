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
        
        mock.Setup(m=>m.Select(
            1 //Any value
        )).Returns(testData);
        
        var controller = new StudentController(mock.Object);
        
        ViewResult result = (ViewResult)controller.Edit(1);

        var model = result.ViewData!;
        Assert.NotNull(model);

        Type type = typeof(Student);
        
        Assert.True(model.All((keyPair) =>
        {
            PropertyInfo? property =  type.GetProperty(keyPair.Key);
            
            Assert.NotNull(property);

            var val = property.GetValue(testData);

            return Equals(keyPair.Value, val);
        }));
        
        mock.Verify(m => m.Select(1), Times.Once);
    }
}