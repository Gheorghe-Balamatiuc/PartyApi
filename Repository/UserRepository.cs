using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class UserRepository (
    PartyContext context,
    ILogger logger
) : Repository<User>(
        context,
        logger
    ), 
    IUserRepository
{
    public async Task<User?> GetUserWithPartiesAsync(int id)
    {
        try {
            return await _dbSet.Include(u => u.MemberParties).Include(u => u.CreatedParties).FirstOrDefaultAsync(u => u.UserId == id);
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error getting user with id {id}", id);
            return null;
        }
    }

    public async Task<bool> AddPartyToUserAsync(int userId, int partyId)
    {
        try {
            var user = await _dbSet.Include(u => u.MemberParties).FirstOrDefaultAsync(u => u.UserId == userId);
            var party = await _context.Parties.FindAsync(partyId);

            if (user == null || party == null)
            {
                _logger.LogWarning("User with id {userId} or party with id {partyId} not found", userId, partyId);
                return false;
            }

            if (user.MemberParties.Contains(party))
            {
                _logger.LogWarning("User with id {userId} already has party with id {partyId}", userId, partyId);
                return false;
            }

            user.MemberParties.Add(party);
            await SaveAsync();
            return true;
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error adding party with id {partyId} to user with id {userId}", partyId, userId);
            return false;
        }
    }

    public async Task<bool> RemovePartyFromUserAsync(int userId, int partyId)
    {
        try {
            var user = await _dbSet.Include(u => u.MemberParties).FirstOrDefaultAsync(u => u.UserId == userId);
            var party = await _context.Parties.FindAsync(partyId);

            if (user == null || party == null)
            {
                _logger.LogWarning("User with id {userId} or party with id {partyId} not found", userId, partyId);
                return false;
            }

            if (!user.MemberParties.Contains(party))
            {
                _logger.LogWarning("User with id {userId} does not have party with id {partyId}", userId, partyId);
                return false;
            }

            user.MemberParties.Remove(party);
            await SaveAsync();
            return true;
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error removing party with id {partyId} from user with id {userId}", partyId, userId);
            return false;
        }
    }
}