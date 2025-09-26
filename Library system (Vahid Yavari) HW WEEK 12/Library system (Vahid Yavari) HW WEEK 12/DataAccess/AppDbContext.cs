using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.DataAccess
{
   public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(@"Server=Localhost;Database=LibrarySystem;Integrated Security=true  ;TrustServerCertificate=true; ");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property<string>("Password");

            // ---------------- User ----------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Mobile)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Role)
                      .IsRequired();

                entity.Property(u => u.IsActive)
                      .HasDefaultValue(true);

                //entity.Ignore(u => u.IsLoggedIn); 

                entity.HasMany(u => u.ListOfBorrowedBooks)
                      .WithOne(bb => bb.User)
                      .HasForeignKey(bb => bb.UserId);
            });

            // ---------------- Category ----------------
            modelBuilder.Entity<Category>(entity =>
            {
              //  entity.ToTable("Categories");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.CategoryName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(c => c.BooksList)
                      .WithOne(b => b.Category)
                      .HasForeignKey(b => b.CategoryId);
            });

            // ---------------- Book ----------------
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(b => b.Author)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(b => b.IsBorrowed)
                      .HasDefaultValue(false);

                entity.HasOne(b => b.Category)
                      .WithMany(c => c.BooksList)
                      .HasForeignKey(b => b.CategoryId);

                entity.HasMany(b => b.BorrowedBooks)
                      .WithOne(bb => bb.Book)
                      .HasForeignKey(bb => bb.BookId);

                entity.HasMany<Review>()
                      .WithOne(r => r.Book)
                      .HasForeignKey(r => r.BookId);
            });

            // ---------------- BorrowedBook ----------------
            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.ToTable("BorrowedBooks");

                entity.HasKey(bb => bb.Id);

                entity.Property(bb => bb.BorrowDate)
                      .IsRequired();

                entity.Property(bb => bb.ReturnDate)
                      .IsRequired(false);

                entity.HasOne(bb => bb.User)
                      .WithMany(u => u.ListOfBorrowedBooks)
                      .HasForeignKey(bb => bb.UserId);

                entity.HasOne(bb => bb.Book)
                      .WithMany(b => b.BorrowedBooks)
                      .HasForeignKey(bb => bb.BookId);
            });

            // ---------------- Review ----------------
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Comment)
                      .HasMaxLength(1000)
                      .IsRequired(false);

                entity.Property(r => r.Rating)
                      .IsRequired();

                entity.HasCheckConstraint("CK_Reviews_Rating", "[Rating] >= 1 AND [Rating] <= 5");

                entity.Property(r => r.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)   
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                
                entity.HasOne(r => r.Book)
                      .WithMany(b => b.Reviews)  
                      .HasForeignKey(r => r.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }



    }
}
