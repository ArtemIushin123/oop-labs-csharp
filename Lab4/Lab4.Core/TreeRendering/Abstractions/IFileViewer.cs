using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;

public interface IFileViewer
{
    string ViewFile(IFile file, FileViewMode mode);
}