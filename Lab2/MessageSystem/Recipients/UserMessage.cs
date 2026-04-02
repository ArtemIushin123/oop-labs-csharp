using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class UserMessage
{
    public Message Message { get; }

    public MessageStatus Status { get; private set; }

    public UserMessage(Message message)
    {
        Message = message;
        Status = new MessageStatus.Unread();
    }

    public ResultType MarkAsRead()
    {
        if (Status is MessageStatus.Read)
            return new ResultType.TheMessageHasAlreadyBeenRead();

        Status = new MessageStatus.Read();
        return new ResultType.Success();
    }
}