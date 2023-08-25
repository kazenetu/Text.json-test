using System.Text;
using Domain.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Results;

namespace Infrastructure;

/// <summary>
/// ファイル出力リポジトリクラス
/// </summary>
public class FileOutputRepository : IFileOutputRepository
{
    /// <summary>
    /// ファイル出力する
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <param name="command">コマンドパラメータ</param>
    /// <returns>出力結果</returns>
    public FileOutputResult OutputResult(ClassesEntity classInstance, FileOutputCommand command)
    {
        //必須パラメータチェック
        if (classInstance is null) return new FileOutputResult(false, string.Empty, string.Empty);
        if (command.RootPath is null) return new FileOutputResult(false, string.Empty, string.Empty);

        // フォルダの存在確認とフォルダ作成
        if (!Directory.Exists(command.RootPath))
        {
            Directory.CreateDirectory(command.RootPath);
        }

        // 拡張子取得
        var ext = command.LanguageType switch
        {
            OutputLanguageType.CS => "cs",
            _ => throw new Exception("ext error")
        };

        // ファイルパス作成
        var filePath = Path.Combine(command.RootPath, $"{classInstance.Name}.{ext}");

        // ソースコードを作成
        var sourceCode = command.LanguageType switch
        {
            OutputLanguageType.CS => GetCSCode(),
            _ => throw new Exception("ext error")
        };

        // ファイル出力
        File.WriteAllText(filePath, sourceCode);

        return new FileOutputResult(true, filePath, sourceCode);

        // C#ソースコードを取得
        string GetCSCode()
        {
            // 名前空間
            var nameSpace = string.Empty;
            if (command.Params.ContainsKey(ParamKeys.CS_NameSpace))
            {
                nameSpace = command.Params[ParamKeys.CS_NameSpace];
            }

            var initialSpaceIndex = 0;
            // 名前空間が設定していない場合はインデントを調整する
            if (nameSpace == string.Empty)
            {
                initialSpaceIndex = 1;
            }

            // Entityからソースコードの変換
            return Utils.SoruceConverter.ToCsCode(classInstance, initialSpaceIndex, nameSpace, command.IndentSpaceCount);
        }
    }
}