using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JHOP.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<UserEducation> UserEducations => Set<UserEducation>();
        public DbSet<UserJobExperience> UserJobExperiences => Set<UserJobExperience>();
        public DbSet<UserLanguages> UserLanguages => Set<UserLanguages>();
        public DbSet<UserInterests> UserInterests => Set<UserInterests>();
        public DbSet<UserSkills> UserSkills => Set<UserSkills>();
        public DbSet<UserStrengths> UserStrengths => Set<UserStrengths>();
        public DbSet<UserProfilePersonalData> UserProfilePersonalData => Set<UserProfilePersonalData>();

        public DbSet<ProfilePhoto> ProfilePhoto => Set<ProfilePhoto>();

        public DbSet<UserProjects> UserProjects { get; set; }

        public DbSet<UserCvOffer> UserCvOffers { get; set; }
        public DbSet<CvFile> CvFiles { get; set; }

        public DbSet<LaborMarketOffer> laborMarketOffers { get; set; }









        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // PROFILE
            // ============================
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.UserId)
                      .HasColumnType("nvarchar(450)")
                      .IsRequired();

                entity.Property(p => p.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.ProfileIndex)
                      .IsRequired();

                // Relacja: Profile -> AppUser (wiele profili na usera)
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Profiles)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Unikalny indeks: max 1 profil o danym indeksie na usera
                entity.HasIndex(p => new { p.UserId, p.ProfileIndex })
                      .IsUnique();

                // Ograniczenie: tylko 1..3
                entity.HasCheckConstraint(
                    "CK_Profiles_ProfileIndex_1_3",
                    "[ProfileIndex] BETWEEN 1 AND 3"
                );
            });


            // ============================
            // USER EDUCATION
            // ============================
            modelBuilder.Entity<UserEducation>(entity =>
            {
                entity.ToTable("UserEducations");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.SchoolName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.StudyProfile)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Educations)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.ProfileId);
            });


            // ============================
            // USER JOB EXPERIENCE
            // ============================
            modelBuilder.Entity<UserJobExperience>(entity =>
            {
                entity.ToTable("UserJobExperiences");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.CompanyName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.JobTitle)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(e => e.JobDescription)
                      .HasMaxLength(4000)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.JobExperiences)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.ProfileId);
            });


            // ============================
            // USER LANGUAGES
            // ============================
            modelBuilder.Entity<UserLanguages>(entity =>
            {
                entity.ToTable("UserLanguages");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Language)
                      .HasMaxLength(80)
                      .IsRequired();

                entity.Property(e => e.Level)
                      .HasMaxLength(40)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Languages)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.ProfileId, e.Language })
                      .IsUnique();
            });


            // ============================
            // USER INTERESTS
            // ============================
            modelBuilder.Entity<UserInterests>(entity =>
            {
                entity.ToTable("UserInterests");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Interest)
                      .HasMaxLength(120)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(2000);

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Interests)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.ProfileId, e.Interest })
                      .IsUnique();
            });


            // ============================
            // USER SKILLS
            // ============================
            modelBuilder.Entity<UserSkills>(entity =>
            {
                entity.ToTable("UserSkills");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Skill)
                      .HasMaxLength(120)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Skills)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.ProfileId, e.Skill })
                      .IsUnique();
            });


            // ============================
            // USER STRENGTHS
            // ============================
            modelBuilder.Entity<UserStrengths>(entity =>
            {
                entity.ToTable("UserStrengths");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Strength)
                      .HasMaxLength(120)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Strengths)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.ProfileId, e.Strength })
                      .IsUnique();
            });

            // ============================
            // USER PROFILE PERSONAL DATA (1:1)
            // ============================
            modelBuilder.Entity<UserProfilePersonalData>(entity =>
            {
                entity.ToTable("UserProfilePersonalData");


                entity.Property(e => e.FirstName)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(256)
                      .IsRequired();

                entity.Property(e => e.PhoneNumber)
                      .HasMaxLength(50);



                entity.Property(e => e.PersonalProfile)
                      .HasMaxLength(550);

                entity.Property(e => e.GitHubURL)
                      .HasMaxLength(550);

                entity.Property(e => e.LinkedInURL)
                      .HasMaxLength(550);

                // 🔥 RELACJA 1:1
                entity.HasOne(e => e.Profile)
                      .WithOne(p => p.PersonalData)
                      .HasForeignKey<UserProfilePersonalData>(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);



                // 🔒 Gwarancja 1:1 (unikalny ProfileId)
                entity.HasIndex(e => e.ProfileId)
                      .IsUnique();
                entity.HasKey(e => e.ProfileId);


            });
            // ============================
            // USER PROFILE PHOTO DATA (1:1)
            // ============================

            modelBuilder.Entity<ProfilePhoto>(entity =>
            {
                entity.ToTable("ProfilePhoto");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Data)
                      .IsRequired();

                entity.HasOne(e => e.Profile)
                      .WithOne(p => p.ProfilePhoto)
                      .HasForeignKey<ProfilePhoto>(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
