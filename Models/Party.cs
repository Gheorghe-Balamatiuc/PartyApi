using Microsoft.EntityFrameworkCore;
using PartyApi.FluentConfigs;

namespace PartyApi.Models;

[EntityTypeConfiguration(typeof(PartyConfig))]
public class Party 
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public decimal Budget { get; set; }

    public IEnumerable<Member> Members { get; } = new List<Member>();
}