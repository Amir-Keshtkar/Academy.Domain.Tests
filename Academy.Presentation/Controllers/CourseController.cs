using Academy.Application;
using Academy.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public List<Course> GetAll()
        {
            return _courseService.GetAll();
        }

        [HttpGet("{id}")]
        public Course GetBy(int id)
        {
            return _courseService.GetBy(id);
        }

        [HttpPost]
        public int CreateCourse(CreateCourse command)
        {
            return _courseService.Create(command);
        }

        [HttpPut]
        public int Edit(EditCourse command)
        {
            return _courseService.Edit(command);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _courseService.Delete(id);
        }
    }
}
