using Academy.Domain;
using Academy.Domain.Tests.Unit.Builders;
using FluentAssertions;
using NSubstitute;

namespace Academy.Application.Tests.Unit {
    public class CourseServiceTests {

        [Fact]
        public void Should_CreateANewCourse() {
            var command = new CreateCourse() {
                Id = 1,
                Name = "amir",
                IsOnline = true,
                Tuition = 3f,
                Instructor = "instruct",
            };
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = new CourseService(courseRepository);

            courseService.Create(command);

            //ourseRepository.Received().Create(Arg.Any<Course>());
            courseRepository.ReceivedWithAnyArgs().Create(default);
        }

        [Fact]
        public void Should_CreateANewCourseAndReturnId() {
            var command = new CreateCourse() {
                Id = 1,
                Name = "amir",
                IsOnline = true,
                Tuition = 3.3f,
                Instructor = "instruction",
            };
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = new CourseService(courseRepository);

            var actual = courseService.Create(command);

            actual.Should().Be(command.Id);
        }

        [Fact]
        public void Should_ThrowException_WhenAddingCourseIsDuplicated() {
            var command = new CreateCourse() {
                Id = 2,
                Name = "amir",
                IsOnline = true,
                Tuition = 100.1f,
                Instructor = "instruction"
            };
            var courseRepository = Substitute.For<ICourseRepository>();
            var course = new CourseTestBuilder().Build();
            courseRepository.GetBy(2).Returns(course);
            var courseService = new CourseService(courseRepository);

            Action actual = () => courseService.Create(command);

            actual.Should().Throw<Exception>();
        }


    }
}