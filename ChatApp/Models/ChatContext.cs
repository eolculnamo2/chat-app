using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models
{
  public partial class ChatContext : DbContext
  {
    public ChatContext() { }
    public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }
    public DbSet<UserRegistration> UserRegistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseMySql("server=localhost;database=chatapp;user=root;pwd=MySQLP@$$w0rd");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserRegistration>(entity =>
      {
        entity.HasKey(e => e.Username);

        entity.ToTable("users");

        entity.HasIndex(e => e.Username)
            .HasName("usernmae")
            .IsUnique();

        entity.Property(e => e.Username)
            .HasColumnName("username")
            .HasColumnType("varchar(55)");

        entity.Property(e => e.Password)
            .HasColumnName("password")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.ImageUrl)
            .HasColumnName("image_url")
            .HasColumnType("varchar(200)");
      });
    }
  }
}