using Academy.Domain;
using Academy.Domain.Exceptions;
using Academy.Domain.Tests.Unit.Builders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Tynamix.ObjectFiller;

namespace Academy.Application.Tests.Unit
{
    public class CourseServiceTests
    {
        private readonly CourseService _courseService;
        private readonly CourseTestBuilder _courseTestBuilder;
        private readonly ICourseRepository _courseRepository;

        public CourseServiceTests()
        {
            _courseRepository = Substitute.For<ICourseRepository>();
            _courseService = new CourseService(_courseRepository);
            _courseTestBuilder = new CourseTestBuilder();
        }

        [Fact]
        public void Should_CreateANewCourse()
        {
            var command = SomeCreateCourse();

            _courseService.Create(command);

            //ourseRepository.Received().Create(Arg.Any<Course>());
            _courseRepository.ReceivedWithAnyArgs().Create(default);
        }

        [Fact]
        public void Should_CreateANewCourseAndReturnId()
        {
            var command = SomeCreateCourse();
            _courseRepository.Create(default).ReturnsForAnyArgs(10);

            var actual = _courseService.Create(command);

            actual.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Should_ThrowException_WhenAddingCourseIsDuplicated()
        {
            CreateCourse command = SomeCreateCourse();
            var course = _courseTestBuilder.Build();
            _courseRepository.GetBy(Arg.Any<int>()).Returns(course);

            Action actual = () => _courseService.Create(command);

            actual.Should().ThrowExactly<DuplicatedCourseNameException>();
        }

        [Fact]
        public void Should_UpdateCourse()       //Should_CallDeleteAndThenCreateRepository
        {
            var command = SomeEditCourse();
            var course = _courseTestBuilder.Build();
            _courseRepository.GetBy(command.Id).Returns(course);

            _courseService.Edit(command);

            //_courseRepository.Received().Delete(command.Id);
            //_courseRepository.Received().Create(Arg.Any<Course>());
            Received.InOrder(() =>
            {
                _courseRepository.Delete(command.Id);
                _courseRepository.Create(Arg.Any<Course>());
            });
        }

        [Fact]
        public void Should_ReturnIdOfUpdatedRecord()
        {
            var command = SomeEditCourse();
            var id=_courseRepository.Create(default).ReturnsForAnyArgs(10);
            var course=_courseTestBuilder.Build();
            _courseRepository.GetBy(Arg.Any<int>()).Returns(course);

            var actual = _courseService.Edit(command);

            actual.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Should_ThrowException_WhenUpdatingCourseNotExists()
        {
            EditCourse command = SomeEditCourse();
            _courseRepository.GetBy(command.Id).ReturnsNull();

            Action actual = () => _courseService.Edit(command);

            actual.Should().Throw<CourseNotExistException>();
        }
        [Fact]
        public void Should_DeleteCourse()
        {
            const int id=1;

            _courseService.Delete(id);

            _courseRepository.Received().Delete(id);
        }

        [Fact]
        public void Should_GetListOfCourses()
        {
            _courseRepository.GetAll().Returns(new List<Course>());

            var courses=_courseService.GetAll();

            courses.Should().BeOfType<List<Course>>();
            _courseRepository.Received().GetAll();
        }
        private static EditCourse SomeEditCourse()
        {
            return new EditCourse
            {
                Id = 1,
                Name = "amir",
                IsOnline = true,
                Instructor = "instruct",
                Tuition = 33f
            };
        }
        private static CreateCourse SomeCreateCourse()
        {
            // Faker.Net
            //return new CreateCourse()
            //{
            //    Id = 12,
            //    Name = Faker.Name.FullName(),
            //    IsOnline = true,
            //    Tuition = 100.1f,
            //    Instructor = "instruction"
            //};

            //Tynamics Object Filler
            var filler = new Filler<CreateCourse>();
            filler.Setup().OnProperty(x => x.Tuition).Use(33);
            return filler.Create();
        }
    }
}