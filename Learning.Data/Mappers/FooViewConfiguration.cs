using Elearning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Data.Mappers
{
    public class FooViewConfiguration : EntityTypeConfiguration<vStudents>
    {
        public FooViewConfiguration()
        {
            this.HasKey(t => t.Id);
            this.ToTable("vStudents");
        }
    }
}
