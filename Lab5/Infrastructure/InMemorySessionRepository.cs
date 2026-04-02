using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly Dictionary<Guid, AccountSession> _sessions = new();

    public AccountSession? FindById(Guid sessionId)
    {
        _sessions.TryGetValue(sessionId, out AccountSession? session);
        return session;
    }

    public void Save(AccountSession accountSession)
    {
        _sessions[accountSession.Id] = accountSession;
    }
}