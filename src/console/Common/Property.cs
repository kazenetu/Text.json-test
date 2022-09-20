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
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Property()
    {
        Name = string.Empty;
        TypeName = string.Empty;
        Comment = string.Empty;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="typeName">型名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <returns>プロパティエンティティ インスタンス</returns>
    public static Property Create(string name, string typeName, string comment)
    {
        return new Property()
        {
            Name = name,
            TypeName = typeName,
            Comment = comment
        };
    }
}