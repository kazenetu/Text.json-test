/// <summary>
/// クラス集約エンティティ
/// </summary>
public class ClassesEntity
{
    /// <summary>
    /// インナークラスリスト
    /// </summary>
    /// <returns>インナークラスリスト</returns>
    public IReadOnlyList<Class>? InnerClasses { get; private set; } = null;

    /// <summary>
    /// ルートクラス
    /// </summary>
    /// <returns>ルートクラス</returns>
    public Class? RootClass { get; private set; } = null;

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
            RootClass = rootClass,
            InnerClasses = innerClasses
        };
    }
}