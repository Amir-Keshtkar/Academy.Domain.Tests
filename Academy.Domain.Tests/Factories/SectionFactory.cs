using Academy.Domain.Tests.Unit.CollectionFixtures;

namespace Academy.Domain.Tests.Unit.Factories
{
    [Collection("Database Collection")]
    public class SectionFactory
    {
        const int Id = 0;
        const string Name = "TDD";

        public SectionFactory(DatabaseFixture databaseFixture)
        {

        }

        public static Section Create()
        {
            return new Section(Id, Name);
        }

    }
}
