using System.Text.Json;

namespace Infrastructure.JsonProperties;

/// <summary>
/// Jsonプロパティインターフェイス
/// </summary>
public interface IJsonProperty
{
    /// <summary>
    /// キー名
    /// </summary>
    /// <returns>JsonValueKind</returns>
    JsonValueKind GetKeyName();

    /// <summary>
    /// Jsonプロパティ結果の取得
    /// </summary>
    /// <param name="src">対象インスタンス</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    /// <returns>Jsonプロパティ結果</returns>
    JsonPropertyResult GetJsonPropertyResult(JsonProperty src, int innerClassNo);

    /// <summary>
    /// プロパティのC#の型を取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>C#の型</returns>
    protected static string GetPropertyType(JsonElement src)
    {
        return src.ValueKind switch
        {
            JsonValueKind.String => "string",
            JsonValueKind.Number => "number",
            JsonValueKind.True => "true",
            JsonValueKind.False => "true",
            JsonValueKind.Null => "null",
            _ => string.Empty,
        };
    }
}