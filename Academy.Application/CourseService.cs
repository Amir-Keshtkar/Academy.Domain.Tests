using Academy.Domain;
using Academy.Domain.Exceptions;

namespace Academy.Application
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public int Create(CreateCourse command)
        {
            if (_courseRepository.GetBy(command.Id) != null)
            {
                throw new DuplicatedCourseNameException();
            }
            var course = new Course(command.Name, command.IsOnline, command.Tuition, command.Instructor);
            return _courseRepository.Create(course);
        }

        public int Edit(EditCourse command)
        {
            if (_courseRepository.GetBy(command.Id) == null)
            {
                throw new CourseNotExistException();
            }
            _courseRepository.Delete(command.Id);
            var course = new Course(command.Name, command.IsOnline, command.Tuition, command.Instructor);
            return _courseRepository.Create(course);

            //var course = _courseRepository.GetBy(command.Id);
            //course.Id = command.Id;
            //course.Name = command.Name;
            //course.IsOnline = command.IsOnline;
            //course.Tuition = command.Tuition;
            //course.Instructor = command.Instructor;
            //_courseRepository.Edit(command);
        }

        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }

        public List<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public Course GetBy(int id)
        {
            return _courseRepository.GetBy(id);
        }
    }
}