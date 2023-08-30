namespace Domain.Commands;

/// <summary>
/// 出力言語タイプ
/// </summary>
public enum OutputLanguageType
{
    /// <summary>
    /// C#
    /// </summary>
    CS, 

    /// <summary>
    /// Kotlin
    /// </summary>
    KT,
}

/// <summary>
/// 追加パラメータ
/// </summary>
public enum ParamKeys
{
    /// <summary>
    /// C#用名前空間
    /// </summary>
    CS_NameSpace,

    /// <summary>
    /// Kotlin用パッケージ名
    /// </summary>
    KT_Package,

    /// <summary>
    /// インデントスペース数
    /// </summary>
    IndentSpaceCount,
}

/// <summary>
/// ファイル出力コマンドクラス
/// </summary>
/// <param name="RootPath">ファイル出力のルートパス</param>
/// <param name="LanguageType">出力言語タイプ</param>
/// <param name="IndentSpaceCount">インデントスペース数</param>
/// <param name="Params">追加パラメータ</param>
public record FileOutputCommand(string RootPath, OutputLanguageType LanguageType, int IndentSpaceCount, Dictionary<ParamKeys, string> Params);