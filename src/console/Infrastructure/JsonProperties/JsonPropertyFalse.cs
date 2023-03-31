using System.Text.Json;
using Domain.ValueObjects;

namespace Infrastructure.JsonProperties;

/// <summary>
/// JSONプロパティ：false
/// </summary>
public class JsonPropertyFalse : IJsonProperty
{
    /// <summary>
    /// キー名
    /// </summary>
    /// <returns>JsonValueKind</returns>
    public JsonValueKind GetKeyName()
    {
        return JsonValueKind.False;
    }

    /// <summary>
    /// Jsonプロパティ結果の取得
    /// </summary>
    /// <param name="element">対象インスタンス</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    /// <returns>Jsonプロパティ結果</returns>
    public JsonPropertyResult GetJsonPropertyResult(JsonProperty element, int innerClassNo)
    {
        // プロパティ生成
        var propertyType = IJsonProperty.GetPropertyType(element.Value);
        var prop =  new PropertyValueObject(element.Name, new PropertyType(propertyType, false));
        return new JsonPropertyResult(prop, string.Empty, innerClassNo);
    }
}