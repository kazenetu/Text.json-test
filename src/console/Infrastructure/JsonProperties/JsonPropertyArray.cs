using System.Text.Json;
using Domain.ValueObjects;

namespace Infrastructure.JsonProperties;

/// <summary>
/// JSONプロパティ：Array
/// </summary>
public class JsonPropertyArray : IJsonProperty
{
    /// <summary>
    /// キー名
    /// </summary>
    /// <returns>JsonValueKind</returns>
    public JsonValueKind GetKeyName()
    {
        return JsonValueKind.Array;
    }

    /// <summary>
    /// Jsonプロパティ結果の取得
    /// </summary>
    /// <param name="element">対象インスタンス</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    /// <returns>Jsonプロパティ結果</returns>
    public JsonPropertyResult GetJsonPropertyResult(JsonProperty element, int innerClassNo)
    {
        var innerClassJson= string.Empty;
        var propertyType = string.Empty;
        var arrayIndex = 0;
        while (arrayIndex < element.Value.GetArrayLength())
        {
            var ValueKind = element.Value[arrayIndex].ValueKind;

            // クラス作成
            if (ValueKind == JsonValueKind.Object)
            {
                // インナークラス番号をインクリメント
                innerClassNo++;

                // インナークラス用JSON文字列を格納
                innerClassJson = element.Value[arrayIndex].ToString();

                break;
            }
            propertyType = IJsonProperty.GetPropertyType(element.Value[arrayIndex]);
            break;
        }

        // プロパティ生成
        PropertyValueObject prop;
        if (string.IsNullOrEmpty(innerClassJson))
        {
            // 値プロパティ
            prop = new PropertyValueObject(element.Name, new PropertyType(propertyType, true));
        }
        else
        {
            // インナークラス
            prop = new PropertyValueObject(element.Name, new PropertyType(innerClassNo, true));
        }

        return new JsonPropertyResult(prop, innerClassJson, innerClassNo);
    }
}