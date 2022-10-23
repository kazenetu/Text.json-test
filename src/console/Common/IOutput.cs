/// <summary>
/// 出力インターフェース
/// </summary>
public interface IOutput
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="outputData">出力データ</param>
    /// <param name="filePath">出力ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>出力結果</returns>
    bool OutputFile(String outputData, String filePath, String nameSpace = "");
}