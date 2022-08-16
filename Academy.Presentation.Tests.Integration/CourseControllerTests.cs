using Academy.Application;
using Academy.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;

namespace Academy.Presentation.Tests.Integration
{
    public class CourseControllerTests
    {
        private const string Path = "/api/Course";
        private readonly RESTFulApiFactoryClient _restClient;

        public CourseControllerTests()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            var httpClient = applicationFactory.CreateClient();
            _restClient = new RESTFulApiFactoryClient(httpClient);
        }

        [Fact]
        public async void Should_GetAllCoursesAsync()
        {
            var actual = await _restClient.GetContentAsync<List<Course>>(Path);

            actual.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_CreateNewCourse()
        {
            var course = SomeCreateCourse();

            var id = await _restClient.PostContentAsync<CreateCourse, int>(Path, course);
            var courses = await _restClient.GetContentAsync<List<Course>>(Path);

            courses.Should().ContainSingle(x => x.Id == id);

            await _restClient.DeleteContentAsync($"{Path}/{id}");
        }

        [Fact]
        public async void Should_GetCourseById()
        {
            var course = SomeCreateCourse();

            var id = await _restClient.PostContentAsync<CreateCourse, int>(Path, course);
            var actual = await _restClient.GetContentAsync<Course>($"{Path}/{id}");

            actual.Id.Should().Be(id);
            await _restClient.DeleteContentAsync($"{Path}/{id}");
        }

        [Fact]
        public async void Should_EditExistingCourse()
        {
            var createCourse = SomeCreateCourse();
            var id = await _restClient.PostContentAsync<CreateCourse, int>(Path, createCourse);
            var editCourse = new EditCourse()
            {
                Id = id,
                Name = Guid.NewGuid().ToString(),
                IsOnline = true,
                Tuition = 23.3,
                Instructor = "insruct",
            };
            var editId = await _restClient.PutContentAsync<EditCourse, int>(Path, editCourse);
            var courses = await _restClient.GetContentAsync<List<Course>>(Path);

            courses.Should().ContainSingle(x => x.Id == editId);
            courses.Should().NotContain(x => x.Id == id);

            await _restClient.DeleteContentAsync($"{Path}/{editId}");
        }

        [Fact]
        public async void Should_DeleteCourse()
        {
            var createCourse = SomeCreateCourse();
            var id = await _restClient.PostContentAsync<CreateCourse, int>(Path, createCourse);
            
            await _restClient.DeleteContentAsync($"{Path}/{id}");
            var courses = await _restClient.GetContentAsync<List<Course>>(Path);

            courses.Should().NotContain(x=>x.Id==id);
        }


        private static CreateCourse SomeCreateCourse()
        {
            return new CreateCourse
            {
                Name = Guid.NewGuid().ToString(),
                Instructor = "insruct",
                IsOnline = true,
                Tuition = 780
            };
        }
    }
}