using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class Test
{
    [Fact]
    public void WithdrawWithSufficientBalanceShouldDecreaseBalanceAndSave()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();

        var account = new Account("1234", new Money(100m, new Currency.Rub()));
        var withdrawAmount = new Money(30m, new Currency.Rub());

        IAccountRepository accountRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessionRepo = Substitute.For<ISessionRepository>();

        accountRepo.FindById(accountId).Returns(account);
        sessionRepo.FindById(sessionId)
            .Returns(new AccountSession(accountId, new SessionType.User()));

        var service = new AccountService(accountRepo, sessionRepo);

        ResultType result = service.Withdraw(sessionId, withdrawAmount);

        Assert.IsType<ResultType.Success>(result);
        Assert.Equal(70m, account.GetBalance().Amount);
        accountRepo.Received(1).Save(account);
    }

    [Fact]
    public void WithdrawWithInsufficientBalanceShouldReturnFailureAndNotSave()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();

        var account = new Account("1234", new Money(50m, new Currency.Rub()));
        var withdrawAmount = new Money(80m, new Currency.Rub());

        IAccountRepository accountRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessionRepo = Substitute.For<ISessionRepository>();

        accountRepo.FindById(accountId).Returns(account);
        sessionRepo.FindById(sessionId)
            .Returns(new AccountSession(accountId, new SessionType.User()));

        var service = new AccountService(accountRepo, sessionRepo);

        ResultType result = service.Withdraw(sessionId, withdrawAmount);

        Assert.IsType<ResultType.Failure>(result);
        accountRepo.DidNotReceive().Save(Arg.Any<Account>());
    }

    [Fact]
    public void DepositShouldIncreaseBalanceAndSave()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();

        var account = new Account("1234", new Money(50m, new Currency.Rub()));
        var depositAmount = new Money(25m, new Currency.Rub());

        IAccountRepository accountRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessionRepo = Substitute.For<ISessionRepository>();

        accountRepo.FindById(accountId).Returns(account);
        sessionRepo.FindById(sessionId)
            .Returns(new AccountSession(accountId, new SessionType.User()));

        var service = new AccountService(accountRepo, sessionRepo);

        ResultType result = service.Deposit(sessionId, depositAmount);

        Assert.IsType<ResultType.Success>(result);
        Assert.Equal(75m, account.GetBalance().Amount);
        accountRepo.Received(1).Save(account);
    }

    [Fact]
    public void LoginUserShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var account = new Account("1234", new Money(100, new Currency.Rub()));

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        accRepo.FindById(accountId).Returns(account);

        var service = new AuthenticationService(accRepo, sessRepo, "admin123");

        (ResultType result, Guid? sessionId) = service.LoginUser(accountId, "1234");

        Assert.IsType<ResultType.Success>(result);
        Assert.NotNull(sessionId);
    }

    [Fact]
    public void LoginUserShouldFailOnInvalidPin()
    {
        var accountId = Guid.NewGuid();
        var account = new Account("1234", new Money(100, new Currency.Rub()));

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        accRepo.FindById(accountId).Returns(account);

        var service = new AuthenticationService(accRepo, sessRepo, "admin123");

        (ResultType result, Guid? _) = service.LoginUser(accountId, "4321");

        Assert.IsType<ResultType.Failure>(result);
    }

    [Fact]
    public void LoginAdminShouldReturnSuccess()
    {
        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        var service = new AuthenticationService(accRepo, sessRepo, "pass");

        (ResultType result, Guid? sessionId) = service.LoginAdmin("pass");

        Assert.IsType<ResultType.Success>(result);
        Assert.NotNull(sessionId);
    }

    [Fact]
    public void LoginAdminShouldFailOnWrongPassword()
    {
        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        var service = new AuthenticationService(accRepo, sessRepo, "pass");

        (ResultType result, Guid? _) = service.LoginAdmin("wrong");

        Assert.IsType<ResultType.Failure>(result);
    }

    [Fact]
    public void CreateAccountShouldReturnId()
    {
        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        var service = new AccountService(accRepo, sessRepo);

        Guid id = service.CreateAccount("1234", new Money(50, new Currency.Rub()));

        Assert.NotEqual(Guid.Empty, id);
    }

    [Fact]
    public void DepositShouldIncreaseBalance()
    {
        var account = new Account("1234", new Money(10, new Currency.Rub()));
        var session = new AccountSession(account.Id, new SessionType.User());

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        accRepo.FindById(account.Id).Returns(account);
        sessRepo.FindById(session.Id).Returns(session);

        var svc = new AccountService(accRepo, sessRepo);

        svc.Deposit(session.Id, new Money(20, new Currency.Rub()));

        Assert.Equal(30, account.GetBalance().Amount);
    }

    [Fact]
    public void WithdrawShouldDecreaseBalance()
    {
        var account = new Account("1234", new Money(50, new Currency.Rub()));
        var session = new AccountSession(account.Id, new SessionType.User());

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        accRepo.FindById(account.Id).Returns(account);
        sessRepo.FindById(session.Id).Returns(session);

        var svc = new AccountService(accRepo, sessRepo);

        svc.Withdraw(session.Id, new Money(20, new Currency.Rub()));

        Assert.Equal(30, account.GetBalance().Amount);
    }

    [Fact]
    public void WithdrawShouldFailWhenInsufficient()
    {
        var account = new Account("1234", new Money(10, new Currency.Rub()));
        var session = new AccountSession(account.Id, new SessionType.User());

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        accRepo.FindById(account.Id).Returns(account);
        sessRepo.FindById(session.Id).Returns(session);

        var svc = new AccountService(accRepo, sessRepo);

        ResultType result = svc.Withdraw(session.Id, new Money(20, new Currency.Rub()));

        Assert.IsType<ResultType.Failure>(result);
    }

    [Fact]
    public void GetBalanceShouldFailInAdminSession()
    {
        var session = new AccountSession(null, new SessionType.Admin());

        IAccountRepository accRepo = Substitute.For<IAccountRepository>();
        ISessionRepository sessRepo = Substitute.For<ISessionRepository>();

        sessRepo.FindById(session.Id).Returns(session);

        var svc = new AccountService(accRepo, sessRepo);

        Money? bal = svc.GetBalance(session.Id);

        Assert.Null(bal);
    }
}
