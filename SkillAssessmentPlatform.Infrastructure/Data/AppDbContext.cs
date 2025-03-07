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

            // Configure the inheritance
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Admin, NormalizedName = Actors.Admin.ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Examiner, NormalizedName = Actors.Examiner.ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.SeniorExaminer, NormalizedName = Actors.SeniorExaminer.ToUpper() },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Actors.Applicant, NormalizedName = Actors.Applicant.ToUpper() }
        );

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<Examiner>()
                .ToTable("Examiners")
                .HasBaseType<User>();

            builder.Entity<Applicant>()
                .ToTable("Applicants")
                .HasBaseType<User>();


             builder.Entity<User>()
            .HasOne(u => u.Applicant)
            .WithOne(a => a.User)
            .HasForeignKey<Applicant>(a => a.Id)  
            .IsRequired();

            builder.Entity<User>()
            .HasOne(u => u.Examiner)
            .WithOne(a => a.User)
            .HasForeignKey<Examiner>(a => a.Id)
            .IsRequired();

        }

    }
}
