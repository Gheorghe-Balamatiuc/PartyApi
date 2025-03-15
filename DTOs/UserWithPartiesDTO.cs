namespace PartyApi.DTOs;

public class UserWithPartiesDTO 
{
    public int UserId { get; set; }

    public required string Auth0UserId { get; set; }

    public required string Email { get; set; }

    public ICollection<PartyDTO> CreatedParties { get; } = [];

    public ICollection<PartyDTO> MemberParties { get; } = [];
}