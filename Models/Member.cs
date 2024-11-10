using Microsoft.EntityFrameworkCore;
using PartyApi.FluentConfigs;

namespace PartyApi.Models;

[EntityTypeConfiguration(typeof(MemberConfig))]
public class Member
{
    public int MemberId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int PartyId { get; set; }

    public Party Party { get; set; } = null!;
}