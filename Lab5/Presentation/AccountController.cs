using Itmo.ObjectOrientedProgramming.Lab5.Application.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ISessionRepository _sessionRepository;

    public AccountController(IAccountService accountService, ISessionRepository sessionRepository)
    {
        _accountService = accountService;
        _sessionRepository = sessionRepository;
    }

    [HttpGet("balance")]
    public IActionResult GetBalance([FromHeader(Name = "X-AccountSession-Id")] Guid? sessionId)
    {
        if (sessionId is null) return Unauthorized("AccountSession header missing");

        Money? balance = _accountService.GetBalance(sessionId.Value);
        return balance is null
            ? Unauthorized("Cannot access balance (invalid/ admin session?)")
            : Ok(new { balance.Amount, Currency = balance.Currency.GetType().Name });
    }

    [HttpGet("operations")]
    public IActionResult GetOperations([FromHeader(Name = "X-AccountSession-Id")] Guid? sessionId)
    {
        if (sessionId is null) return Unauthorized("AccountSession header missing");

        IReadOnlyList<Operation>? ops = _accountService.GetOperations(sessionId.Value);
        if (ops is null) return Unauthorized("Cannot access operations (invalid session?)");

        var res = ops.Select(o => new
        {
            o.Id,
            o.Date,
            o.Money.Amount,
            Currency = o.Money.Currency.GetType().Name,
            Operation = o.TypeOfOperation.GetType().Name,
        });

        return Ok(res);
    }

    [HttpPost("deposit")]
    public IActionResult Deposit([FromHeader(Name = "X-AccountSession-Id")] Guid? sessionId, [FromBody] TransactionRequest request)
    {
        if (sessionId is null) return Unauthorized("AccountSession header missing");

        ResultType result = _accountService.Deposit(sessionId.Value, new Money(request.Amount, new Currency.Rub()));
        return result is ResultType.Failure f ? MapFailureToAction(f) : Ok();
    }

    [HttpPost("create")]
    public IActionResult CreateAccount([FromHeader(Name = "X-AccountSession-Id")] Guid? sessionId, [FromBody] CreateAccountRequest request)
    {
        if (sessionId is null) return Unauthorized("AccountSession header missing");

        AccountSession? session = _sessionRepository.FindById(sessionId.Value);
        if (session is null || !session.IsActive) return Unauthorized("Invalid session");
        if (session.SessionType is not SessionType.Admin) return Unauthorized("Admin session required");

        Guid id = _accountService.CreateAccount(request.Password, new Money(request.InitialBalance, new Currency.Rub()));
        return CreatedAtAction(nameof(GetBalance), new { sessionId = sessionId.Value }, id);
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromHeader(Name = "X-AccountSession-Id")] Guid? sessionId, [FromBody] TransactionRequest request)
    {
        if (sessionId is null) return Unauthorized("AccountSession header missing");

        ResultType result = _accountService.Withdraw(sessionId.Value, new Money(request.Amount, new Currency.Rub()));
        return result is ResultType.Failure f ? MapFailureToAction(f) : Ok();
    }

    private IActionResult MapFailureToAction(ResultType.Failure failure)
    {
        string lower = failure.Str.ToLowerInvariant();
        return lower.Contains("session", StringComparison.Ordinal) ||
            lower.Contains("admin", StringComparison.Ordinal) ||
            lower.Contains("unauthorized", StringComparison.Ordinal) ||
            lower.Contains("not assigned", StringComparison.Ordinal)
            ? Unauthorized(failure.Str)
            : BadRequest(failure.Str);
    }
}
