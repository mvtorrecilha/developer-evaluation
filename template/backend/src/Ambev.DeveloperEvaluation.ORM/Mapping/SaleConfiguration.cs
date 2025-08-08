using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s =>s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.CustomerId)
               .IsRequired();

            builder.Property(c => c.CustomerName)
               .IsRequired();

            builder.Property(c => c.BranchId)
               .IsRequired();

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        }
    }
}
