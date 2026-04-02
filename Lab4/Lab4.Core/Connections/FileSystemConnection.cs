using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

public class FileSystemConnection
{
    public string ConnectionRoot { get; }

    public string CurrentLocalPath { get; private set; }

    public IFileSystem FileSystem { get; }

    public ConnectionState State { get; set; }

    public FileSystemConnection(IFileSystem fileSystem, string connectionRootSystemPath)
    {
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        ConnectionRoot = Path.GetFullPath(connectionRootSystemPath ?? throw new ArgumentNullException(nameof(connectionRootSystemPath)));
        CurrentLocalPath = "/";
        State = new ConnectionState.Connected();
    }

    public void Disconnect()
    {
        State = new ConnectionState.Disconnected();
    }

    public void ChangeDirectory(string newLocalPath)
    {
        if (State is ConnectionState.Disconnected)
            throw new InvalidOperationException("Connection is not active.");

        CurrentLocalPath = newLocalPath;
    }
}