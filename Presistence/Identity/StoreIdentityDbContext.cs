using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Identity
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
                : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>().ToTable("Addresses");
            base.OnModelCreating(builder);

        }
    }
}
