namespace Domain.Commands;

/// <summary>
/// ファイル出力コマンドクラス
/// </summary>
/// <param name="RootPath">ファイル出力のルートパス</param>
/// <param name="NameSpace">名前空間</param>
public record FileOutputCommand(string RootPath, string NameSpace);