using System.Text;

/// <summary>
/// ファイル出力クラス
/// </summary>
public class WriteFile : IOutput
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <param name="filePath">出力ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>出力結果</returns>
    public bool Output(Class classInstance, String? rootPath= null, String? nameSpace = null)
    {
        // フォルダの存在確認とフォルダ作成
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        var filePath = Path.Combine(rootPath,$"{classInstance.Name}.cs");

        // ファイル出力
        using (FileStream fs = File.OpenWrite(filePath))
        {
            Byte[] info = new UTF8Encoding(true).GetBytes(classInstance.ToString(0));
            fs.Write(info, 0, info.Length);
        }

        return false;
    }
}