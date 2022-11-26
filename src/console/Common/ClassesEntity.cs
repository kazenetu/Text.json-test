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
    /// 非公開コンストラクタ
    /// </summary>
    private ClassesEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="innerClasses">インナークラスリスト</param>
    /// <returns>クラス集約エンティティ インスタンス</returns>
    public static ClassesEntity Create(IReadOnlyList<Class> innerClasses)
    {
        // 入力チェック
        if(!innerClasses.Any()) new ArgumentException($"{nameof(innerClasses)} count is zero");

        // インスタンスを返す
        return new ClassesEntity()
        {
            InnerClasses = innerClasses
        };
    }
}