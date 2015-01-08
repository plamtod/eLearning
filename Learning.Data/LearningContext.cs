using System;
using System.Reflection;
using System.Data.Entity;
using Learning.Data.Mappers;
using System.Linq;
using System.Data.Entity.ModelConfiguration;
using Learning.Repository;
using Learning.Domain.Entities;
using Elearning.Domain.Entities;

namespace Learning.Data
{
    public class LearningContext : DbContext, IDbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<vStudents> vStudents { get; set; }
        
        public LearningContext()
            : base("eLearningConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LearningContext, LearningContextMigrationConfiguration>());

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type =>type.BaseType!= null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var configuration in configurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(configuration);
                modelBuilder.Configurations.Add(configurationInstance);

            }

            //modelBuilder.Configurations.Add(new CourseMapper());
            //modelBuilder.Configurations.Add(new SubjectMapper());
            //modelBuilder.Configurations.Add(new TutorMapper());
            //modelBuilder.Configurations.Add(new EnrollmentMapper());
            //modelBuilder.Configurations.Add(new StudentMapper());

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }
    }
}
