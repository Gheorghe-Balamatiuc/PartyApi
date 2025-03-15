namespace PartyApi.DTOs;

public class UserDTO
{
    public int UserId { get; set; }

    public required string Auth0UserId { get; set; }

    public required string Email { get; set; }
}