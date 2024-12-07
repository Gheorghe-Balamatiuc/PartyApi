namespace PartyApi.DTOs;

public class MemberDTO
{
    public int MemberId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int PartyId { get; set; }
}