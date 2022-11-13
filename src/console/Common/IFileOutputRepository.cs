/// <summary>
/// ファイル出力リポジトリインターフェース
/// </summary>
public interface IFileOutputRepository
{
    /// <summary>
    /// ファイル出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <param name="command">コマンドパラメータ</param>
    /// <returns>出力結果</returns>
    bool Output(Class classInstance, FileOutputCommand command);
}
