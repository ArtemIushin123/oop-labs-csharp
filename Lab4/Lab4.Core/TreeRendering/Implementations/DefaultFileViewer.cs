using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Implementations;

public class DefaultFileViewer : IFileViewer
{
    public string ViewFile(IFile file, FileViewMode mode)
    {
        return mode switch
        {
            FileViewMode.TextMode => $"[TextMode File] {file.Name}",
            FileViewMode.InfoMode => $"[File InfoMode] Name: {file.Name}",
            _ => file.Name,
        };
    }
}