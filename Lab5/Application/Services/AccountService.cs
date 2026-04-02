using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    private readonly ISessionRepository _sessionRepository;

    public AccountService(IAccountRepository accountRepository, ISessionRepository sessionRepository)
    {
        _accountRepository = accountRepository;
        _sessionRepository = sessionRepository;
    }

    public IReadOnlyList<Operation>? GetOperations(Guid sessionId)
    {
        AccountSession? session = _sessionRepository.FindById(sessionId);
        if (session is null || !session.IsActive || session.AccountId is null)
            return null;

        Account? account = _accountRepository.FindById(session.AccountId.Value);
        return account?.GetOperations();
    }

    public Guid CreateAccount(string password, Money initialBalance)
    {
        var account = new Account(password, initialBalance);
        _accountRepository.Save(account);
        return account.Id;
    }

    public ResultType Withdraw(Guid sessionId, Money amount)
    {
        AccountSession? session = _sessionRepository.FindById(sessionId);
        if (session is null || !session.IsActive)
            return new ResultType.Failure("AccountSession not found or inactive");
        if (session.SessionType is SessionType.Admin)
            return new ResultType.Failure("Admin session cannot perform withdraw");
        if (session.AccountId is null)
            return new ResultType.Failure("User session has no account assigned");
        Guid accountId = session.AccountId.Value;
        Account? account = _accountRepository.FindById(accountId);
        if (account is null)
            return new ResultType.Failure("Account not found");
        try
        {
            account.Withdraw(amount);
        }
        catch (Exception e)
        {
            return new ResultType.Failure(e.Message);
        }

        _accountRepository.Save(account);

        return new ResultType.Success();
    }

    public ResultType Deposit(Guid sessionId, Money amount)
    {
        AccountSession? session = _sessionRepository.FindById(sessionId);
        if (session is null || !session.IsActive)
            return new ResultType.Failure("AccountSession not found or inactive");
        if (session.SessionType is SessionType.Admin)
            return new ResultType.Failure("Admin session cannot perform withdraw");
        if (session.AccountId is null)
            return new ResultType.Failure("User session has no account assigned");
        Guid accountId = session.AccountId.Value;
        Account? account = _accountRepository.FindById(accountId);
        if (account is null)
            return new ResultType.Failure("Account not found");
        try
        {
            account.Deposit(amount);
        }
        catch (Exception e)
        {
            return new ResultType.Failure(e.Message);
        }

        _accountRepository.Save(account);

        return new ResultType.Success();
    }

    public Money? GetBalance(Guid sessionId)
    {
        AccountSession? session = _sessionRepository.FindById(sessionId);
        if (session is null || !session.IsActive)
            return null;
        if (session.SessionType is SessionType.Admin)
            return null;
        if (session.AccountId is null)
            return null;
        Guid accountId = session.AccountId.Value;
        Account? account = _accountRepository.FindById(accountId);
        return account?.GetBalance();
    }
}