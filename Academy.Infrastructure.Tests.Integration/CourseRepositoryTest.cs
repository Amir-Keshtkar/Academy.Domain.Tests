using Academy.Domain;
using Academy.Domain.Tests.Unit.Builders;
using FluentAssertions;

namespace Academy.Infrastructure.Tests.Integration
{
    public class CourseRepositoryTest : IClassFixture<RealDatabaseFixture>
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseTestBuilder _courseTestBuilder;

        public CourseRepositoryTest(RealDatabaseFixture realDatabaseFixture)
        {
            _courseRepository = new CourseRepository(realDatabaseFixture.context);
            _courseTestBuilder = new CourseTestBuilder();
        }

        [Fact]
        public void Should_ReturnAllCourses()
        {
            var courses = _courseRepository.GetAll();

            courses.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void Should_CreateCourses()
        {
            var course = _courseTestBuilder.Build();

            _courseRepository.Create(course);

            var courses = _courseRepository.GetAll();
            courses.Should().Contain(course);
        }

        [Fact]
        public void Should_ReturnIdOfTheCreatedCourse()
        {
            var course = _courseTestBuilder.Build();

            int id = _courseRepository.Create(course);

            id.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Should_GetCourseByName()
        {
            const string courseName = "onion architecture";
            var expeted = _courseTestBuilder.withName(courseName).Build();
            _courseRepository.Create(expeted);

            var actual = _courseRepository.GetBy(courseName);

            actual.Name.Should().Be(expeted.Name);
            actual.Tuition.Should().Be(expeted.Tuition);
            actual.Instructor.Should().Be(expeted.Instructor);

        }

        [Fact]
        public void Should_DeleteExistingCourse()
        {
            var course = _courseTestBuilder.Build();
            var id = _courseRepository.Create(course);

            _courseRepository.Delete(id);

            var actual = _courseRepository.GetBy(course.Id);
            actual.Should().BeNull();
        }

        [Fact]
        public void Should_GetCourseById()
        {
            var expected = _courseTestBuilder.Build();
            int id = _courseRepository.Create(expected);

            var actual = _courseRepository.GetBy(id);

            actual.Should().Be(expected);
        }
    }
}