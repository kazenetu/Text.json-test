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
    public ClassesEntity CreateClassEntityFromFile(string filePath)
    {
        var result = string.Empty;

        // ファイル読み込み
        result = File.ReadAllText(filePath);

        // 文字列として読み取り
        var rootClassName = Path.GetFileNameWithoutExtension(filePath);
        rootClassName = $"{rootClassName.Substring(0, 1).ToUpper()}{rootClassName.Substring(1)}";
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
        rootClassName = $"{rootClassName.Substring(0, 1).ToUpper()}{rootClassName.Substring(1)}";
        var classesEntity = ClassesEntity.Create(rootClassName);

        // JSON文字列読み込み
        JsonParse(json, rootClassName, ref classesEntity, 0);

        return classesEntity;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="className">クラス名</param>
    /// <param name="classesEntity">集約エンティティ</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    private Class JsonParse(string json, string className, ref ClassesEntity classesEntity, int innerClassNo)
    {
        return ProcessJsonDocument(json, className, ref classesEntity, innerClassNo);
    }

    /// <summary>
    /// JsonDocumentで構造を解析する
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    /// <param name="className">クラス名</param>
    /// <param name="classesEntity">集約エンティティ</param>
    /// <param name="innerClassNo">インナークラス番号</param>
    private Class ProcessJsonDocument(string json, string className, ref ClassesEntity classesEntity, int innerClassNo)
    {
        // Classインスタンス設定
        var classEntity = Class.Create(className);

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

            // プロパティ生成
            var createInnerClass = false;
            PropertyValueObject prop;
            if (string.IsNullOrEmpty(classJson))
            {
                prop = new PropertyValueObject(element.Name, new PropertyType(propertyType, isList));
            }
            else
            {
                prop = new PropertyValueObject(element.Name, new PropertyType(innerClassNo, isList));
                createInnerClass = true;
            }

            // プロパティ追加
            if (className == classesEntity.Name)
            {
                classesEntity.AddRootProperty(prop);
            }
            else
            {
                classEntity.AddProperty(prop);
            }

            // インナークラス追加
            if (!string.IsNullOrEmpty(classJson) && createInnerClass)
            {
                classesEntity.AddInnerClass(JsonParse(classJson, prop.PropertyTypeClassName, ref classesEntity, innerClassNo));
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