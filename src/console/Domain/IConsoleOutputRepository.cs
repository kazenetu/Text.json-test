/// <summary>
/// コンソール出力リポジトリインターフェース
/// </summary>
public interface IConsoleOutputRepository
{
    /// <summary>
    /// コンソール出力する
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <returns>出力結果</returns>
    void Output(ClassesEntity classInstance);
}
