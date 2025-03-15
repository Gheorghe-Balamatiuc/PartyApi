namespace PartyApi.DTOs;

public class PartyWithMembersDTO
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public required decimal Budget { get; set; }

    public int? UserId { get; set; }

    public ICollection<UserDTO> Members { get; } = [];
}