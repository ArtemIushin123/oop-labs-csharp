using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<Guid, Account> _storage = new();

    public Account? FindById(Guid id)
    {
        _storage.TryGetValue(id, out Account? acc);
        return acc;
    }

    public void Save(Account account)
    {
        _storage[account.Id] = account;
    }
}