using System.Text;
/// <summary>
/// プロパティ情報
/// </summary>
public class Property
{
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; init; }

    /// <summary>
    /// 型名
    /// </summary>
    /// <value>型名称</value>
    public string TypeName { get; init; }

    /// <summary>
    /// 初期値
    /// </summary>
    /// <value>初期値</value>
    public string DefaultValue {get; init;} = string.Empty;

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Property()
    {
        Name = string.Empty;
        TypeName = string.Empty;
    }

    /// <summary>
    /// 構成情報を返す
    /// </summary>
    /// <param name="level">インデックスレベル</param>
    public string ToString(int level)
    {
        var result = new StringBuilder();

        var levelSpace = new string('S' , level).Replace("S","  ");
        result.Append($"{levelSpace}public {TypeName} {Name}{{set; get;}}");
        if(!string.IsNullOrEmpty(DefaultValue))
        {
            result.Append($" = {DefaultValue};");
        }
        result.AppendLine();

        return result.ToString();
    }
    
    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="typeName">型名称</param>
    /// <returns>プロパティエンティティ インスタンス</returns>
    public static Property Create(string name, string typeName)
    {
        var defaultValue = string.Empty; 
        if(typeName == "string" || typeName == "object")
        {
            defaultValue = "string.Empty";
        }

        return new Property()
        {
            Name = name,
            TypeName = typeName,
            DefaultValue = defaultValue
        };
    }
}
