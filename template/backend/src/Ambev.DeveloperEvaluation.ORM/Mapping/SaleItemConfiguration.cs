﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.ProductId)
                .IsRequired();

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.Cancelled)
                .IsRequired();

            builder.Ignore(si => si.TotalAmount);
         
        }
    }
}
