using Clay.AccessControl.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Clay.AccessControl.Api.Data;
public interface IAccessControlDbContext
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Lock> Locks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Audit> Audits { get; set; }
}