using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using InstaShare.Authorization.Roles;
using InstaShare.Authorization.Users;
using InstaShare.Models;
using InstaShare.MultiTenancy;

namespace InstaShare.EntityFrameworkCore
{
    public class InstaShareDbContext : AbpZeroDbContext<Tenant, Role, User, InstaShareDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<UserFile> UserFiles { get; set; }

        public InstaShareDbContext(DbContextOptions<InstaShareDbContext> options)
            : base(options)
        {
        }
    }
}
