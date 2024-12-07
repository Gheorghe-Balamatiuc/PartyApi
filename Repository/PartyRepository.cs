using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class PartyRepository(PartyContext context) : Repository<Party>(context), IPartyRepository
{
    public async Task<Party?> GetPartyWithMembersAsync(int id)
    {
        return await _dbSet.Include(p => p.Members).FirstOrDefaultAsync(p => p.PartyId == id);
    }
}