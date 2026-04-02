using FluentAssertions;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Archivers;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Topics;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class Test
{
    [Fact]
    public void WhenUserReceivesMessage_MessageStatusShouldBeUnread()
    {
        var user = new User("Bob");
        var message = new Message("Header", "Body", new ImportanceLevel.Low());

        user.Receive(message);

        user.GetMessages().Should().ContainSingle();
        user.GetMessages().First().Status.Should().BeOfType<MessageStatus.Unread>();
    }

    [Fact]
    public void WhenUserMarksMessageAsRead_StatusShouldChangeToRead()
    {
        var user = new User("Alice");
        var message = new Message("Header", "Body", new ImportanceLevel.Low());
        user.Receive(message);
        UserMessage userMessage = user.GetMessages().First();

        ResultType result = userMessage.MarkAsRead();

        result.Should().BeOfType<ResultType.Success>();
        userMessage.Status.Should().BeOfType<MessageStatus.Read>();
    }

    [Fact]
    public void WhenUserMarksAlreadyReadMessage_ShouldReturnError()
    {
        var user = new User("Alice");
        var message = new Message("Header", "Body", new ImportanceLevel.Low());
        user.Receive(message);
        UserMessage userMessage = user.GetMessages().First();
        userMessage.MarkAsRead();

        ResultType result = userMessage.MarkAsRead();

        result.Should().BeOfType<ResultType.TheMessageHasAlreadyBeenRead>();
    }

    [Fact]
    public void LowImportanceMessage_ShouldNotReachHighFilteredRecipient()
    {
        var innerRecipient = new Mock<IRecipient>();
        var filteringRecipient = new FilteringRecipient(innerRecipient.Object, new ImportanceLevel.High());
        var message = new Message("Header", "Body", new ImportanceLevel.Low());

        filteringRecipient.Receive(message);

        innerRecipient.Verify(r => r.Receive(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public void LoggingRecipient_ShouldCallLoggerOnMessageReceived()
    {
        var innerRecipient = new Mock<IRecipient>();
        var logger = new Mock<ILogger>();
        var loggingRecipient = new LoggingRecipient(innerRecipient.Object, logger.Object);
        var message = new Message("Header", "Body", new ImportanceLevel.Low());

        loggingRecipient.Receive(message);

        logger.Verify(l => l.Log(It.Is<string>(s => s.Contains("Header"))), Times.Once);
        innerRecipient.Verify(r => r.Receive(It.IsAny<Message>()), Times.Once);
    }

    [Fact]
    public void FormattingArchiver_ShouldCallFormatterMethods()
    {
        var formatterMock = new Mock<IFormatter>();
        var archiver = new FormattingArchiver(formatterMock.Object);
        var message = new Message("Header", "Body", new ImportanceLevel.High());

        archiver.Archive(message);

        formatterMock.Verify(f => f.WriteHeader(It.Is<string>(h => h == "Header")), Times.Once);
        formatterMock.Verify(f => f.WriteBody(It.Is<string>(b => b == "Body")), Times.Once);
    }

    [Fact]
    public void OnlyRecipientWithoutFilter_ShouldReceiveLowImportanceMessage()
    {
        var user1 = new Mock<IRecipient>();
        var user2 = new Mock<IRecipient>();
        var filteredUser = new FilteringRecipient(user2.Object, new ImportanceLevel.High());
        var group = new RecipientGroup(new List<IRecipient> { user1.Object, filteredUser });
        var topic = new Topic("News");

        topic.AddRecipient(group);

        var message = new Message("Header", "Body", new ImportanceLevel.Low());
        topic.Send(message);

        user1.Verify(r => r.Receive(It.IsAny<Message>()), Times.Once);
        user2.Verify(r => r.Receive(It.IsAny<Message>()), Times.Never);
    }
}
