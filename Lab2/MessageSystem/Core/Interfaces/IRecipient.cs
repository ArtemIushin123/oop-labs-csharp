namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

public interface IRecipient
{
    void Receive(Message message);
}