using Microsoft.EntityFrameworkCore;
using PartyApi.Models;

namespace PartyApi.Data;

public class PartyContext : DbContext
{
    public PartyContext()
    {
    }

    public PartyContext(DbContextOptions<PartyContext> options)
        : base(options)
    {
    }

    public DbSet<Party> Parties { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<User> Users { get; set; }
}