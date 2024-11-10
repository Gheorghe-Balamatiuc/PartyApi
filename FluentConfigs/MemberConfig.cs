using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyApi.Models;

namespace PartyApi.FluentConfigs;

class MemberConfig : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.MemberId);
        builder.Property(m => m.FirstName).IsRequired();
        builder.Property(m => m.LastName).IsRequired();

        builder.HasOne(e => e.Party)
            .WithMany(e => e.Members)
            .HasForeignKey(e => e.PartyId)
            .IsRequired();

        builder.HasData(
            new Member
            {
                MemberId = 1,
                FirstName = "John",
                LastName = "Doe",
                PartyId = 1
            },
            new Member
            {
                MemberId = 2,
                FirstName = "Jane",
                LastName = "Doe",
                PartyId = 1
            },
            new Member
            {
                MemberId = 3,
                FirstName = "Alice",
                LastName = "Smith",
                PartyId = 2
            },
            new Member
            {
                MemberId = 4,
                FirstName = "Bob",
                LastName = "Smith",
                PartyId = 2
            }
        );
    }
}