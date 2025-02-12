using PartyApi.Data;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

public class UnitOfWork(
    PartyContext context,
    ILogger<UnitOfWork> logger
    ) : IUnitOfWork, IDisposable
{
    private readonly PartyContext _context = context;
    private readonly ILogger<UnitOfWork> _logger = logger;

    private IPartyRepository? partyRepository;
    private IMemberRepository? memberRepository;

    public IPartyRepository PartyRepository
    {
        get
        {
            partyRepository ??= new PartyRepository(
                _context,
                _logger
            );
            return partyRepository;
        }
    }

    public IMemberRepository MemberRepository
    {
        get
        {
            memberRepository ??= new MemberRepository(
                _context,
                _logger
            );
            return memberRepository;
        }
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}