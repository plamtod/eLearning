using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Learning.Domain.Entities;

namespace Learning.Data.Mappers
{
    public class CourseMapper: EntityTypeConfiguration<Course>
    {
        public CourseMapper()
        {
            this.ToTable("Courses");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Name).HasMaxLength(255);

            this.Property(c => c.Duration).IsRequired();

            this.Property(c => c.Description).IsOptional();
            this.Property(c => c.Description).HasMaxLength(1000);
            
            //set FK coulumns names
            this.HasRequired(c => c.CourseSubject).WithMany(v=>v.Courses).Map(c=>c.MapKey("SubjectID"));
            this.HasRequired(c => c.CourseTutor).WithMany(t => t.Courses).Map(m => m.MapKey("TutorID"));
        }
    }
}
