using Academy.Application;
using Academy.Domain;
using Academy.Presentation.Controllers;
using FluentAssertions;
using NSubstitute;

namespace Academy.Presentation.Test.Unit
{
    public class CourseControllerTest
    {
        private readonly CourseController _controller;
        private readonly ICourseService _service;

        public CourseControllerTest()
        {
            _service = Substitute.For<ICourseService>();
            _controller = new CourseController(_service);
        }

        [Fact]
        public void Should_ReturnAllCourses()
        {
            _controller.GetAll();

            _service.Received().GetAll();
        }

        [Fact]
        public void Should_ReturnListOfAllCourses()
        {
            _service.GetAll().Returns(new List<Course>());

            var courses = _controller.GetAll();

            courses.Should().BeOfType<List<Course>>();
        }

        [Fact]
        public void Should_ReturnACourse()
        {
            var command = new CreateCourse()
            {
                Name = "asp",
                IsOnline = true,
                Tuition = 30,
                Instructor = "instructor"
            };

            var id=_controller.CreateCourse(command);
            var course=_controller.GetBy(id);

            _service.Received().GetBy(id);
        }

        [Fact]
        public void Should_CreateANewCourse()
        {
            var command = new CreateCourse()
            {
                Name = "asp",
                IsOnline = true,
                Tuition = 30,
                Instructor = "instructor"
            };
            
            _controller.CreateCourse(command);

            _service.Received().Create(command);
        }

        [Fact]
        public void Should_EditExistingCourse()
        {
            var command = new EditCourse()
            {
                Id = 5,
                Name = "asp",
                IsOnline = true,
                Tuition = 30,
                Instructor = "instructor"
            };

            _controller.Edit(command);

            _service.Received().Edit(command);
        }

        [Fact]
        public void Should_DeleteExistingCourse()
        {
            const int id=5;
            _controller.Delete(id);

            _service.Received().Delete(id);
        }
    }
}