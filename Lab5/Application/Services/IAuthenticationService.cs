using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IAuthenticationService
{
    (ResultType Result, Guid? SessionId) LoginUser(Guid id, string pin);

    (ResultType Result, Guid? SessionId) LoginAdmin(string systemPassword);
}