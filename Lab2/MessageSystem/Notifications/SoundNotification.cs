using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Notifications;

public class SoundNotification : INotification
{
    private readonly IBeepService _beeper;

    public SoundNotification(IBeepService beeper) { _beeper = beeper; }

    public void Notify() => _beeper.Beep();
}