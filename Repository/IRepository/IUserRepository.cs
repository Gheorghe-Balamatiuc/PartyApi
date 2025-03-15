using PartyApi.Models;

namespace PartyApi.Repository.IRepository;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithPartiesAsync(int id);

    Task<bool> AddPartyToUserAsync(int userId, int partyId);

    Task<bool> RemovePartyFromUserAsync(int userId, int partyId);
}