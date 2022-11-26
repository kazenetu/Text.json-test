/// <summary>
/// クラス集約エンティティ
/// </summary>
public class ClassesEntity
{
    /// <summary>
    /// インナークラスリスト
    /// </summary>
    /// <returns>インナークラスリスト</returns>
    public ReadOnlyList<Class> InnerClasses { get; private set; } = null;

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
    public ClassesEntity Create(ReadOnlyList<Class> innerClasses)
    {
        // 入力チェック
        if(innerClasses is null) new ArgumentNullException($"{innerClasses} is null");
        if(!innerClasses.Any()) new ArgumentException($"{innerClasses} is zero");

        // パラメータ設定
        InnerClasses = innerClasses;
    }
}