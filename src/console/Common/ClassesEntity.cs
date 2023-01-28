using System.Text;

/// <summary>
/// クラス集約エンティティ
/// </summary>
public class ClassesEntity
{
    /// <summary>
    /// 非公開インナークラスリスト
    /// </summary>
    /// <returns>非公開インナークラスリスト</returns>
    private List<Class> innerClasses = new();

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
    /// ルートクラスのプロパティ追加
    /// </summary>
    /// <param name="Property">追加対象</param>
    public void AddRootProperty(Property Property)
    {
        // HACK ルートクラス存在チェック
        if(RootClass is null) new Exception($"{nameof(RootClass)} is null");

        // プロパティ追加
        RootClass?.AddProperty(Property);
    }

    /// <summary>
    /// インナークラスの追加
    /// </summary>
    /// <param name="innerClass">追加対象</param>
    public void AddInnerClass(Class innerClass)
    {
        // 入力チェック
        if(innerClass is null) new ArgumentException($"{nameof(innerClass)} is null");

        // インナークラスリストに追加
        innerClasses.Add(innerClass!);
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="rootClassName">ルートクラス名</param>
    /// <returns>クラス集約エンティティ インスタンス</returns>
    public static ClassesEntity Create(string rootClassName)
    {
        // 入力チェック
        if(rootClassName is null) new ArgumentException($"{nameof(rootClassName)} is null");

        // インスタンスを返す
        var result = new ClassesEntity()
        {
            RootClass = Class.Create(rootClassName!)
        };

        return result;
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
            // インナークラスのクラス文字列作成
            foreach (var classInstance in innerClasses)
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

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");

        // プロパティ文字列作成
        result.Append($"{levelSpace}public {propertyEntity}");
        result.AppendLine();

        return result.ToString();
    }
    #endregion

}