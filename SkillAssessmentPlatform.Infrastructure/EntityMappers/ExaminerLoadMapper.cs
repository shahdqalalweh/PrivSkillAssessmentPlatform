using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace SkillAssessmentPlatform.Infrastructure.EntityMappers
{
    public class ExaminerLoadMapper : IEntityTypeConfiguration<ExaminerLoad>
    {
        public void Configure(EntityTypeBuilder<ExaminerLoad> builder)
        {
            builder.HasKey(e => new { e.ExaminerID, e.Type });
            builder.HasOne(e => e.Examiner)
               .WithMany(ex => ex.ExaminerLoads)
               .HasForeignKey(e => e.ExaminerID) 
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
   
}
