namespace Appplication.Commands;

/// <summary>
/// C#用コマンドクラス
/// </summary>
/// <param name="NameSpace">名前空間</param>
/// <param name="RootPath">出力ルートパス</param>
/// <param name="RootClassName">ルートクラス名(ファイル名)</param>
/// <param name="indentSpaceCount">インデントスペース数</param>
public record CSharpCommand(string NameSpace, string RootPath,string RootClassName, int indentSpaceCount = 2);