namespace Academy.Domain.Tests.Builders {
    internal class CourseTestBuilder {
        private readonly int Id = 1;
        private string Name = "TDD & BDD";
        private readonly bool isOnline = true;
        private double Tuition = 600;
        private string instructor = "instructor";

        public CourseTestBuilder withName (string name) {
            Name = name;
            return this;
        }

        public CourseTestBuilder withTuition (double tuition) {
            Tuition = tuition;
            return this;
        }

        public Course Build () {
            return new Course(Id, Name, isOnline, Tuition, instructor);
        }
    }
}
