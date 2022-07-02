using Academy.Domain.Exceptions;
using Academy.Domain.Tests.Unit.Builders;
using Academy.Domain.Tests.Unit.ClassFixtures;
using Academy.Domain.Tests.Unit.CollectionFixtures;
using Academy.Domain.Tests.Factories;

namespace Academy.Domain.Tests.Tests {

    [Collection("Database Collection")]
    public class CourseTests: IClassFixture<IdentifierFixture> {
        private readonly CourseTestBuilder _courseTestBuilder;

        public CourseTests (DatabaseFixture databaseFixture) {
            _courseTestBuilder = new CourseTestBuilder();
        }

        [Fact]
        public void Constructor_ShouldConstructCourseProperly () {
            var guild = IdentifierFixture.Id;
            const int id = 1;
            const string name = "TDD & BDD";
            const bool isOnline = true;
            const double tuition = 600;
            const string instructor = "instructor";

            var course = _courseTestBuilder.Build();

            course.Id.Should().Be(id);
            course.Name.Should().Be(name);
            course.IsOnline.Should().Be(isOnline);
            course.Tuition.Should().Be(tuition);
            course.Instructor.Should().Be(instructor);
            course.Sections.Should().BeEmpty();

            //Assert.Equal(id, course.Id);
            //Assert.Equal(name, course.Name);
            //Assert.Equal(isOnline, course.IsOnline);
            //Assert.Equal(tuition, course.Tuition);
        }

        [Fact]
        public void Constructor_ShouldThrowException_When_NameIsNotProvided () {
            var guild = IdentifierFixture.Id;
            var courseTestBuilder = _courseTestBuilder.withName("");
            Action course = () => courseTestBuilder.Build();
            course.Should().ThrowExactly<CourseNameIsInvalidException>();
            //Assert.Throws<Exception>(course);
        }

        [Fact]
        public void Constructor_ShouldThrowException_When_TuitionIsNotProvided () {
            Action course = () => _courseTestBuilder.withTuition(0).Build();
            course.Should().ThrowExactly<CourseTuitionIsInvalidException>();
            //Assert.Throws<Exception>(course);
        }

        [Fact]
        public void AddSection_ShouldAddNewSections_whenIdAndNamePassed () {
            var course = _courseTestBuilder.Build();
            var sectionToAdd = SectionFactory.Create();

            course.AddSection(sectionToAdd);

            course.Sections.Should().ContainEquivalentOf(sectionToAdd);
        }

        [Fact]
        public void Should_BeEqualWhenIdIsSame () {
            int sameId = 1;
            var courseBuilder = new CourseTestBuilder();
            var course1 = courseBuilder.withId(sameId).Build();
            var course2 = courseBuilder.withId(sameId).Build();

            var actual = course1.Equals(course2);

            actual.Should().Be(true);
            //course1.Should().Be(course2);
        }

        [Fact]
        public void Should_NotBeEqualWhenIdIsNotSame () {
            var courseBuilder = new CourseTestBuilder();
            var course1 = courseBuilder.withId(1).Build();
            var course2 = courseBuilder.withId(2).Build();

            var actual = course1.Equals(course2);

            actual.Should().BeFalse();
            // course1.Should().NotBe(course2);
        }

        [Fact]
        public void Should_NotBeEqualWhenObjectIsNull () {
            var courseBuilder = new CourseTestBuilder();
            var course1 = courseBuilder.withId(1).Build();

            var actual = course1.Equals(null);

            actual.Should().BeFalse();
            //course1.Should().NotBe(null);
        }
    }
}