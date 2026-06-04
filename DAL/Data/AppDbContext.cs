using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReservationTable> ReservationTables { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // CATEGORY
            // ============================

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired();
            });

            // ============================
            // CUSTOMER
            // ============================

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);

                entity.Property(c => c.FullName)
                      .IsRequired();

                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });

            // ============================
            // MENU ITEM
            // ============================

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                      .IsRequired();

                entity.Property(m => m.Price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(m => m.IsAvailable)
                      .HasDefaultValue(true);

                entity.HasOne(m => m.Category)
                      .WithMany(c => c.MenuItems)
                      .HasForeignKey(m => m.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================
            // ORDER
            // ============================

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.TotalPrice)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(o => o.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.RestaurantTable)
                      .WithMany(rt => rt.Orders)
                      .HasForeignKey(o => o.TableId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================
            // ORDER ITEM
            // ============================

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.Price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(oi => oi.Quantity)
                      .IsRequired();

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.MenuItem)
                      .WithMany(m => m.OrderItems)
                      .HasForeignKey(oi => oi.MenuItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================
            // PAYMENT
            // ============================

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(p => p.PaymentMethod)
                      .IsRequired();

                entity.Property(p => p.PaidAt)
                      .HasDefaultValueSql("GETDATE()");

                // 🔥 IMPORTANT: Avoid multiple cascade path
                entity.HasOne(p => p.Customer)
                      .WithMany(c => c.Payments)
                      .HasForeignKey(p => p.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Order)
                      .WithMany(o => o.Payments)
                      .HasForeignKey(p => p.OrderId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // RESERVATION
            // ============================

            modelBuilder.Entity<ReservationTable>(entity =>
            {
                entity.HasKey(r => r.ReservationId);

                entity.Property(r => r.NumberOfGuests)
                      .IsRequired();

                entity.Property(r => r.ReservedDate)
                      .IsRequired();

                entity.Property(r => r.Status)
                      .HasDefaultValue("Pending");

                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Reservations)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.RestaurantTable)
                      .WithMany(rt => rt.Reservations)
                      .HasForeignKey(r => r.TableId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Prevent double booking same table on same date
                entity.HasIndex(r => new { r.TableId, r.ReservedDate })
                      .IsUnique();
            });

            // ============================
            // RESTAURANT TABLE
            // ============================

            modelBuilder.Entity<RestaurantTable>(entity =>
            {
                entity.HasKey(rt => rt.TableId);

                entity.Property(rt => rt.Capacity)
                      .IsRequired();

                 

                entity.Property(rt => rt.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(rt => rt.TableNumber)
                      .IsUnique();
            });

            // ============================
            // ROLE
            // ============================

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.RoleName)
                      .IsRequired();

                entity.HasMany(r => r.Users)
                      .WithOne(u => u.Role)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================
            // USER
            // ============================

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FullName)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .IsRequired();

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });

            // ============================
            // SUPPLIER
            // ============================

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(s => s.SupplierId);

                entity.Property(s => s.Name)
                      .IsRequired();

                entity.Property(s => s.ContactInfo)
                      .IsRequired();
            });

            // ============================
            // STOCK ITEM
            // ============================

            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.HasKey(s => s.StockItemId);

                entity.Property(s => s.Name)
                      .IsRequired();

                entity.Property(s => s.Quantity)
                      .IsRequired();

                entity.Property(s => s.ReorderLevel)
                      .IsRequired();

                entity.HasOne(s => s.Supplier)
                      .WithMany(sup => sup.StockItems)
                      .HasForeignKey(s => s.SupplierId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
