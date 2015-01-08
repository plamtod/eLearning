using System.Collections.Generic;

namespace Learning.Domain.Entities
{
    public class Subject
    {
        public Subject()
        {
            Courses = new List<Course>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; } 
    }
}
