using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyApi.Models;

namespace PartyApi.FluentConfigs;

class PartyConfig : IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.HasKey(p => p.PartyId);
        builder.Property(p => p.PartyName).IsRequired();
        builder.Property(p => p.Budget).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.UserId).IsRequired(false);

        builder.HasMany(p => p.Members)
            .WithMany(u => u.MemberParties);
    }
}