
namespace Academy.Domain.Tests.Tests {
    public class SectionTests {
        [Fact]
        public void Consturctor_Should_Construct_Section_Properly () {
            const int id = 1;
            const string name = "";

            var section = new Section(id, name);
            section.Id.Should().Be(id);
            section.Name.Should().Be(name);
        }
    }
}
