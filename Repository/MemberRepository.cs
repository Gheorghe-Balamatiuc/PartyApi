using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class MemberRepository(PartyContext context) : Repository<Member>(context), IMemberRepository
{
    public async Task<Member?> GetByIdWithPartyAsync(int id)
    {
        return await _dbSet.Include(m => m.Party).FirstOrDefaultAsync(m => m.MemberId == id);
    }
}