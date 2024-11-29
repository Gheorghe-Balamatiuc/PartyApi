using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class MemberRepository : Repository<Member>, IMemberRepository
{
    public MemberRepository(PartyContext context) : base(context)
    {
    }
}