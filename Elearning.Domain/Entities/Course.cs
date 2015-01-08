using System.Collections.Generic;

namespace Learning.Domain.Entities
{
    public class Course
    {
        public Course()
        {
            CourseSubject = new Subject();
            CourseTutor = new Tutor();
            Enrollments = new List<Enrollment>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }

        public Subject CourseSubject { get; set; }
        public Tutor CourseTutor { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
