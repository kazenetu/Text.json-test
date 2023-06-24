namespace Domain.ValueObjects;

/// <summary>
/// 基底プロパティ型ValueObject
/// </summary>
/// <param name="IsList">配列か否か</param>
public abstract record BasePropertyType(bool IsList);

/// <summary>
/// プロパティ型ValueObject
/// </summary>
public record PropertyType : BasePropertyType
{
    /// <summary>
    /// 型種別
    /// </summary>
    public enum Kinds
    {
        String,
        Decimal,
        Bool,
        Class,
        Null
    }

    /// <summary>
    /// C#Typeと値種別のリスト
    /// </summary>
    private static readonly List<(Type type, Kinds kind)> TypeKinds = new()
    {
        (typeof(string), Kinds.String),
        (typeof(decimal), Kinds.Decimal),
        (typeof(bool), Kinds.Bool),
        (typeof(Nullable), Kinds.Null),
    };

    /// <summary>
    /// 型種別
    /// </summary>
    public Kinds Kind { get; init; }

    /// <summary>
    /// クラス名
    /// </summary>
    /// <value>クラス名(デフォルトはstring.Empty)</value>
    public string ClassName { get; } = string.Empty;

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="type">C#Type</param>
    /// <param name="isList">配列か否か</param>
    /// <returns>プロパティ型インスタンス</returns>
    public PropertyType(Type type, bool isList) : base(isList)
    {
        // 対象抽出
        var target = TypeKinds.Where(item => item.type == type);

        // パラメータチェック
        if(!target.Any())  throw new ArgumentException($"{nameof(type)}({type.Name}) is null");

        // 型種別設定
        Kind = target.First().kind;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="classNo">クラス名用No</param>
    /// <param name="isList">配列か否か</param>
    /// <returns>プロパティ型インスタンス</returns>
    public PropertyType(int classNo, bool isList) : base(isList)
    {
        // パラメータチェック
        if (classNo < 0) throw new ArgumentException($"{nameof(classNo)} is negative value");

        // クラス名を設定
        var className = "InnerClass";
        if (classNo >= 2)
        {
            className += $"{Convert.ToChar('A' + (classNo - 2))}";
        }

        // 型種別設定
        Kind = GetKind(string.Empty, className);

        // クラス名
        ClassName = className;
    }

    /// <summary>
    /// C#型情報を返す
    /// </summary>
    /// <returns>C#型情報</returns>
    public override string ToString()
    {
        var csTypeName = ToPropertyType();
        if (IsList)
        {
            return $"List<{csTypeName}>?";
        }
        return csTypeName;
    }

    /// <summary>
    /// 型種別を取得する
    /// </summary>    
    /// <param name="srcTypeName">type名</param>
    /// <param name="className">クラス名</param>
    /// <returns>型情報</returns>
    private Kinds GetKind(string srcTypeName, string className)
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
        // クラス名が存在しない場合は例外エラー
        if (string.IsNullOrEmpty(className))
        {
            throw new Exception($"{srcTypeName} has no type set");
        }

        return Kinds.Class;
    }

    /// <summary>
    /// C#の型に変換
    /// </summary>
    /// <returns></returns>
    [Obsolete]
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
                if(IsList)
                {
                    return $"{ClassName}";
                }
                return $"{ClassName}?";
        }
        // それ以外は例外エラー
        throw new Exception($"{Kind} has no type set");
    }
}