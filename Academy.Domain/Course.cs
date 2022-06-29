
namespace Academy.Domain {
    public class Course {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public double Tuition { get; set; }
        public string Instructor { get; set; }
        public List<Section> Sections { get; set; }

        public Course (int id, string name, bool isOnline, double tuition, string instructor) {
            GuardAgainInvalidName(name);
            GuardAgainInvalidTuition(tuition);
            Id = id;
            Name = name;
            IsOnline = isOnline;
            Tuition = tuition;
            Instructor = instructor;
            Sections = new List<Section>();
        }

        public static void GuardAgainInvalidName (string name) {
            if(string.IsNullOrWhiteSpace(name)) {
                throw new CourseNameIsInvalidException();
            }
        }

        public static void GuardAgainInvalidTuition (double tuition) {
            if(tuition <= 0) {
                throw new CourseTuitionIsInvalidException();
            }
        }

        public void AddSection (Section sectionToAdd) {
            Sections.Add(sectionToAdd);
        }
    }
}