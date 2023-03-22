namespace Domain.Commands;

/// <summary>
/// ファイル出力コマンドクラス
/// </summary>
public class FileOutputCommand
{
    /// <summary>
    /// ファイル出力のルートパス
    /// </summary>
    public string RootPath { get; init; }

    /// <summary>
    /// 名前空間
    /// </summary>
    public string NameSpace { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="rootPath">ファイル出力のルートパス</param>
    /// <param name="nameSpace">名前空間</param>
    public FileOutputCommand(string rootPath, string nameSpace)
    {
        RootPath = rootPath;
        NameSpace = nameSpace;
    }
}