using System.Text;

/// <summary>
/// ファイル出力クラス
/// </summary>
public class WriteFile : IOutput
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="outputData">出力データ</param>
    /// <param name="filePath">出力ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>出力結果</returns>
    public bool OutputFile(String outputData, String filePath, String nameSpace = "")
    {
        // フォルダの存在確認とフォルダ作成
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        // ファイル出力
        using (FileStream fs = File.OpenWrite(filePath))
        {
            Byte[] info = new UTF8Encoding(true).GetBytes(outputData);
            fs.Write(info, 0, info.Length);
        }

        return false;
    }
}