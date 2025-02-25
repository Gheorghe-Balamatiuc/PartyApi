namespace PartyApi.DTOs;

public class UserNoIdDTO
{
    public required string Auth0UserId { get; set; }

    public required string Email { get; set; }
}