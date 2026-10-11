using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.SchoolModels;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Models.TableModels.Works;
using SchoolHubApi.Enums;

namespace SchoolHubApi.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PendingRegistration> PendingRegistrations => Set<PendingRegistration>();
    public DbSet<SchoolHubUser> Users => Set<SchoolHubUser>();

    public DbSet<TeacherReport> TeacherReports => Set<TeacherReport>();
    public DbSet<StudentReport> StudentReports => Set<StudentReport>();

    public DbSet<School> Schools => Set<School>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Mark> Marks => Set<Mark>();
    public DbSet<SchoolWork> Works => Set<SchoolWork>();

    public DbSet<Table> Tables => Set<Table>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SchoolHubUser>()
            .HasDiscriminator<UserRole>("UserRole")
            .HasValue<SchoolHubUser>(UserRole.User)
            .HasValue<StudentUser>(UserRole.Student)
            .HasValue<TeacherUser>(UserRole.Teacher);
        
        modelBuilder.Entity<TableWork>()
            .HasDiscriminator<WorkType>("WorkType")
            .HasValue<TableInstructions>(WorkType.Instructions)
            .HasValue<TableQuiz>(WorkType.Quiz);
    }
}
