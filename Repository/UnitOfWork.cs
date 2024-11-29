using PartyApi.Data;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly PartyContext _context;

    private IPartyRepository? partyRepository;
    private IMemberRepository? memberRepository;

    public IPartyRepository PartyRepository
    {
        get
        {
            this.partyRepository ??= new PartyRepository(_context);
            return this.partyRepository;
        }
    }

    public IMemberRepository MemberRepository
    {
        get
        {
            this.memberRepository ??= new MemberRepository(_context);
            return this.memberRepository;
        }
    }

    public UnitOfWork(PartyContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}