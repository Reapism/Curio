﻿using Curio.Core.Entities;
using Curio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Curio.Web
{
    public static class SeedData
    {
        public static readonly ToDoItem ToDoItem1 = new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = "Get Sample Working",
            Description = "Try to get the sample to build."
        };
        public static readonly ToDoItem ToDoItem2 = new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = "Review Solution",
            Description = "Review the different projects in the solution and how they relate to one another."
        };
        public static readonly ToDoItem ToDoItem3 = new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = "Run and Review Tests",
            Description = "Make sure all the tests run and review what they are doing."
        };

        /// <summary>
        /// Seeds the database with default values if any values exist.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Do not need dispatcher parameter this early on - marked null intentionally.
            using (var dbContext = new CurioClientDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CurioClientDbContext>>(), null))
            {
                if (dbContext.ToDoItems.Any())
                {
                    return;
                }

                PopulateTestData(dbContext);
            }
        }
        public static void PopulateTestData(CurioClientDbContext dbContext)
        {
            foreach (var item in dbContext.ToDoItems)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(ToDoItem1);
            dbContext.ToDoItems.Add(ToDoItem2);
            dbContext.ToDoItems.Add(ToDoItem3);

            dbContext.SaveChanges();
        }
    }
}