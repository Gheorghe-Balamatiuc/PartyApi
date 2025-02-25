using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyApi.Models;

namespace PartyApi.FluentConfigs;

class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.Auth0UserId).IsRequired();
        builder.Property(u => u.Email).IsRequired();
    }
}