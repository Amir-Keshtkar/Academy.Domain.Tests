using Academy.Domain;

namespace Academy.Infrastrucure.Tests.Unit {
    public class CourseRepository: ICourseRepository {
        public List<Course> Courses = new List<Course>() {
            new Course(1,"amir", true, 100.1f, "instruction"),
        };

        public void Create(Course course) {
            Courses.Add(course);
        }

        public List<Course> GetAll() {
            return Courses;
        }

        public Course GetBy(int id) {
            return Courses.FirstOrDefault(x => x.Id == id)!;
        }

        public void Delete(int id) {
            Courses.Remove(GetBy(id));
        }

        public Course GetBy(string name) {
            return Courses.FirstOrDefault(x => x.Name == name)!;
        }
    }
}