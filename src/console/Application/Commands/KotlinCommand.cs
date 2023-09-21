namespace Appplication.Commands;

/// <summary>
/// Kotlin用コマンドクラス
/// </summary>
/// <param name="PackageName">パッケージ名</param>
/// <param name="RootPath">出力ルートパス</param>
/// <param name="RootClassName">ルートクラス名(ファイル名)</param>
/// <param name="Prefix">固定プレフィックス</param>
/// <param name="Suffix">固定サフィックス</param>
public record KotlinCommand(string PackageName, string RootPath, string RootClassName, string Prefix = "", string Suffix = "") : CommonCommand(Prefix, Suffix);