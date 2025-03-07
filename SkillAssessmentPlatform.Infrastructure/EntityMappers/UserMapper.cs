using Microsoft.EntityFrameworkCore;
using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.EntityMappers
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            throw new NotImplementedException();
        }
    }
}
