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
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    private static void ProcessJsonDocument(string json)
    {
        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            // 型を特定する
            var valueKind = "none...";
            switch(element.Value.ValueKind)
            {
                case JsonValueKind.String:
                    valueKind = "String";
                    if(DateTime.TryParse(element.Value.ToString(), out var _))
                    {
                        valueKind = "DateTime";
                    }
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
            Console.WriteLine($"{valueKind} {element.Name}");
        }
    }
}