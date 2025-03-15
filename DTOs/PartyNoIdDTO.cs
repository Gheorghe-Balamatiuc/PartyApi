namespace PartyApi.DTOs;

public class PartyNoIdDTO
{
    public required string PartyName { get; set; }

    public required decimal Budget { get; set; }

    public int? UserId { get; set; }
}