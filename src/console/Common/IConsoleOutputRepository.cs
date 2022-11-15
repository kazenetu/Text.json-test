/// <summary>
/// コンソール出力リポジトリインターフェース
/// </summary>
public interface IConsoleOutputRepository
{
    /// <summary>
    /// コンソール出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <returns>出力結果</returns>
    void Output(Class classInstance);
}
