namespace Domain.ValueObjects;

/// <summary>
/// プロパティValueObject
/// </summary>
public record PropertyValueObject
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
    /// コンストラクタ
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="propertyType">型クラス インスタンス</param>
    public PropertyValueObject(string name, PropertyType propertyType)
    {
        // パラメータチェック
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} is null");

        // デフォルト値設定
        if (propertyType.ToString() is "string" or "object")
        {
            DefaultValue = "string.Empty";
        }

        // 名前とプロパティ型の設定
        Name = name;
        Type = propertyType;
    }

    /// <summary>
    /// C#プロパティ文字列を返す
    /// </summary>
    /// <returns>C#プロパティ文字列</returns>
    public override string ToString()
    {
        // デフォルト文字列設定
        var defualt = string.Empty;
        if (DefaultValue is not "")
        {
            defualt = $" = {DefaultValue};";
        }

        return $"{Type} {Name}{{set; get;}}{defualt}";
    }
}
