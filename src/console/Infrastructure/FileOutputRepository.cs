using System.Text;
using Domain.Interfaces;

namespace Infrastructure;

/// <summary>
/// ファイル出力リポジトリクラス
/// </summary>
public class FileOutputRepository : IFileOutputRepository
{
    /// <summary>
    /// ファイルを出力する
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <param name="command">コマンドパラメータ</param>
    /// <returns>出力結果</returns>
    public bool Output(ClassesEntity classInstance, FileOutputCommand command)
    {
        //必須パラメータチェック
        if (classInstance is null) return false;
        if (command.RootPath is null) return false;

        // フォルダの存在確認とフォルダ作成
        if (!Directory.Exists(command.RootPath))
        {
            Directory.CreateDirectory(command.RootPath);
        }
        var filePath = Path.Combine(command.RootPath, $"{classInstance.Name}.cs");

        var fileData = new StringBuilder();
        var nameSpaceNone = string.IsNullOrEmpty(command.NameSpace);
        var initialSpaceIndex = 0;

        if (!nameSpaceNone)
        {
            initialSpaceIndex = 1;
            fileData.AppendLine($"namespace {command.NameSpace}");
            fileData.AppendLine("{");
        }
        fileData.Append(classInstance.GetClassString(initialSpaceIndex));
        if (!nameSpaceNone)
        {
            fileData.AppendLine("}");
        }

        // ファイル出力
        File.WriteAllText(filePath, fileData.ToString());

        return true;
    }
}