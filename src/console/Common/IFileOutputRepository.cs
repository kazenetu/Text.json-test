/// <summary>
/// ファイル出力リポジトリインターフェース
/// </summary>
public interface IFileOutputRepository
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="filePath">出力ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    void Initialize(string? rootPath = null, string? nameSpace = null);

    /// <summary>
    /// ファイル出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <returns>出力結果</returns>
    bool Output(Class classInstance);
}
