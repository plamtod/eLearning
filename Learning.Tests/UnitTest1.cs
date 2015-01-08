using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Learning.Data;
using System.Linq;
using Learning.Domain.Entities;
using System.Data.Entity;

namespace Learning.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LearningContext context = new LearningContext();

            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            

            //var tutor = context.Tutors.Find(1);
           // var u = context.Entry(tutor).Collection(s => s.Courses).Query().ToList();
            var tutor = new Tutor() { UserName = "AhmadJoudeh", Id = 1, FirstName = "Ahmad1" };
            var d = context.Tutors.Include(c => c.Courses).FirstOrDefault(c => c.Id == 1);
            context.Entry(context.Tutors.Find(1)).CurrentValues.SetValues(tutor);

            context.SaveChanges();
           
        }
    }
}
