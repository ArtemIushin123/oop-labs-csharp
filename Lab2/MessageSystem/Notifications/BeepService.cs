using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Notifications;

public class BeepService : IBeepService { public void Beep() => Console.Beep(); }