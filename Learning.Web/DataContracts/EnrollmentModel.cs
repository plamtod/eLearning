using System;

namespace Learning.Web.DataContracts
{
    public class EnrollmentModel
    {
        public DateTime EnrollmentDate { get; set; }
        public CourseModel Course { get; set; }
    }
}