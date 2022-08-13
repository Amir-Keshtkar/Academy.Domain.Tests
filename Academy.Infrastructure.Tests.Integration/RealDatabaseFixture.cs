using Academy.Domain;
using Academy.Domain.Tests.Unit.Builders;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Academy.Infrastructure.Tests.Integration
{
    public class RealDatabaseFixture : IDisposable
    {
        public AcademyContext context;
        private readonly TransactionScope _scope;

        public RealDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<AcademyContext>()
                .UseSqlServer("Data Source=.; Initial Catalog=TddAcademy; Persist Security Info=True;User ID=sa; Password=Amir1378").Options;
            context = new AcademyContext(options);
            _scope = new TransactionScope();

            var build=new CourseTestBuilder();

            var asp= build.withName("ASP.NET Core 5")
                .withTuition(780)
                .withInstructor("Hossein")
                .Build();

            var git = build.withName("Git")
                .withTuition(120)
                .withInstructor("Amir")
                .Build();

            var webDesign = build.withName("Web Design")
                .withTuition(320)
                .withInstructor("Mohammad")
                .Build();

            context.Add(asp);
            context.Add(git);
            context.Add(webDesign);
            context.SaveChanges();
        }

        public void Dispose()
        {
            _scope.Dispose();
            context.Database.ExecuteSqlRaw("truncate Table [TddAcademy].[dbo].[Courses]");
            context.Database.ExecuteSqlRaw("truncate Table [TddAcademy].[dbo].[Sections]");
            context.SaveChanges();
            context.Dispose();
        }
    }
}
