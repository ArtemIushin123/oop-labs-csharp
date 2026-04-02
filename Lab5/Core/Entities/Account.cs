using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

public class Account
{
    public Guid Id { get; }

    private string Password { get; }

    private Money Balance { get; set; }

    private List<Operation> Operations { get; }

    public Account(string password, Money balance)
    {
        Id = Guid.NewGuid();
        Password = password;
        Balance = balance;
        Operations = new List<Operation>();
    }

    public void AddOperation(Operation operation)
    {
        Operations.Add(operation);
    }

    public void Deposit(Money amount)
    {
        Balance = Balance.Add(amount);
        AddOperation(new Operation(Id, new Money(amount.Amount, amount.Currency), new TypeOfOperations.Deposit()));
    }

    public void Withdraw(Money amount)
    {
        Balance = Balance.Subtract(amount);
        AddOperation(new Operation(Id, new Money(amount.Amount, amount.Currency), new TypeOfOperations.Withdraw()));
    }

    public void Transfer(Account to, Money amount)
    {
        Withdraw(amount);
        to.Deposit(amount);
        AddOperation(new Operation(Id, new Money(amount.Amount, amount.Currency), new TypeOfOperations.TransferOut()));
        to.AddOperation(new Operation(to.Id, new Money(amount.Amount, amount.Currency), new TypeOfOperations.TransferIn()));
    }

    public IReadOnlyList<Operation> GetOperations()
    {
        return Operations.AsReadOnly();
    }

    public bool ValidatePassword(string password)
    {
        return Password == password;
    }

    public Money GetBalance()
    {
        return new Money(Balance.Amount, Balance.Currency);
    }
}