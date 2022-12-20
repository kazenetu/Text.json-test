/// <summary>
/// プロパティ型
/// </summary>
public class PropertyType
{
    /// <summary>
    /// 型種別
    /// </summary>
    private enum Kinds
    {
        String,
        Decimal,
        Bool,
        Class,
        Null
    }

    /// <summary>
    /// 型種別
    /// </summary>
    private Kinds Kind;

    /// <summary>
    /// クラス名
    /// </summary>
    /// <value>クラス名(デフォルトはstring.Empty)</value>
    public string ClassName {get; init;}

    /// <summary>
    /// 配列か否か
    /// </summary>
    private bool IsList;

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private PropertyType()
    {
        // クラス名はstring.Emptyがデフォルト
        ClassName = string.Empty;

        // 配列なし
        IsList = false;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="srcTypeName">type名</param>
    /// <param name="isList">配列か否か</param>
    /// <returns>プロパティ型インスタンス</returns>
    public static PropertyType Create(string srcTypeName, bool isList = false)
    {
        // TODO Type名を列挙型に変換

        return new PropertyType()
        {
            // HACK 種別設定
            Kind = Kinds.String,

            // 配列か否かの設定
            IsList = isList,
        };
    }
}