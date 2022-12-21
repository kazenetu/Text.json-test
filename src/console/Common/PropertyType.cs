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
    /// C#型情報を返す
    /// </summary>
    /// <returns>C#型情報</returns>
    public override string ToString()
    {
        var csTypeName = ToPropertyType();
        if(IsList){
            return $"List<{csTypeName}>?";
        }
        return csTypeName;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="srcTypeName">type名</param>
    /// <param name="isList">配列か否か</param>
    /// <returns>プロパティ型インスタンス</returns>
    public static PropertyType Create(string srcTypeName, bool isList = false)
    {
        return new PropertyType()
        {
            // 型種別設定
            Kind = GetKind(srcTypeName),

            // 配列か否かの設定
            IsList = isList,
        };
    }

    /// <summary>
    /// 型種別を取得する
    /// </summary>    
    /// <param name="srcTypeName">type名</param>
    /// <returns>型情報</returns>
    private static Kinds GetKind(string srcTypeName)
    {
        // 型を特定する
        switch (srcTypeName.ToLower())
        {
            case "string":
                return Kinds.String;
            case "number":
                return Kinds.Decimal;
            case "true":
                return Kinds.Bool;
            case "false":
                return Kinds.Bool;
            case "null":
                return Kinds.Null;
        }
        // それ以外は例外エラー
        throw new Exception($"{srcTypeName} has no type set");
    }

    /// <summary>
    /// C#の型に変換
    /// </summary>
    /// <returns></returns>
    private string ToPropertyType()
    {
        switch (Kind)
        {
            case Kinds.String:
                return "string";
            case Kinds.Decimal:
                return "decimal";
            case Kinds.Bool:
                return "bool";
            case Kinds.Null:
                return "object";
            case Kinds.Class:
                return ClassName;
        }
        // それ以外は例外エラー
        throw new Exception($"{Kind} has no type set");
    }
}