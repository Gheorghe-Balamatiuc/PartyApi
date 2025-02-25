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
}