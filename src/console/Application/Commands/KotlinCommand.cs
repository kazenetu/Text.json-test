namespace Appplication.Commands;

/// <summary>
/// Kotlin用コマンドクラス
/// </summary>
/// <param name="PackageName">パッケージ名</param>
/// <param name="RootPath">出力ルートパス</param>
/// <param name="RootClassName">ルートクラス名(ファイル名)</param>
public record KotlinCommand(string PackageName, string RootPath, string RootClassName);