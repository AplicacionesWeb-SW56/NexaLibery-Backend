using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using NexaLibery_Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;

namespace NexaLibery_Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // MultimediaContent Context
        builder.Entity<Library>().ToTable("Library");
        builder.Entity<Library>().HasKey(l => l.id);
        builder.Entity<Library>().Property(l => l.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Library>().Property(l => l.title).IsRequired().HasMaxLength(1000);
        builder.Entity<Library>().Property(l => l.description).IsRequired().HasMaxLength(1000);
        builder.Entity<Library>().Property(l => l.date)
            .IsRequired()
            .HasColumnType("TIMESTAMP")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        builder.Entity<Library>().Property(l => l.pic).IsRequired().HasMaxLength(1000);
        builder.Entity<Library>().Property(l => l.url).IsRequired().HasMaxLength(1000);
        builder.Entity<Library>().Property(l => l.premium).IsRequired().HasMaxLength(1000);
     
        builder.Entity<Multimedia>().ToTable("Multimedia");
        builder.Entity<Multimedia>().HasKey(m => m.id);
        builder.Entity<Multimedia>().Property(m => m.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Multimedia>().Property(m => m.title).IsRequired().HasMaxLength(1000);
        builder.Entity<Multimedia>().Property(m => m.description).IsRequired().HasMaxLength(1000);
        builder.Entity<Multimedia>().Property(m => m.date)
            .IsRequired()
            .HasColumnType("TIMESTAMP")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        builder.Entity<Multimedia>().Property(m => m.pic).IsRequired().HasMaxLength(1000);
        builder.Entity<Multimedia>().Property(m => m.url).IsRequired().HasMaxLength(1000);
        builder.Entity<Multimedia>().Property(m => m.premium).IsRequired().HasMaxLength(1000);
        
        builder.Entity<Podcast>().ToTable("Podcast");
        builder.Entity<Podcast>().HasKey(m => m.id);
        builder.Entity<Podcast>().Property(m => m.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Podcast>().Property(m => m.title).IsRequired().HasMaxLength(1000);
        builder.Entity<Podcast>().Property(m => m.description).IsRequired().HasMaxLength(1000);
        builder.Entity<Podcast>().Property(m => m.date)
            .IsRequired()
            .HasColumnType("TIMESTAMP")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        builder.Entity<Podcast>().Property(m => m.pic).IsRequired().HasMaxLength(1000);
        builder.Entity<Podcast>().Property(m => m.url).IsRequired().HasMaxLength(1000);
        builder.Entity<Podcast>().Property(m => m.premium).IsRequired().HasMaxLength(1000);
        
        // IAM Context
        
        builder.Entity<User>().ToTable("User");
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Email).IsRequired();
        builder.Entity<User>().Property(u => u.Description).IsRequired();
        builder.Entity<User>().Property(u => u.BornDate).IsRequired();
        builder.Entity<User>().Property(u => u.CardNumber).IsRequired();
        builder.Entity<User>().Property(u => u.Photo).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}
