namespace PartyApi.DTOs;

public class PartyWithMembersDTO
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public decimal Budget { get; set; }

    public IEnumerable<MemberDTO> Members { get; set; } = [];
}