using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IAccountService
{
    Guid CreateAccount(string password, Money initialBalance);

    IReadOnlyList<Operation>? GetOperations(Guid sessionId);

    ResultType Withdraw(Guid sessionId, Money amount);

    ResultType Deposit(Guid sessionId, Money amount);

    Money? GetBalance(Guid sessionId);
}