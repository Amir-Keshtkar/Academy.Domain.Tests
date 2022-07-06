namespace Academy.Application
{
    public interface ICourseService
    {
        int Create(CreateCourse command);
        void Edit(EditCourse command);
    }
}
