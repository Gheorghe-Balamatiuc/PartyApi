using PartyApi.Models;

namespace PartyApi.Repository.IRepository;

public interface IPartyRepository : IRepository<Party>
{
    // Add methods specific to the PartyRepository
    Task<Party?> GetPartyWithMembersAsync(int id);
}