using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;

public interface ITreeRenderer
{
    string RenderTree(IFolder root, TreeRenderOptions options);
}