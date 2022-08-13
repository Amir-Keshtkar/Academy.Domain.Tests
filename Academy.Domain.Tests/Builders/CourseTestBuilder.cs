namespace Academy.Domain.Tests.Unit.Builders {
    public class CourseTestBuilder {
        private string Name = "TDD & BDD";
        private readonly bool isOnline = true;
        private double Tuition = 600;
        private string Instructor = "instructor";

        public CourseTestBuilder withName (string name) {
            Name = name;
            return this;
        }

        public CourseTestBuilder withTuition (double tuition) {
            Tuition = tuition;
            return this;
        }

        public CourseTestBuilder withInstructor(string instructor)
        {
            Instructor=instructor;
            return this;
        }

        public Course Build () {
            return new Course(Name, isOnline, Tuition, Instructor);
        }
    }
}
