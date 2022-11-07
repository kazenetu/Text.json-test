using System.Text;

/// <summary>
/// ファイル出力リポジトリクラス
/// </summary>
public class FileOutputRepository : IOutputRepository
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
        //必須パラメータチェック
        if(classInstance is null) return false;
        if(rootPath is null) return false;

        // フォルダの存在確認とフォルダ作成
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        var filePath = Path.Combine(rootPath,$"{classInstance.Name}.cs");

        var fileData = new StringBuilder();
        var nameSpaceNone = string.IsNullOrEmpty(nameSpace);
        var initialSpaceIndex = 0;

        if(!nameSpaceNone)
        {
            initialSpaceIndex = 1;
            fileData.AppendLine($"namespace {nameSpace}");
            fileData.AppendLine("{");
        }
        fileData.Append(ClassEntityToStringUtil.GetClassString(classInstance, initialSpaceIndex));
        if(!nameSpaceNone)
        {
            fileData.AppendLine("}");
        }


        // ファイル出力
        using (FileStream fs = File.OpenWrite(filePath))
        {
            Byte[] info = new UTF8Encoding(true).GetBytes(fileData.ToString());
            fs.Write(info, 0, info.Length);
        }

        return true;
    }
}