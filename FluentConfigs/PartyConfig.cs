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
        builder.Property(p => p.Budget).HasColumnType("decimal(18,2)");
        builder.HasData(
            new Party
            {
                PartyId = 1,
                PartyName = "Birthday Party",
                Budget = 1000
            },
            new Party
            {
                PartyId = 2,
                PartyName = "Wedding Party",
                Budget = 5000
            }
        );
    }
}