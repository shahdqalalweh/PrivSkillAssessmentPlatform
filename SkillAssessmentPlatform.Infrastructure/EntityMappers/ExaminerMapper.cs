using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillAssessmentPlatform.Core.Entities.Users;

namespace YourNamespace.EntityMapper
{
    public class ExaminerMapper : IEntityTypeConfiguration<Examiner>
    {
        public void Configure(EntityTypeBuilder<Examiner> builder)
        {
            builder.ToTable("Examiners")
                   .HasBaseType<User>();
        }
    }
}
