using Academy.Domain.Tests.Unit.Builders;
using FluentAssertions;

namespace Academy.Infrastrucure.Tests.Unit {
    public class CourseRepositoryTests {
        private readonly CourseRepository _courseRepository;
        private readonly CourseTestBuilder _courseTestBuilder;

        public CourseRepositoryTests () {
            _courseTestBuilder = new CourseTestBuilder();
            _courseRepository = new CourseRepository();
        }

        [Fact]
        public void Should_AddNewCourseToCoursesList () {
            var course = _courseTestBuilder.Build();

            _courseRepository.Create(course);

            //repository.Courses.Should().ContainEquivalentOf(course);
            _courseRepository.Courses.Should().Contain(course);
        }

        [Fact]
        public void Should_ReturnListOfCourse () {

            var actual = _courseRepository.GetAll();

            actual.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void Should_ReturnCourseById () {
            const int id = 3;
            var expectedCourse = _courseTestBuilder.withId(id).Build();
            _courseRepository.Create(expectedCourse);

            var actual = _courseRepository.GetBy(id);

            actual.Should().Be(expectedCourse);
        }

        [Fact]
        public void Should_ReturnNullWhenIdNotExists () {
            const int id = 3;

            var actual = _courseRepository.GetBy(id);

            actual.Should().BeNull();
        }
    }
}