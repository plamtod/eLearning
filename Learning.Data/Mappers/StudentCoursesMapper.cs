using Elearning.Domain.Entities;
using Learning.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Learning.Data.Mappers
{
    public class StudentCoursesMapper : EntityTypeConfiguration<vStudentCourses>
    {
        public StudentCoursesMapper()
        {
            this.ToTable("vStudentCourses");

            this.HasKey(s => s.ID);
            this.Property(s => s.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.ID).IsRequired();

        }
    }
}
