using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

public class AccountSession
{
    public Guid Id { get; }

    public Guid? AccountId { get; }

    public SessionType SessionType { get; }

    public DateTime CreatedAt { get; }

    public bool IsActive { get; set; }

    public AccountSession(Guid? accountId, SessionType sessionType)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        SessionType = sessionType;
        CreatedAt = DateTime.Now;
        IsActive = true;
    }

    public void CloseSession()
    {
        IsActive = false;
    }
}