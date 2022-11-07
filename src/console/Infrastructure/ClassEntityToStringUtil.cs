using System.Text;

/// <summary>
/// クラスエンティティからclass文字列を作成するユーティリティクラス
/// </summary>
public static class ClassEntityToStringUtil
{
    /// <summary>
    /// クラスエンティティからclass文字列生成して返す
    /// </summary>
    /// <param name="classEntity">クラスエンティティインスタンス</param>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    public static string GetClassString(Class classEntity, int indentLevel = 0)
    {
        var result = new StringBuilder();

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");
        result.AppendLine($"{levelSpace}public class {classEntity.Name}");
        result.AppendLine($"{levelSpace}{{");

        // クラス文字列作成
        foreach (var classInstance in classEntity.InnerClass)
        {
            result.AppendLine($"{levelSpace}{GetClassString(classInstance, indentLevel + 1)}");
        }

        // プロパティ文字列作成
        foreach (var property in classEntity.Properties)
        {
            result.Append($"{levelSpace}{GetPropertyString(property, indentLevel + 1)}");
        }

        result.AppendLine($"{levelSpace}}}");

        return result.ToString();
    }

    /// <summary>
    /// プロパティエンティティからプロパティ文字列を作成して返す
    /// </summary>
    /// <param name="propertyEntity">プロパティエンティティインスタンス</param>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>プロパティ文字列</returns>
    private static string GetPropertyString(Property propertyEntity, int indentLevel)
    {
        var result = new StringBuilder();

        // デフォルト設定
        var defaultValue = string.Empty;
        if (propertyEntity.TypeName == "string" || propertyEntity.TypeName == "object")
        {
            defaultValue = "string.Empty";
        }

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");

        // プロパティ文字列作成
        result.Append($"{levelSpace}public {propertyEntity.TypeName} {propertyEntity.Name}{{set; get;}}");
        if (!string.IsNullOrEmpty(defaultValue))
        {
            result.Append($" = {defaultValue};");
        }
        result.AppendLine();

        return result.ToString();
    }
}