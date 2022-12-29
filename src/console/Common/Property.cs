using System.Text;
/// <summary>
/// プロパティValueObject
/// </summary>
public record Property
{
    /// <summary>
    /// Class型名を返す
    /// </summary>
    /// <value>Class型名(未設定の場合はstring.Empty)</value>
    public string PropertyTypeClassName
    {
        get
        {
            return Type?.ClassName ?? string.Empty;
        }
    }

    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    private string Name = string.Empty;

    /// <summary>
    /// 型ValueObject
    /// </summary>
    /// <value>型ValueObject</value>
    private PropertyType? Type;

    /// <summary>
    /// 初期値
    /// </summary>
    /// <value>初期値</value>
    private string DefaultValue = string.Empty;

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Property()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="propertyType">型クラス インスタンス</param>
    /// <returns>プロパティValueObject インスタンス</returns>
    public static Property Create(string name, PropertyType propertyType)
    {
        var defaultValue = string.Empty;
        if (propertyType.ToString() is "string" or "object")
        {
            defaultValue = "string.Empty";
        }

        return new Property()
        {
            Name = name,
            Type = propertyType,
            DefaultValue = defaultValue
        };
    }

    /// <summary>
    /// プロパティを返す
    /// </summary>
    /// <returns>C#プロパティ</returns>
    public override string ToString()
    {
        var defualt = string.Empty;
        if(DefaultValue is not "")
        {
            defualt = $" = {DefaultValue};";
        }

        return $"{Type} {Name}{{set; get;}}{defualt}";
    }
}
