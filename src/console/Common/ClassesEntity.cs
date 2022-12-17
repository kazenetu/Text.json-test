using System.Text;

/// <summary>
/// クラス集約エンティティ
/// </summary>
public class ClassesEntity
{
    /// <summary>
    /// インナークラスリスト
    /// </summary>
    /// <returns>インナークラスリスト</returns>
    private IReadOnlyList<Class>? InnerClasses = null;

    /// <summary>
    /// ルートクラス
    /// </summary>
    /// <returns>ルートクラス</returns>
    private Class? RootClass = null;

    /// <summary>
    /// ルートクラスのクラス名を返す
    /// </summary>
    /// <returns>ルートクラス</returns>
    public string Name{
        get => RootClass?.Name ?? "RootClass";
    }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private ClassesEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="rootClass">ルートクラス</param>
    /// <param name="innerClasses">インナークラスリスト</param>
    /// <returns>クラス集約エンティティ インスタンス</returns>
    public static ClassesEntity Create(Class rootClass, IReadOnlyList<Class> innerClasses)
    {
        // 入力チェック
        if(innerClasses is null) new NullReferenceException($"{nameof(innerClasses)} is null");
        if(innerClasses?.Count <= 0) new ArgumentException($"{nameof(innerClasses)} count is zero");

        // インスタンスを返す
        return new ClassesEntity()
        {
            RootClass = Class.Create(rootClass),
            InnerClasses = new List<Class>(innerClasses)
        };
    }

    #region class文字列作成

    /// <summary>
    /// class文字列生成して返す
    /// </summary>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    public string GetClassString(int indentLevel = 0)
    {
        //必須パラメータチェック
        if(RootClass is null){
            throw new Exception("RootClassが設定されていません");
        };

        var result = string.Empty;

        //HACK 名前空間の設定

        // ルートクラスを出力
        result += GetClassString(RootClass, indentLevel);


        return result;
    }

    /// <summary>
    /// クラスエンティティからclass文字列生成して返す
    /// </summary>
    /// <param name="classEntity">クラスエンティティインスタンス</param>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    private string GetClassString(Class classEntity, int indentLevel = 0)
    {
        var result = new StringBuilder();

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");
        result.AppendLine($"{levelSpace}public class {classEntity.Name}");
        result.AppendLine($"{levelSpace}{{");

        if(classEntity == RootClass)
        {
            //必須パラメータチェック
            if(InnerClasses is null){
                throw new Exception("InnerClassesが設定されていません");
            };

            // インナークラスのクラス文字列作成
            foreach (var classInstance in InnerClasses)
            {
                result.AppendLine($"{GetClassString(classInstance, indentLevel + 1)}");
            }
        }

        // プロパティ文字列作成
        foreach (var property in classEntity.Properties)
        {
            result.Append($"{GetPropertyString(property, indentLevel + 1)}");
        }

        result.AppendLine($"{levelSpace}}}");

        return result.ToString();
    }

    /// <summary>
    /// プロパティエンティティからプロパティ文字列を作成して返す
    /// </summary>
    /// <param name="propertyEntity">プロパティエンティティインスタンス</param>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>プロパティ文字列</returns>
    private string GetPropertyString(Property propertyEntity, int indentLevel)
    {
        var result = new StringBuilder();

        // デフォルト設定
        var defaultValue = string.Empty;
        if (propertyEntity.TypeName == "string" || propertyEntity.TypeName == "object")
        {
            defaultValue = "string.Empty";
        }

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");

        // プロパティ文字列作成
        result.Append($"{levelSpace}public {propertyEntity.TypeName} {propertyEntity.Name}{{set; get;}}");
        if (!string.IsNullOrEmpty(defaultValue))
        {
            result.Append($" = {defaultValue};");
        }
        result.AppendLine();

        return result.ToString();
    }
    #endregion

}