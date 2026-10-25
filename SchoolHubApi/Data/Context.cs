using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.SchoolModels;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Models.WorkModels;
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

    public DbSet<Table> Tables => Set<Table>();
    public DbSet<TableMember> TableMembers => Set<TableMember>();
    public DbSet<Category> TableCategories => Set<Category>();

    public DbSet<Work> Works => Set<Work>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SchoolHubUser>()
            .HasDiscriminator<UserRole>("UserRole")
            .HasValue<SchoolHubUser>(UserRole.User)
            .HasValue<StudentUser>(UserRole.Student)
            .HasValue<TeacherUser>(UserRole.Teacher);
        
        modelBuilder.Entity<Table>()
            .HasOne(t => t.Owner)
            .WithMany(u => u.OwnTables)
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<TableMember>()
            .HasIndex(m => new { m.TableId, m.UserId })
            .IsUnique();

        modelBuilder.Entity<TableMember>()
            .HasOne(m => m.Table)
            .WithMany(t => t.Members)
            .HasForeignKey(m => m.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TableMember>()
            .HasOne(m => m.User)
            .WithMany(u => u.Memberships)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TableMember>()
            .HasIndex(m => new { m.TableId, m.UserId })
            .IsUnique();
        
        modelBuilder.Entity<Table>()
            .HasIndex(t => new { t.OwnerId, t.Name })
            .IsUnique();
    
        modelBuilder.Entity<Category>()
            .HasOne(c => c.Table)
            .WithMany(t => t.Categories)
            .HasForeignKey(c => c.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

        modelBuilder.Entity<TableMessage>()
            .HasOne(m => m.Category)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TableMessage>()
            .HasOne(m => m.Work)
            .WithMany()
            .HasForeignKey(m => m.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TableMessage>()
            .HasOne(m => m.Sender)
            .WithMany(m => m.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Messages)
            .WithOne()
            .HasForeignKey("CategoryId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Work>()
            .HasOne(w => w.Creator)
            .WithMany(u => u.Works)
            .HasForeignKey(w => w.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Work>()
            .HasDiscriminator<WorkType>("WorkType")
            .HasValue<WorkInstruction>(WorkType.Instruction)
            .HasValue<WorkQuiz>(WorkType.Quiz);

        modelBuilder.Entity<WorkQuiz>()
            .OwnsOne(w => w.Questions, b => b.ToJson());
    }
}
