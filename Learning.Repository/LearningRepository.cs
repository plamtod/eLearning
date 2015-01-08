using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Learning.Domain.Entities;
using Elearning.Domain.Entities;

namespace Learning.Data
{
    public class LearningRepository : ILearningRepository
    {
        LearningContext _ctx;

        
        public LearningRepository(LearningContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Subject> GetAllSubjects()
        {
            return _ctx.Subjects.AsQueryable();
        }

        public Subject GetSubject(int subjectId)
        {
            return _ctx.Subjects.Find(subjectId);
        }
       
        public IQueryable<Course> GetCoursesBySubject(int subjectId)
        {
            return _ctx.Courses
                .Include(c => c.CourseSubject)
                .Include(c => c.CourseTutor)
                .Where(c => c.CourseSubject.Id == subjectId)
                .AsQueryable();
        }

        public IQueryable<vStudents> GetvStudents() { 
            return  _ctx.vStudents.AsQueryable();
        }

        public IQueryable<Course> GetAllCourses()
        {
            return _ctx.Courses
                .Include(c => c.CourseTutor)
                .Include(c => c.CourseSubject)
                .AsQueryable();
        }

        public Course GetCourse(int courseId, bool includeEnrollments = true)
        {
            if (includeEnrollments)
            {
                return _ctx.Courses
                    .Include(c => c.Enrollments)
                    .Include(c => c.CourseSubject)
                    .Include(c => c.CourseTutor)
                    .SingleOrDefault(c => c.Id == courseId);
            }
            else
            {
                return _ctx.Courses
                .Include(c => c.CourseSubject)
                .Include(c => c.CourseTutor)
                .SingleOrDefault(c => c.Id == courseId);
            }
        }

        public bool CourseExists(int courseId)
        {
            return _ctx.Courses.Any(c => c.Id == courseId);
        }

        public IQueryable<Student> GetAllStudentsWithEnrollments()
        {
            //return _ctx.Students
            //        .Include("Enrollments")
            //        .Include("Enrollments.Course")
            //         .Include("Enrollments.Course.CourseSubject")
            //         .Include("Enrollments.Course.CourseTutor")
            //        .AsQueryable();
            return _ctx.Students
                .Include(c => c.Enrollments)
                .Include(c => c.Enrollments.Select(t => t.Course))
                .Include(c => c.Enrollments.Select(t => t.Course.CourseSubject))
                .Include(c => c.Enrollments.Select(t => t.Course.CourseTutor))
                .AsQueryable();

        }

        public IQueryable<Student> GetAllStudentsSummary()
        {
            return _ctx.Students
                .AsQueryable();
        }

        public IQueryable<Student> GetEnrolledStudentsInCourse(int courseId)
        {
            return _ctx.Students
                .Include(e => e.Enrollments)
                .Where(e => e.Enrollments.Any(t => t.Course.Id == courseId))
                .AsQueryable();

        }

        public Student GetStudentEnrollments(string userName)
        {
            return _ctx.Students
                .Include(c=>c.Enrollments)
                .Include(c => c.Enrollments.Select(t=>t.Course))
                .Include(c => c.Enrollments.Select(t => t.Course.CourseSubject))
                .Include(c => c.Enrollments.Select(t => t.Course.CourseTutor))
                .SingleOrDefault(c=>c.UserName == userName);
        }

        public Student GetStudent(string userName)
        {
            return _ctx.Students
                .Include(c => c.Enrollments)
                .SingleOrDefault(c => c.UserName == userName);
        }

        public Tutor GetTutor(int tutorId)
        {
            return _ctx.Tutors.Find(tutorId);
        }

        public bool LoginStudent(string userName, string password)
        {
            var student = _ctx.Students.Where(s => s.UserName == userName).SingleOrDefault();

            if (student != null)
            {
                if (student.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Insert(Student student)
        {
            try
            {
                _ctx.Students.Add(student);
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Update(Student originalStudent, Student updatedStudent)
        {
            _ctx.Entry(originalStudent).CurrentValues.SetValues(updatedStudent);
            return true;
        }

        public bool DeleteStudent(int id)
        {
            var student = _ctx.Students.Find(id);
            if(student!= null){
                _ctx.Students.Remove(student);
                return true;
            }
            return false;
        }

        public int EnrollStudentInCourse(int studentId, int courseId, Enrollment enrollment)
        {
            if (_ctx.Enrollments.Any(c => c.Course.Id == courseId && c.Student.Id == studentId)) {

                return 2;
            }

            try
            {
                _ctx.Database.ExecuteSqlCommand("INSERT INTO Enrollments VALUES (@p0, @p1, @p2)", enrollment.EnrollmentDate, courseId.ToString(), studentId.ToString());
                return 1;
            }
            catch (DbEntityValidationException validationException)
            {
                foreach (var exception in validationException.EntityValidationErrors)
                { 
                    foreach(var ve in exception.ValidationErrors){
                        Debug.WriteLine("Property:{0}, Error:{1}", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return 0;
        }

        public bool Insert(Course course)
        {
            try {
                _ctx.Courses.Add(course);
            }
            catch {
                return false;
            }
            return true;
        }

        public bool Update(Course originalCourse, Course updatedCourse)
        {
            _ctx.Entry(originalCourse).CurrentValues.SetValues(updatedCourse);
            //To update child entites in Course entity
            //originalCourse.CourseSubject = updatedCourse.CourseSubject;
            //originalCourse.CourseTutor = updatedCourse.CourseTutor;

            return true;
        }

        public bool DeleteCourse(int id)
        {
            
                var entity = _ctx.Courses.Find(id);
                if (entity != null)
                {
                    _ctx.Courses.Remove(entity);
                    return true;
                }
            
            return false;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
