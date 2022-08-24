using System.Text;
using System.Text.Json;

internal class Program
{
    /// <summary>
    /// エントリメソッド
    /// </summary>    
    private static void Main(string[] args)
    {
        var json = string.Empty;

        // 単純なJSON文字列作成
        json = @"{
            ""propString"" : ""string""
            , ""propNumber"":10
            , ""propDate"":""2022/01/01 10:11:12""
            , ""propTrue"":true
            , ""propFalse"":false
            , ""propNull"":null
            , ""propArray"":[1,2,3]
        }";

        Console.WriteLine("--単純なJSON文字列--");
        ProcessJsonDocument(json);
        // 出力：
        // --単純なJSON文字列--
        // String propString
        // Number propNumber
        // DateTime propDate
        // True propTrue
        // False propFalse
        // Null propNull
        // Array(Number) propArray

        // Objectが含まれるJSON文字列作成
        json = @"{
            ""propObjct"" : 
            {
                ""propObjString"":""propObjString""
            }
            , ""propNumber"":10
        }";

        Console.WriteLine("--Objectが含まれるJSON文字列--");
        ProcessJsonDocument(json);
        // --Objectが含まれるJSON文字列--
        // Object propObjct
        //   String propObjString
        //   
        // Number propNumber

        // Object配列が含まれるJSON文字列作成
        json = @"{
            ""propObjcts"" : 
            [
                    {
                        ""propObjString"":""propObjString""
                    }
                    ,{
                        ""propObjString"":""propObjString""
                    }
            ]
        }";

        Console.WriteLine("--Object配列が含まれるJSON文字列--");
        ProcessJsonDocument(json);
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    private static void ProcessJsonDocument(string json)
    {
        var innerProperties = new StringBuilder();
        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            // 型を特定する
            var propertyData = GetPropertyNameAndKind(element.Value);

            // 追加の処理を入れる
            switch(propertyData.valueKind)
            {
                case JsonValueKind.String:
                    if(DateTime.TryParse(element.Value.ToString(), out var _))
                    {
                        propertyData.kindName = "DateTime";
                    }
                    break;
                case JsonValueKind.Array:
                    var arrayType = string.Empty;
                    var arrayIndex = 0;
                    while(arrayIndex < element.Value.GetArrayLength())
                    {
                        var item = GetPropertyNameAndKind(element.Value[arrayIndex]);
                        if(string.IsNullOrEmpty(arrayType) || arrayType == element.Value[arrayIndex].ValueKind.ToString())
                        {
                            arrayType = element.Value[arrayIndex].ValueKind.ToString();
                        }
                        else
                        {
                            arrayType = "etc";
                        }
                        arrayIndex++;
                    }
                    if(string.IsNullOrEmpty(arrayType))
                    {
                        arrayType = "noting...";
                    }
                    propertyData.kindName += $"({arrayType})";
                    break;
                case JsonValueKind.Object:
                    foreach (var objElement in element.Value.EnumerateObject())
                    {
                        var (kindName,ValueKind) = GetPropertyNameAndKind(objElement.Value);
                        innerProperties.AppendLine($"  {kindName} {objElement.Name}");
                    }
                    break;
            }
            Console.WriteLine($"{propertyData.kindName} {element.Name}");
            if(innerProperties.Length > 0)
            {
                Console.WriteLine(innerProperties.ToString());
            }
            innerProperties.Clear();
        }
    }

    /// <summary>
    /// JsonElementの型名とJsonValueKindを取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>型名とJsonValueKindのタプル</returns>
    private static (string kindName, JsonValueKind valueKind) GetPropertyNameAndKind(JsonElement src)
    {
        // 型を特定する
        var valueKind = "none...";
        switch(src.ValueKind)
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