using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObject;

public class Money
{
    public decimal Amount { get; }

    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("The amount cannot be less than zero");
        }

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        CheckCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        CheckCurrency(other);
        return Amount < other.Amount
            ? throw new ArgumentException("There are insufficient funds in the account")
            : new Money(Amount - other.Amount, Currency);
    }

    private void CheckCurrency(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new Exception("Not the same currency");
        }
    }

    public static Money operator +(Money left, Money right) => left.Add(right);

    public static Money operator -(Money left, Money right) => left.Subtract(right);
}
