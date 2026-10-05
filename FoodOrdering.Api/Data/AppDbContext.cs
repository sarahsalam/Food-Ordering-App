using FoodOrdering.Api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Data;

public class 
    AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)

    {
    }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<UserCoupon> UserCoupons { get; set; }
    public DbSet<CouponUsage> CouponUsages { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public DbSet<RbacInitialization> RbacInitializations { get; set; }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<RestaurantStaff> RestaurantStaff { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<RestaurantBranch> RestaurantBranches { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductVariant> ProductVariants { get; set; }

    public DbSet<Addon> Addons { get; set; }

    public DbSet<ProductAddon> ProductAddons { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItemAddon> CartItemAddons { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<OrderItemAddon> OrderItemAddons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Payment>().HasIndex(item => item.OrderId).IsUnique();
        modelBuilder.Entity<Payment>().Property(item => item.Amount).HasPrecision(18, 2);
        modelBuilder.Entity<Payment>().Property(item => item.Status).IsConcurrencyToken();
        modelBuilder.Entity<Payment>().HasOne(item => item.Order).WithOne(order => order.Payment).HasForeignKey<Payment>(item => item.OrderId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Payment>().HasOne(item => item.Rider).WithMany().HasForeignKey(item => item.RiderId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Payment>().ToTable(table =>
        {
            table.HasCheckConstraint("CK_Payments_Amount", "\"Amount\" >= 0");
            table.HasCheckConstraint("CK_Payments_Method", "\"PaymentMethod\" = 'CashOnDelivery'");
            table.HasCheckConstraint("CK_Payments_Status", "\"Status\" IN ('Pending', 'Collected', 'SubmittedToOwner')");
        });

        modelBuilder.Entity<PaymentHistory>().HasOne(item => item.Payment).WithMany().HasForeignKey(item => item.PaymentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PaymentHistory>().HasOne(item => item.ChangedByUser).WithMany().HasForeignKey(item => item.ChangedByUserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PaymentHistory>().Property(item => item.Note).HasMaxLength(2000);

        modelBuilder.Entity<Coupon>().HasIndex(item => item.Code).IsUnique();
        modelBuilder.Entity<Coupon>().Property(item => item.Code).HasMaxLength(50);
        modelBuilder.Entity<Coupon>().Property(item => item.Description).HasMaxLength(1000);
        modelBuilder.Entity<Coupon>().Property(item => item.DiscountValue).HasPrecision(18, 2);
        modelBuilder.Entity<Coupon>().Property(item => item.MinimumOrderAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Coupon>().Property(item => item.MaximumDiscount).HasPrecision(18, 2);
        modelBuilder.Entity<Coupon>().HasOne(item => item.CreatedByAdmin).WithMany().HasForeignKey(item => item.CreatedByAdminId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Coupon>().ToTable(table =>
        {
            table.HasCheckConstraint("CK_Coupons_Code", "\"Code\" ~ '^[A-Z0-9_-]+$'");
            table.HasCheckConstraint("CK_Coupons_Discount", "\"DiscountValue\" > 0 AND (\"DiscountType\" = 'Fixed' OR (\"DiscountType\" = 'Percentage' AND \"DiscountValue\" <= 100))");
            table.HasCheckConstraint("CK_Coupons_Limits", "\"MinimumOrderAmount\" >= 0 AND (\"MaximumDiscount\" IS NULL OR \"MaximumDiscount\" > 0) AND (\"UsageLimit\" IS NULL OR \"UsageLimit\" > 0)");
            table.HasCheckConstraint("CK_Coupons_Dates", "\"ExpiryDate\" > \"StartDate\"");
        });

        modelBuilder.Entity<UserCoupon>().HasIndex(item => new { item.UserId, item.CouponId }).IsUnique();
        modelBuilder.Entity<UserCoupon>().Property(item => item.IsUsed).IsConcurrencyToken();
        modelBuilder.Entity<UserCoupon>().HasOne(item => item.User).WithMany().HasForeignKey(item => item.UserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserCoupon>().HasOne(item => item.Coupon).WithMany().HasForeignKey(item => item.CouponId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserCoupon>().HasOne(item => item.AssignedByAdmin).WithMany().HasForeignKey(item => item.AssignedByAdminId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CouponUsage>().HasIndex(item => item.OrderId).IsUnique();
        modelBuilder.Entity<CouponUsage>().HasIndex(item => new { item.CouponId, item.UserId }).IsUnique();
        modelBuilder.Entity<CouponUsage>().Property(item => item.DiscountAmount).HasPrecision(18, 2);
        modelBuilder.Entity<CouponUsage>().HasOne(item => item.Coupon).WithMany().HasForeignKey(item => item.CouponId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CouponUsage>().HasOne(item => item.User).WithMany().HasForeignKey(item => item.UserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CouponUsage>().HasOne(item => item.Order).WithMany().HasForeignKey(item => item.OrderId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Delivery>().HasIndex(item => item.OrderId).IsUnique();
        modelBuilder.Entity<Delivery>().Property(item => item.RiderId).IsConcurrencyToken();
        modelBuilder.Entity<Delivery>().HasOne(item => item.Order).WithOne(order => order.Delivery).HasForeignKey<Delivery>(item => item.OrderId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Delivery>().HasOne(item => item.Rider).WithMany().HasForeignKey(item => item.RiderId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Delivery>().HasOne(item => item.AssignedByUser).WithMany().HasForeignKey(item => item.AssignedByUserId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderStatusHistory>().HasOne(item => item.Order).WithMany().HasForeignKey(item => item.OrderId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderStatusHistory>().HasOne(item => item.ChangedByUser).WithMany().HasForeignKey(item => item.ChangedByUserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderStatusHistory>().Property(item => item.Note).HasMaxLength(2000);


        modelBuilder.Entity<Order>().Property(order => order.Status).IsConcurrencyToken();
        modelBuilder.Entity<Order>().Property(order => order.AssignedRiderId).IsConcurrencyToken();

        modelBuilder.Entity<Permission>().HasIndex(permission => permission.Name).IsUnique();
        modelBuilder.Entity<Permission>().Property(permission => permission.Name).HasMaxLength(100);
        modelBuilder.Entity<RolePermission>().HasKey(item => new { item.RoleId, item.PermissionId });
        modelBuilder.Entity<RolePermission>().HasOne(item => item.Role).WithMany()
            .HasForeignKey(item => item.RoleId);
        modelBuilder.Entity<RolePermission>().HasOne(item => item.Permission).WithMany()
            .HasForeignKey(item => item.PermissionId);
        modelBuilder.Entity<RestaurantStaff>().HasKey(item => new { item.UserId, item.RestaurantId });
        modelBuilder.Entity<RestaurantStaff>().HasOne(item => item.User).WithMany()
            .HasForeignKey(item => item.UserId);
        modelBuilder.Entity<RestaurantStaff>().HasOne(item => item.Restaurant).WithMany()
            .HasForeignKey(item => item.RestaurantId);
        modelBuilder.Entity<Restaurant>().Property(item => item.ApprovalStatus).HasConversion<string>();
        modelBuilder.Entity<Restaurant>().HasOne<ApplicationUser>().WithMany()
            .HasForeignKey(item => item.ReviewedByUserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>().HasOne(item => item.AssignedRider).WithMany()
            .HasForeignKey(item => item.AssignedRiderId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(refreshToken => refreshToken.Token)
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(refreshToken => refreshToken.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Restaurant>()
            .HasIndex(restaurant => restaurant.OwnerId);

        modelBuilder.Entity<Restaurant>()
            .HasOne(restaurant => restaurant.Owner)
            .WithMany(user => user.Restaurants)
            .HasForeignKey(restaurant => restaurant.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RestaurantBranch>()
            .HasIndex(branch => branch.RestaurantId);

        modelBuilder.Entity<RestaurantBranch>()
            .HasOne(branch => branch.Restaurant)
            .WithMany(restaurant => restaurant.Branches)
            .HasForeignKey(branch => branch.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasKey(category => category.Id);

        modelBuilder.Entity<Category>()
            .HasIndex(category => category.RestaurantId);

        modelBuilder.Entity<Category>()
            .HasOne(category => category.Restaurant)
            .WithMany(restaurant => restaurant.Categories)
            .HasForeignKey(category => category.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasKey(product => product.Id);

        modelBuilder.Entity<Product>()
            .HasIndex(product => product.CategoryId);

        modelBuilder.Entity<Product>()
            .Property(product => product.BasePrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductVariant>()
            .HasKey(variant => variant.Id);

        modelBuilder.Entity<ProductVariant>()
            .HasIndex(variant => variant.ProductId);

        modelBuilder.Entity<ProductVariant>()
            .Property(variant => variant.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProductVariant>()
            .HasOne(variant => variant.Product)
            .WithMany(product => product.ProductVariants)
            .HasForeignKey(variant => variant.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Addon>().HasOne(addon => addon.Restaurant)
            .WithMany().HasForeignKey(addon => addon.RestaurantId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Addon>()
            .HasKey(addon => addon.Id);

        modelBuilder.Entity<Addon>()
            .Property(addon => addon.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProductAddon>()
            .HasKey(productAddon => new { productAddon.ProductId, productAddon.AddonId });

        modelBuilder.Entity<ProductAddon>()
            .HasIndex(productAddon => productAddon.AddonId);

        modelBuilder.Entity<ProductAddon>()
            .HasOne(productAddon => productAddon.Product)
            .WithMany(product => product.ProductAddons)
            .HasForeignKey(productAddon => productAddon.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductAddon>()
            .HasOne(productAddon => productAddon.Addon)
            .WithMany(addon => addon.ProductAddons)
            .HasForeignKey(productAddon => productAddon.AddonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cart>()
            .HasKey(cart => cart.Id);

        modelBuilder.Entity<Cart>()
            .HasIndex(cart => cart.UserId)
            .IsUnique();

        modelBuilder.Entity<Cart>()
            .HasOne(cart => cart.User)
            .WithOne(user => user.Cart)
            .HasForeignKey<Cart>(cart => cart.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartItemAddon>().HasKey(item => new { item.CartItemId, item.AddonId });
        modelBuilder.Entity<CartItemAddon>().HasOne(item => item.CartItem)
            .WithMany(item => item.Addons).HasForeignKey(item => item.CartItemId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CartItemAddon>().HasOne(item => item.Addon)
            .WithMany().HasForeignKey(item => item.AddonId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartItem>()
            .HasKey(item => item.Id);

        modelBuilder.Entity<CartItem>()
            .HasIndex(item => item.CartId);

        modelBuilder.Entity<CartItem>()
            .HasIndex(item => item.ProductId);

        modelBuilder.Entity<CartItem>()
            .HasIndex(item => item.ProductVariantId);

        modelBuilder.Entity<CartItem>()
            .Property(item => item.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CartItem>()
            .HasOne(item => item.Cart)
            .WithMany(cart => cart.Items)
            .HasForeignKey(item => item.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(item => item.Product)
            .WithMany(product => product.CartItems)
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartItem>()
            .HasOne(item => item.ProductVariant)
            .WithMany(variant => variant.CartItems)
            .HasForeignKey(item => item.ProductVariantId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasKey(order => order.Id);

        modelBuilder.Entity<Order>()
            .HasIndex(order => order.UserId);

        modelBuilder.Entity<Order>()
            .HasIndex(order => order.RestaurantId);

        modelBuilder.Entity<Order>()
            .HasIndex(order => order.BranchId);

        modelBuilder.Entity<Order>()
            .Property(order => order.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.Tax)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.DeliveryFee)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.Discount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.Status)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(order => order.DeliveryAddress)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Restaurant)
            .WithMany(restaurant => restaurant.Orders)
            .HasForeignKey(order => order.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Branch)
            .WithMany(branch => branch.Orders)
            .HasForeignKey(order => order.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasKey(item => item.Id);

        modelBuilder.Entity<OrderItem>()
            .HasIndex(item => item.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasIndex(item => item.ProductId);

        modelBuilder.Entity<OrderItem>()
            .HasIndex(item => item.ProductVariantId);

        modelBuilder.Entity<OrderItem>()
            .Property(item => item.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(item => item.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Product)
            .WithMany(product => product.OrderItems)
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.ProductVariant)
            .WithMany(variant => variant.OrderItems)
            .HasForeignKey(item => item.ProductVariantId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<OrderItemAddon>()
            .HasKey(itemAddon => itemAddon.Id);

        modelBuilder.Entity<OrderItemAddon>()
            .HasIndex(itemAddon => itemAddon.OrderItemId);

        modelBuilder.Entity<OrderItemAddon>()
            .HasIndex(itemAddon => itemAddon.AddonId);

        modelBuilder.Entity<OrderItemAddon>()
            .Property(itemAddon => itemAddon.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItemAddon>()
            .HasOne(itemAddon => itemAddon.OrderItem)
            .WithMany(item => item.Addons)
            .HasForeignKey(itemAddon => itemAddon.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItemAddon>()
            .HasOne(itemAddon => itemAddon.Addon)
            .WithMany(addon => addon.OrderItemAddons)
            .HasForeignKey(itemAddon => itemAddon.AddonId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}
