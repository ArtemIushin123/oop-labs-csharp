using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly string _systemPassword;

    public AuthenticationService(
        IAccountRepository accountRepository,
        ISessionRepository sessionRepository,
        string systemPassword)
    {
        _accountRepository = accountRepository;
        _sessionRepository = sessionRepository;
        _systemPassword = systemPassword;
    }

    public (ResultType Result, Guid? SessionId) LoginUser(Guid id, string pin)
    {
        Account? account = _accountRepository.FindById(id);
        if (account is null)
            return (new ResultType.Failure("Account not found"), null);
        if (!account.ValidatePassword(pin))
            return (new ResultType.Failure("Invalid PIN"), null);
        var session = new AccountSession(id, new SessionType.User());
        _sessionRepository.Save(session);
        return (new ResultType.Success(), session.Id);
    }

    public (ResultType Result, Guid? SessionId) LoginAdmin(string systemPassword)
    {
        if (systemPassword != _systemPassword)
            return (new ResultType.Failure("Invalid system password"), null);
        var session = new AccountSession(null, new SessionType.Admin());
        _sessionRepository.Save(session);
        return (new ResultType.Success(), session.Id);
    }
}