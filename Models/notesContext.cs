using Microsoft.EntityFrameworkCore;

namespace Notes.Models
{
    /// <summary>
    /// Context for Notes DB. It has 2 tables: users and notes.
    /// Users: id, email, name, password, salt. Notes: id, title, text, userId.
    /// </summary>
    public partial class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Id SERIAL PRIMARY KEY,
            // Title VARCHAR(64),
	        // Text VARCHAR(255),
	        // UserId INT,
            // FOREIGN KEY(UserId) REFERENCES users(Id)
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("notes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("character varying(255)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying(64)");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            // Id SERIAL PRIMARY KEY,
            // Email VARCHAR(255) UNIQUE,
            // Password VARCHAR(64),
            // Salt VARCHAR(32)
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("character varying(255)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("character varying(64)");

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasColumnType("character varying(32)");
            });
        }
    }
}
