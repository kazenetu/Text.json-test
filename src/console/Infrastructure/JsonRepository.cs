using System.IO;

/// <summary>
/// JSON読み込みリポジトリ
/// </summary>
public class JsonRepository : IJsonRepository
{
    /// <summary>
    /// JSONファイルを読み込んでClass情報を返す
    /// </summary>
    /// <param name="filePath">JSONファイル</param>
    /// <returns>Classエンティティ</returns>
    public Class CreateClassEntityFromFile(string filePath)
    {
        var result = string.Empty;

        // ファイル読み込み
        result = File.ReadAllText(filePath);

        // 文字列として読み取り
        return CreateClassEntityFromString(result);
    }

    /// <summary>
    /// JSON文字列を読み込んでClass情報を返す
    /// </summary>
    /// <param name="json">JSO文字列</param>
    /// <returns>Classエンティティ</returns>
    public Class CreateClassEntityFromString(string json)
    {
        // HACK クラスエンティティを作成
        return new Class();

    }
}