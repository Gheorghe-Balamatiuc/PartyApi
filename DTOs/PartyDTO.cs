namespace PartyApi.DTOs;

public class PartyDTO
{
    public int PartyId { get; set; }

    public required string PartyName { get; set; }

    public decimal Budget { get; set; }
}