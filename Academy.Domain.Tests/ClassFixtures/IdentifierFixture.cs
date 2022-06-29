
namespace Academy.Domain.Tests.ClassFixtures {
    public class IdentifierFixture: IDisposable { 
        public static Guid Id{ get; set; }

        public IdentifierFixture() {
             Id= Guid.NewGuid();
        }

        public void Dispose () {
            Id= Guid.Empty;
        }
    }
}
