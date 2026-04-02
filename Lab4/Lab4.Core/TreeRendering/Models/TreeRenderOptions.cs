namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

public class TreeRenderOptions
{
    public int MaxDepth { get; }

    public string FolderSymbol { get; }

    public string FileSymbol { get; }

    public string IndentUnit { get; }

    public TreeRenderOptions(int maxDepth, string folderSymbol, string fileSymbol, string indentUnit)
    {
        MaxDepth = maxDepth;
        FolderSymbol = folderSymbol;
        FileSymbol = fileSymbol;
        IndentUnit = indentUnit;
    }
}