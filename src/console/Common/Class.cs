using System.Text;
using System.Text.Json;

/// <summary>
/// クラス情報
/// </summary>
public class Class
{
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; init; }

    /// <summary>
    /// プロパティリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public List<Property> Properties { get; init; }

    /// <summary>
    /// インナークラスリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public List<Class> InnerClass { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Class()
    {
        Name = string.Empty;
        Properties = new List<Property>();
        InnerClass = new List<Class>();
    }

    /// <summary>
    /// 構成情報を返す
    /// </summary>
    /// <param name="level">インデックスレベル</param>
    public string ToString(int level)
    {
        var result = new StringBuilder();

        var levelSpace = string.Empty;
        var levelIndex = 0;
        while(levelIndex < level)
        {
            levelSpace += "  ";
            levelIndex++;
        }

        result.AppendLine($"{levelSpace}public class {Name} {{");

        // クラス
        foreach (var classInstance in InnerClass)
        {
            result.AppendLine($"{levelSpace}{classInstance.ToString(level + 1)}");
        }

        // プロパティ
        foreach(var property in Properties){
            result. Append($"{levelSpace}{property.ToString(level + 1)}");
        }

        result.AppendLine($"{levelSpace}}}");

        return result.ToString();
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="className">クラス名</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static Class JsonParse(string json, string className = "RootClass")
    {
        var result = new Class()
        {
            Name = className
        };
        result.ProcessJsonDocument(json);

        return result;
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    /// <returns>解析結果</returns>
    private void ProcessJsonDocument(string json)
    {
        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            // C# 値型を取得する
            var  propertyType = GetPropertyName(element.Value); 

            // 追加の処理を入れる
            switch (element.Value.ValueKind)
            {
                case JsonValueKind.Undefined:
                    // TODO 例外エラー
                    break;

                case JsonValueKind.Object:
                    // インナークラス名を取得
                    propertyType = getInnerClassName();

                    // インナークラス生成
                    InnerClass.Add(Class.JsonParse(propertyType, element.Value.ToString()));

                    // nullableに設定
                    propertyType += "?";
                    break;

                case JsonValueKind.Array:
                    var arrayIndex = 0;
                    while (arrayIndex < element.Value.GetArrayLength())
                    {
                        var ValueKind = element.Value[arrayIndex].ValueKind;

                        // クラス作成
                        if (ValueKind == JsonValueKind.Object)
                        {
                            // インナークラス名を取得
                            propertyType = getInnerClassName();

                            // インナークラス生成
                            InnerClass.Add(Class.JsonParse(propertyType, element.Value[arrayIndex].ToString()));

                            // nullableなList設定
                            propertyType = $"List<{propertyType}>?";
                            break;
                        }

                        // 値型のリストを作成
                        propertyType = $"List<{GetPropertyName(element.Value[arrayIndex])}>?";
                        break;
                    }
                    break;
            }

            // プロパティ追加
            Properties.Add(Property.Create(element.Name, propertyType));
        }

        string getInnerClassName()
        {
            var innerClassName ="innerClass";
            if(InnerClass.Any()){
                innerClassName += (InnerClass.Count + 1);
            }
            return innerClassName;
        }
    }

    /// <summary>
    /// C#の型を取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>C#の型</returns>
    private string GetPropertyName(JsonElement src)
    {
        // 型を特定する
        string result = string.Empty;
        switch (src.ValueKind)
        {
            case JsonValueKind.String:
                result = "string";
                break;
            case JsonValueKind.Number:
                result = "decimal";
                break;
            case JsonValueKind.True:
                result = "bool";
                break;
            case JsonValueKind.False:
                result = "bool";
                break;
            case JsonValueKind.Null:
                result = "object";
                break;
        }
        return result;
    }
}