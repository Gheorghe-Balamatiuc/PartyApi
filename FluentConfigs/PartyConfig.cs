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
        builder.Property(p => p.Budget).HasColumnType("decimal(17,2)");
    }
}