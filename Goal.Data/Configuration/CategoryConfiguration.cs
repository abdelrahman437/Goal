using Goal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goal.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(n => n.Name).IsRequired();
            builder.Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");


        }
    }
}
