using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class MemberRepository(
    PartyContext context,
    ILogger logger
) : Repository<Member>(
        context,
        logger
    ), 
    IMemberRepository
{
    public async Task<Member?> GetByIdWithPartyAsync(int id)
    {
        try {
            return await _dbSet.Include(m => m.Party).FirstOrDefaultAsync(m => m.MemberId == id);
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error getting member with id {id}", id);
            return null;
        }
    }
}