using System.Collections.ObjectModel;
using System.Text;

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

        // クラス
        foreach (var classInstance in InnerClass)
        {
            result.AppendLine($"{levelSpace}{classInstance.ToString(level + 1)}");
        }

        // プロパティ
        foreach(var property in Properties){
            result. Append($"{levelSpace}{property.ToString(level + 1)}");
        }

        return result.ToString();
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static Class Create(string name)
    {
        var result = new Class()
        {
            Name = name,
        };

        return result;
    }
}