using Academy.Domain;

namespace Academy.Infrastrucure.Tests.Unit {
    public class CourseRepository {
        public List<Course> Courses = new List<Course>() {
            new Course(1,"amir", true, 100.1f, "instruc"),
        };

        public void Create (Course course) {
            Courses.Add(course);
        }

        public List<Course> GetAll () {
            return Courses;
        }

        public Course GetBy (int id) {
            return Courses.FirstOrDefault(x => x.Id == id);
        }
    }
}