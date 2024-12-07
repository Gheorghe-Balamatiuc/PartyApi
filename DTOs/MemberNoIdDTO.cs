namespace PartyApi.DTOs;

public class MemberNoIdDTO
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int PartyId { get; set; }
}