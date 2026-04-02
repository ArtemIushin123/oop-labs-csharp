namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

public interface IFormatter
{
    void WriteHeader(string header);

    void WriteBody(string body);
}