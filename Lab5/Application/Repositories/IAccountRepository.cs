using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;

public interface IAccountRepository
{
    Account? FindById(Guid id);

    void Save(Account account);
}