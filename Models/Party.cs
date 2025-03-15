using Microsoft.EntityFrameworkCore;
using PartyApi.FluentConfigs;

namespace PartyApi.Models;

[EntityTypeConfiguration(typeof(PartyConfig))]
public class Party 
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public required decimal Budget { get; set; }

    public int? UserId { get; set; }

    public ICollection<User> Members { get; } = [];
}