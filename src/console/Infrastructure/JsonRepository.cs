using System.IO;
using System.Text.Json;

/// <summary>
/// JSON読み込みリポジトリ
/// </summary>
public class JsonRepository : IJsonRepository
{
    /// <summary>
    /// JSONファイルを読み込んでClass情報を返す
    /// </summary>
    /// <param name="filePath">JSONファイル</param>
    /// <returns>Classエンティティ</returns>
    public Class CreateClassEntityFromFile(string filePath)
    {
        var result = string.Empty;

        // ファイル読み込み
        result = File.ReadAllText(filePath);

        // 文字列として読み取り
        return CreateClassEntityFromString(result);
    }

    /// <summary>
    /// JSON文字列を読み込んでClass情報を返す
    /// </summary>
    /// <param name="json">JSO文字列</param>
    /// <returns>Classエンティティ</returns>
    public Class CreateClassEntityFromString(string json)
    {
        List<Class> classes = new ();
        // HACK クラスエンティティを作成
        var rootClass = JsonParse(json,"RootClass", classes, 0);
        rootClass.InnerClass.AddRange(classes);

        return rootClass;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="className">クラス名</param>
    /// <param name="innerClass">インナークラスリスト(nullの場合は自身のインスタンスを利用)</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    private Class JsonParse(string json, string className, List<Class> innerClass, int innerClassNo)
    {
        return ProcessJsonDocument(json, className, innerClass, innerClassNo);
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    /// <param name="className">クラス名</param>
    /// <param name="innerClass">インナークラスリスト</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    private Class ProcessJsonDocument(string json, string className, List<Class> innerClass, int innerClassNo)
    {
        var properties = new List<Property>();

        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            // C# 値型を取得する
            var propertyType = GetPropertyType(element.Value);

            // 追加の処理を入れる
            switch (element.Value.ValueKind)
            {
                case JsonValueKind.Undefined:
                    // TODO 例外エラー
                    break;

                case JsonValueKind.Object:
                    // インナークラス番号をインクリメント
                    innerClassNo++;

                    // インナークラス　C# 値型を取得する
                    propertyType = getInnerClassName(innerClassNo);

                    // インナークラス生成
                    innerClass.Add(JsonParse(element.Value.ToString(), propertyType, innerClass, innerClassNo));

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
                            // インナークラス番号をインクリメント
                            innerClassNo++;

                            // インナークラス名を取得
                            propertyType = getInnerClassName(innerClassNo);

                            // インナークラス生成
                            innerClass.Add(JsonParse(element.Value[arrayIndex].ToString(), propertyType, innerClass, innerClassNo));

                            // nullableなList設定
                            propertyType = $"List<{propertyType}>?";
                            break;
                        }

                        // 値型のリストを作成
                        propertyType = $"List<{GetPropertyType(element.Value[arrayIndex])}>?";
                        break;
                    }
                    break;
            }

            // プロパティ追加
            properties.Add(Property.Create(element.Name, propertyType));
        }
        // HACK Classいスタンス作成
        //return Class.Create(properties.AsReadOnly(), className);
        return Class.Create(properties, className);

        string getInnerClassName(int innerClassNo)
        {
            var innerClassName = "InnerClass";
            if (innerClassNo >= 2)
            {
                innerClassName += $"{Convert.ToChar('A' + (innerClassNo - 2))}";
            }
            return innerClassName;
        }
    }

    /// <summary>
    /// プロパティのC#の型を取得する
    /// </summary>    
    /// <param name="src">対象インスタンス</param>
    /// <returns>C#の型</returns>
    private string GetPropertyType(JsonElement src)
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