using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using smack.infrastructure.Data.Entities;
using smack.core.Entities;
namespace smack.infrastructure.Data;

public partial class SmackDbContext : DbContext
{
    public SmackDbContext()
    {
    }

    public SmackDbContext(DbContextOptions<SmackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Restauranttable> Restauranttables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRestaurant> UserRestaurants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Meghana\\SQLExpress;Database=SmackDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F31298449");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__menuitem__3213E83FF97CC9F2");

            entity.ToTable("menuitems");

            entity.HasIndex(e => e.RestaurantId, "IX_MenuItems_RestaurantId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Isavailable)
                .HasDefaultValue(true)
                .HasColumnName("isavailable");
            entity.Property(e => e.Itemname)
                .HasMaxLength(100)
                .HasColumnName("itemname");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_menuitems_categories");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("FK_Menuitems_users");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_menuitems_restaurants");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229BF27A2D0");

            entity.ToTable("orders");

            entity.HasIndex(e => e.RestaurantId, "IX_Orders_RestaurantId");

            entity.HasIndex(e => new { e.Status, e.Islive }, "IX_Orders_Status_IsLive");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Islive)
                .HasDefaultValue(true)
                .HasColumnName("islive");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("orderdate");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Totalamount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalamount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_orders_restaurants");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_status");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_tables");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_order_users");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orderite__3213E83F819760F0");

            entity.ToTable("orderitems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([quantity]*[price])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_MenuItems");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Orders");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ordersta__3213E83F127ACEA5");

            entity.ToTable("orderstatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Statusname)
                .HasMaxLength(100)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__restaura__3B0FAA9108731C99");

            entity.ToTable("restaurants");

            entity.HasIndex(e => e.Email, "UQ__restaura__AB6E6164D364A19D").IsUnique();

            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(200)
                .HasColumnName("restaurant_name");
        });

        modelBuilder.Entity<Restauranttable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__restaura__B21E8F2402FDB0D3");

            entity.ToTable("restauranttables");

            entity.HasIndex(e => e.Qrcode, "IX_RestaurantTables_QRCode");

            entity.HasIndex(e => e.RestaurantId, "IX_RestaurantTables_RestaurantId");

            entity.HasIndex(e => e.Qrcode, "UQ__restaura__5B869AD937EBFB9B").IsUnique();

            entity.HasIndex(e => new { e.RestaurantId, e.TableNumber }, "UQ_restauranttable").IsUnique();

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Isoccupied)
                .HasDefaultValue(false)
                .HasColumnName("isoccupied");
            entity.Property(e => e.Qrcode)
                .HasMaxLength(100)
                .HasColumnName("QRCode");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.TableNumber).HasColumnName("table_number");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Restauranttables)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_restauranttables_restaurants");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roles__760965CC7B43FDDB");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Rolename)
                .HasMaxLength(100)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FB63D25B7");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616485855C2D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(200)
                .HasColumnName("google_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Usertype).HasColumnName("usertype");

            entity.HasOne(d => d.UsertypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Usertype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Roles");
        });

        modelBuilder.Entity<UserRestaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_res__3213E83FD6177F7B");

            entity.ToTable("user_restaurants");

            entity.HasIndex(e => e.RestaurantId, "IX_UserRestaurants_RestaurantId");

            entity.HasIndex(e => e.RoleId, "IX_UserRestaurants_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UserRestaurants_UserId");

            entity.HasIndex(e => new { e.UserId, e.RestaurantId }, "UQ_Userrestaurant").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.UserRestaurants)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_UserRestaurant_Restaurantid");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRestaurants)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRestaurants_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRestaurants)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRestaurant_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
