using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Implementations;

public class DefaultTreeRenderer : ITreeRenderer
{
    public string RenderTree(IFolder root, TreeRenderOptions options)
    {
        return RenderRecursive(root, 0, options);
    }

    private string RenderRecursive(IFolder folder, int depth, TreeRenderOptions options)
    {
        if (depth >= options.MaxDepth)
            return string.Empty;

        var builder = new StringBuilder();

        foreach (IEntity child in folder.Children)
        {
            string indent = new string(' ', depth * options.IndentUnit.Length);

            if (child is IFolder f)
            {
                builder.AppendLine($"{indent}{options.FolderSymbol} {f.Name}");
                builder.Append(RenderRecursive(f, depth + 1, options));
            }
            else if (child is IFile file)
            {
                builder.AppendLine($"{indent}{options.FileSymbol} {file.Name}");
            }
        }

        return builder.ToString();
    }
}