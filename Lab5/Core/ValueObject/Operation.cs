using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

public class Operation
{
    public Guid Id { get; }

    public Guid AccountId { get; }

    public DateTime Date { get; }

    public Money Money { get; }

    public TypeOfOperations TypeOfOperation { get; }

    public Operation(Guid accountId, Money money, TypeOfOperations typeOfOperation)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Money = money;
        Date = DateTime.Now;
        TypeOfOperation = typeOfOperation;
    }
}