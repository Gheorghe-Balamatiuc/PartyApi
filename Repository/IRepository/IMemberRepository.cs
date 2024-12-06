using PartyApi.Models;

namespace PartyApi.Repository.IRepository;

public interface IMemberRepository : IRepository<Member>
{
    // Add methods specific to the MemberRepository
    Task<Member?> GetByIdWithPartyAsync(int id);
}