using Learning.Data.Entities;
using Learning.Web.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Learning.Web
{
    public class ModelFactory
    {
        private System.Web.Http.Routing.UrlHelper _UrlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _UrlHelper = new System.Web.Http.Routing.UrlHelper(request);
        }

        public SubjectModel Create(Subject subject) {

            return new SubjectModel
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        public TutorModel Create(Tutor tutor)
        {
            return new TutorModel
            {
                Id = tutor.Id,
                Email = tutor.Email,
                UserName = tutor.UserName,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Gender = tutor.Gender
            };
        }

        public EnrollmentModel Create(Enrollment enrollment)
        {
            return new EnrollmentModel
            {
                EnrollmentDate = enrollment.EnrollmentDate,
                Course = Create(enrollment.Course)
            };
        }

        public CourseModel Create(Course course)
        {
            return new CourseModel
            {
                Id = course.Id,
                Url = _UrlHelper.Link("Courses", new {id = course.Id}),
                Name = course.Name,
                Duration = course.Duration,
                Description = course.Description,
                Tutor = Create(course.CourseTutor),
                Subject = Create(course.CourseSubject)
            };

        }
    }
}