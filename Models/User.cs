using Microsoft.EntityFrameworkCore;
using PartyApi.FluentConfigs;

namespace PartyApi.Models;

[EntityTypeConfiguration(typeof(UserConfig))]
public class User {
    public int UserId { get; set; }

    public required string Auth0UserId { get; set; }

    public required string Email { get; set; }

    public ICollection<Party> CreatedParties { get; } = [];

    public ICollection<Party> MemberParties { get; } = [];
}