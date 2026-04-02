using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Archivers;

public class FormattingArchiver : IArchiver
{
    private readonly IFormatter _formatter;

    public FormattingArchiver(IFormatter formatter)
    {
        _formatter = formatter;
    }

    public void Archive(Message message)
    {
        _formatter.WriteHeader(message.Title);
        _formatter.WriteBody(message.Body);
    }
}