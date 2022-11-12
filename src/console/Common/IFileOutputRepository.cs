/// <summary>
/// ファイル出力リポジトリインターフェース
/// </summary>
public interface IFileOutputRepository
{
    /// <summary>
    /// ファイル出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <param name="args">パラメータリスト</param>
    /// <returns>出力結果</returns>
    bool Output(Class classInstance, List<(string key,string value)> args);
}
