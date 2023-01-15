using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;

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
    public ClassesEntity CreateClassEntityFromFile(string filePath)
    {
        var result = string.Empty;

        // ファイル読み込み
        result = File.ReadAllText(filePath);

        // 文字列として読み取り
        var rootClassName = Path.GetFileNameWithoutExtension(filePath);
        rootClassName = $"{rootClassName.Substring(0,1).ToUpper()}{rootClassName.Substring(1)}";
        return CreateClassEntityFromString(result, rootClassName);
    }

    /// <summary>
    /// JSON文字列を読み込んでClass情報を返す
    /// </summary>
    /// <param name="json">JSO文字列</param>
    /// <returns>Classエンティティ</returns>
    /// <param name="rootClassName">ルートクラス名</param>
    public ClassesEntity CreateClassEntityFromString(string json, string rootClassName)
    {
        List<Class> innerClasses = new ();

        rootClassName = $"{rootClassName.Substring(0,1).ToUpper()}{rootClassName.Substring(1)}";
        var rootClass = JsonParse(json, rootClassName, innerClasses, 0);

        var result = ClassesEntity.Create(rootClass);
        foreach(var innerClass in innerClasses)
        {
            result.AddInnerClass(innerClass);
        }

        return result;
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
        // Classインスタンス作成
        var classEntity =  Class.Create(className);

        var jsonDocument = JsonDocument.Parse(json);
        var rootElement = jsonDocument.RootElement;
        foreach (var element in rootElement.EnumerateObject())
        {
            var classJson = string.Empty;
            var isList = false;

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

                    // インナークラス用JSON文字列を格納
                    classJson = element.Value.ToString();

                    break;

                case JsonValueKind.Array:
                    isList = true;

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
                            classJson = element.Value[arrayIndex].ToString();

                            break;
                        }
                        propertyType = GetPropertyType(element.Value[arrayIndex]);
                        break;
                    }
                    break;
            }

            // プロパティ追加
            if(string.IsNullOrEmpty(classJson))
            {
                classEntity.AddProperty(Property.Create(element.Name, new PropertyType(propertyType, isList)));
            }
            else
            {
                classEntity.AddProperty(Property.Create(element.Name, new PropertyType(innerClassNo, isList)));

                // インナークラス生成
                var targetProperty = classEntity.Properties.Last();
                innerClass.Add(JsonParse(classJson, targetProperty.PropertyTypeClassName, innerClass, innerClassNo));
            }
        }

        return classEntity;
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
                result = "number";
                break;
            case JsonValueKind.True:
                result = "true";
                break;
            case JsonValueKind.False:
                result = "true";
                break;
            case JsonValueKind.Null:
                result = "null";
                break;
        }
        return result;
    }

}