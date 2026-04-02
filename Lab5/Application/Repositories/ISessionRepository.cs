using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;

public interface ISessionRepository
{
    void Save(AccountSession accountSession);

    AccountSession? FindById(Guid sessionId);
}