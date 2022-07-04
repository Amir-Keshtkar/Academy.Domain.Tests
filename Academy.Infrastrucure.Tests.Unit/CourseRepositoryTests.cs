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

        [Fact]
        public void Should_DeleteCourseFromStore () {
            const int id = 3;
            var course = _courseTestBuilder.withId(id).Build();
            _courseRepository.Create(course);

            _courseRepository.Delete(id);

            _courseRepository.Courses.Should().NotContain(course);
        }

        [Fact]
        public void Should_ReturnCourseByName () {
            const string amiri = "amiri";
            var expectedCourse = _courseTestBuilder.withName(amiri).Build();
            _courseRepository.Create(expectedCourse);

            var actual = _courseRepository.GetBy(amiri);

            //actual.Name.Should().Be(amiri);
            expectedCourse.Should().Be(actual);
        }

        [Fact]
        public void Should_ReturnNull_WhenCourseWithNameNotExists () {
            const string name="instruction";

            var actual = _courseRepository.GetBy(name);

            actual.Should().BeNull();
        }
    }
}