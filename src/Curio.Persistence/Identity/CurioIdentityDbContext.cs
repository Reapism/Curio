using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curio.Persistence.Identity
{
    public class CurioIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public CurioIdentityDbContext(DbContextOptions<CurioIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(CurioIdentityDbContext).Assembly);
        }
    }
}
