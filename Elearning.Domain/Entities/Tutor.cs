using System.Collections.Generic;

namespace Learning.Domain.Entities
{
    public class Tutor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
