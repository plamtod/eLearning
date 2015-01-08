//using System.ComponentModel.DataAnnotations;

namespace Elearning.Domain.Entities
{
    public class vStudentCourses
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int Duration { get; set; }
    }
}
