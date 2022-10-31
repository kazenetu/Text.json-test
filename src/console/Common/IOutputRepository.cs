/// <summary>
/// 出力リポジトリインターフェース
/// </summary>
public interface IOutputRepository
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <param name="filePath">出力ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>出力結果</returns>
    bool Output(Class classInstance, string? rootPath = null, string? nameSpace = null);
}
