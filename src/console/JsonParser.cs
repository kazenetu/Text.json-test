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

        #region JsonValueKindごとの処理
            (string kindName, JsonValueKind valueKind) JsonValueKindString(JsonElement elementValue, (string kindName, JsonValueKind valueKind) propertyData)
            {
                if (DateTime.TryParse(elementValue.ToString(), out var _))
                {
                    propertyData.kindName = "DateTime";
                }
                return propertyData;
            }

            (string kindName, JsonValueKind valueKind) JsonValueKindArray(JsonElement elementValue,  (string kindName, JsonValueKind valueKind) propertyData)
            {
                var arrayType = string.Empty;
                var arrayIndex = 0;
                while (arrayIndex < elementValue.GetArrayLength())
                {
                    if (string.IsNullOrEmpty(arrayType) || arrayType == elementValue[arrayIndex].ValueKind.ToString())
                    {
                        var (kindName, ValueKind) = GetPropertyNameAndKind(elementValue[arrayIndex]);
                        arrayType = kindName;

                        if (ValueKind == JsonValueKind.Object)
                        {
                            var jsonSrc = elementValue[arrayIndex].ToString();
                            innerProperties?.Add(new JsonParser(jsonSrc, level + 1).Result);
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

                return propertyData;
            }

            (string kindName, JsonValueKind valueKind) JsonValueKindObject(JsonElement elementValue, (string kindName, JsonValueKind valueKind) propertyData)
            {
                foreach (var objElement in elementValue.EnumerateObject())
                {
                    var (kindName, ValueKind) = GetPropertyNameAndKind(objElement.Value);
                    innerProperties?.Add($"  {kindName} {objElement.Name}{Environment.NewLine}");

                    if (ValueKind == JsonValueKind.Object)
                    {
                        innerProperties?.Add(new JsonParser(objElement.Value.ToString(), level + 2).Result);
                        break;
                    }
                }
                return propertyData;
            }
        #endregion
    }

    /// <summary>
    /// JsonElementの型名とJsonValueKindを取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>型名とJsonValueKindのタプル</returns>
    private (string kindName, JsonValueKind valueKind) GetPropertyNameAndKind(JsonElement src)
    {
        // 型を特定する
        var valueKind = src.ValueKind switch
        {
            JsonValueKind.String => "String",
            JsonValueKind.Array => "Array",
            JsonValueKind.Number => "Number",
            JsonValueKind.Object => "Object",
            JsonValueKind.Undefined => "Undefined",
            JsonValueKind.True => "True",
            JsonValueKind.False => "False",
            JsonValueKind.Null => "Null",
            _ => "none..."
        };
        return (valueKind, src.ValueKind);
    }

}