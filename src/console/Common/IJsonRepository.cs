/// <summary>
/// JSON読み込みリポジトリインターフェース
/// </summary>
public interface IJsonRepository
{
    /// <summary>
    /// JSONファイルを読み込んでClass情報を返す
    /// </summary>
    /// <param name="filePath">JSONファイル</param>
    /// <returns>Class文字列</returns>
    Class ReadFromFile(string filePath);

    /// <summary>
    /// JSON文字列を読み込んでClass情報を返す
    /// </summary>
    /// <param name="filePath">JSO文字列</param>
    /// <returns>Class文字列</returns>
    Class ReadFromString(string json);
}
