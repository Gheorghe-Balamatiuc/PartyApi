using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class PartyRepository(
    PartyContext context,
    ILogger logger
    ) : Repository<Party>(
            context,
            logger
        ), 
        IPartyRepository
{
    public async Task<Party?> GetPartyWithMembersAsync(int id)
    {
        try {
            return await _dbSet.Include(p => p.Members).FirstOrDefaultAsync(p => p.PartyId == id);
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error getting party with id {id}", id);
            return null;
        }
    }
}