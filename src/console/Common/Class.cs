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
    /// 非公開コンストラクタ
    /// </summary>
    private Class()
    {
        Name = string.Empty;
        Properties = new List<Property>();
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="properties">プロパティリスト</param>
    /// <param name="className">クラス名</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static Class Create(List<Property> properties, string className)
    {
        // 入力チェック
        if(!properties.Any()) new ArgumentException($"{nameof(properties)} count is zero");
        if(string.IsNullOrEmpty(className)) new ArgumentException($"{nameof(className)} is null");

        // インスタンスを返す
        return new Class()
        {
            Name = className,
            Properties = properties
        };
   }
}