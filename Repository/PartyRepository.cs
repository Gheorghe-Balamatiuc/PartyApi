using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class PartyRepository : Repository<Party>, IPartyRepository
{
    public PartyRepository(PartyContext context) : base(context)
    {
    }
}