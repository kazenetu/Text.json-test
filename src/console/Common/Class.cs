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
    /// <value>クラス名</value>
    public string Name { get; init; }

    /// <summary>
    /// プロパティリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public IReadOnlyList<Property> Properties { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Class()
    {
        Name = string.Empty;
        Properties = new List<Property>();
    }

    /// <summary>
    /// 非公開プロパティリスト
    /// </summary>
    /// <returns>非公開プロパティリスト</returns>
    private List<Property> propertyies = new ();

    /// <summary>
    /// プロパティ追加
    /// </summary>
    /// <param name="Property">追加対象</param>
    public void AddProperty(Property Property)
    {
        // 入力チェック
        if(Property is null) new ArgumentException($"{nameof(Property)} is null");

        // プロパティリスト追加
        propertyies.Add(Property);
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
            Properties = new List<Property>(properties)
        };
   }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="src">ディープコピー元のクラスエンティティ インスタンス</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static Class Create(Class src)
    {
        // 入力チェック
        if(!src.Properties.Any()) new ArgumentException($"{nameof(Properties)} count is zero");
        if(string.IsNullOrEmpty(src.Name)) new ArgumentException($"{nameof(Name)} is null");

        // インスタンスを返す
        return new Class()
        {
            Name = src.Name,
            Properties = new List<Property>(src.Properties)
        };
   }
}