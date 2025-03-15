namespace PartyApi.DTOs;

public class PartyDTO
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public required decimal Budget { get; set; }

    public int? UserId { get; set; }
}