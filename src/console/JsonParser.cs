using System.Text;
using System.Text.Json;

namespace ConsoleApp;

/// <summary>
/// Json解析クラス
/// </summary>    
class JsonParser
{

    /// <summary>
    /// 解析結果
    /// </summary>    
    public string Result { get; init; }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    /// <param name="level">ネストレベル</param>
    public JsonParser(string json, int level = 0)
    {
        Result = ProcessJsonDocument(json, level);
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    /// <param name="level">ネストレベル</param>
    /// <returns>解析結果</returns>
    private string ProcessJsonDocument(string json, int level)
    {
        var result = new StringBuilder();

        var innerProperties = new List<string>();
        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            // 型を特定する
            var propertyData = GetPropertyNameAndKind(element.Value);

            // 追加の処理を入れる
            switch (propertyData.valueKind)
            {
                case JsonValueKind.String:
                    if (DateTime.TryParse(element.Value.ToString(), out var _))
                    {
                        propertyData.kindName = "DateTime";
                    }
                    break;
                case JsonValueKind.Array:
                    var arrayType = string.Empty;
                    var arrayIndex = 0;
                    while (arrayIndex < element.Value.GetArrayLength())
                    {
                        if (string.IsNullOrEmpty(arrayType) || arrayType == element.Value[arrayIndex].ValueKind.ToString())
                        {
                            var (kindName, ValueKind) = GetPropertyNameAndKind(element.Value[arrayIndex]);
                            arrayType = kindName;

                            if (ValueKind == JsonValueKind.Object)
                            {
                                var jsonSrc = element.Value[arrayIndex].ToString();
                                innerProperties.Add(new JsonParser(jsonSrc, level + 1).Result);
                                break;
                            }
                        }
                        else
                        {
                            arrayType = "etc";
                        }
                        arrayIndex++;
                    }
                    if (string.IsNullOrEmpty(arrayType))
                    {
                        arrayType = "noting...";
                    }
                    propertyData.kindName += $"({arrayType})";
                    break;
                case JsonValueKind.Object:
                    foreach (var objElement in element.Value.EnumerateObject())
                    {
                        var (kindName, ValueKind) = GetPropertyNameAndKind(objElement.Value);
                        innerProperties.Add($"  {kindName} {objElement.Name}{Environment.NewLine}");

                        if (ValueKind == JsonValueKind.Object)
                        {
                            innerProperties.Add(new JsonParser(objElement.Value.ToString(), level + 2).Result);
                            break;
                        }
                    }
                    break;
            }
            result.AppendLine($"{GetSpace(level)}{propertyData.kindName} {element.Name}");

            foreach (var property in innerProperties)
            {
                result.Append($"{GetSpace(level)}{property}");
            }
            innerProperties.Clear();
        }

        return result.ToString();

        string GetSpace(int level)
        {
            var spaceResult = string.Empty;
            while (level > 0)
            {
                spaceResult += "  ";
                level--;
            }
            return spaceResult;
        }
    }

    /// <summary>
    /// JsonElementの型名とJsonValueKindを取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>型名とJsonValueKindのタプル</returns>
    private (string kindName, JsonValueKind valueKind) GetPropertyNameAndKind(JsonElement src)
    {
        // 型を特定する
        var valueKind = "none...";
        switch (src.ValueKind)
        {
            case JsonValueKind.String:
                valueKind = "String";
                break;
            case JsonValueKind.Array:
                valueKind = "Array";
                break;
            case JsonValueKind.Number:
                valueKind = "Number";
                break;
            case JsonValueKind.Object:
                valueKind = "Object";
                break;
            case JsonValueKind.Undefined:
                valueKind = "Undefined";
                break;
            case JsonValueKind.True:
                valueKind = "True";
                break;
            case JsonValueKind.False:
                valueKind = "False";
                break;
            case JsonValueKind.Null:
                valueKind = "Null";
                break;
        }
        return (valueKind, src.ValueKind);
    }

}