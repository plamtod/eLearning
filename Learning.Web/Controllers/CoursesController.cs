using Learning.Data;
using Learning.Data.Entities;
using Learning.Web.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Learning.Web.Controllers
{
    public class CoursesController : BaseApiController
    {
        readonly ILearningRepository _repository;

        public CoursesController(ILearningRepository repository)
        {
            repository = repository;//new LearningRepository(new LearningContext());
        }
        public IHttpActionResult Get()
        {
            var courses = _repository.GetAllCourses().ToList().Select(e => ModelFactory.Create(e));
            if (courses.Any())
            {
                return Ok(courses);
            }

            return NotFound();
        }

        public IHttpActionResult GetCourse(int Id)
        {
            var course = ModelFactory.Create(_repository.GetCourse(Id, true));

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }
    }
}
