using Goal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goal.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(e => e.Brand)
                .WithMany(c => c.Product)
                .HasForeignKey(e => e.BrandId)
                .HasPrincipalKey(e => e.Id);
            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id);

            builder
                .HasOne(e => e.Stock)
                .WithOne(c => c.Product)
                .HasForeignKey<Product>(e => e.StockId);
            builder
                .HasMany(e => e.Items)
                .WithOne(c => c.Product)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id);

            builder
                .HasMany(e => e.Images)
                .WithOne(e => e.Product)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.ProductId);
            builder
                .HasOne(e => e.Discount)
                .WithMany(e => e.Products)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.DiscountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(e => e.Id);
            builder.HasIndex(e => new { e.CategoryId, e.BrandId });
            builder.HasIndex(e => e.BrandId);
            builder.HasIndex(e => e.CategoryId);
            builder.Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");


        }
    }
}
