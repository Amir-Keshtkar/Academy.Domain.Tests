﻿
namespace Academy.Domain {

    public interface ICourseRepository {

        void Create (Course course);
        List<Course> GetAll ();
        Course GetBy (int id);
        Course GetBy (string name);
        void Delete (int id);

    }
}
