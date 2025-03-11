using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillAssessmentPlatform.Core.Entities.Users;
using System.Reflection.Emit;
using System.Reflection;


namespace SkillAssessmentPlatform.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Examiner> Examiners { get; set; }
        public DbSet<Applicant> Applicants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //==> this part have to be seprated in EntityMappers
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Admin.ToString(), NormalizedName = Actors.Admin.ToString().ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Examiner.ToString(), NormalizedName = Actors.Examiner.ToString().ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.SeniorExaminer.ToString(), NormalizedName = Actors.SeniorExaminer.ToString().ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Applicant.ToString(), NormalizedName = Actors.Applicant.ToString().ToUpper() }
        );
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
