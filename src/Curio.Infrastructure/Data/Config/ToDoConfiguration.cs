﻿using Curio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curio.Infrastructure.Data.Config
{
    public class ToDoConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired();
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(e => e.LoginType)
                .HasConversion<string>();
        }
    }
}