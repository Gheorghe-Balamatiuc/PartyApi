using PartyApi.Data;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

class PartyRepository(PartyContext context) : Repository<Party>(context), IPartyRepository
{
}