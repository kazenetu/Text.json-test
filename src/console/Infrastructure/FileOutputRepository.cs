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
            OutputLanguageType.KT => "kt",
            _ => throw new Exception("ext error")
        };

        // 固定プレフィックス
        var prefix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Prefix))
        {
            prefix = ToFirstUppers(command.Params[ParamKeys.Prefix]);
        }

        // 固定サフィックス
        var suffix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Suffix))
        {
            suffix = ToFirstUppers(command.Params[ParamKeys.Suffix]);
        }

        // ファイルパス作成
        var filePath = Path.Combine(command.RootPath, $"{prefix}{classInstance.Name}{suffix}.{ext}");

        // ソースコードを作成
        var sourceCode = command.LanguageType switch
        {
            OutputLanguageType.CS => GetCSCode(classInstance, command),
            OutputLanguageType.KT => GetKTCode(classInstance,command),
            _ => throw new Exception("ext error")
        };

        // ファイル出力
        File.WriteAllText(filePath, sourceCode);

        return new FileOutputResult(true, filePath, sourceCode);
    }

    /// <summary>
    /// C# ソースコード生成
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <param name="command">コマンドパラメータ</param>
    /// <returns>ソースコード</returns>
    private static string GetCSCode(ClassesEntity classInstance, FileOutputCommand command)
    {
        // 名前空間
        var nameSpace = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.CS_NameSpace))
        {
            nameSpace = command.Params[ParamKeys.CS_NameSpace];
        }

        // 固定プレフィックス
        var prefix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Prefix))
        {
            prefix = ToFirstUppers(command.Params[ParamKeys.Prefix]);
        }

        // 固定サフィックス
        var suffix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Suffix))
        {
            suffix = ToFirstUppers(command.Params[ParamKeys.Suffix]);
        }

        var initialSpaceIndex = 0;
        // 名前空間が設定していない場合はインデントを調整する
        if (nameSpace == string.Empty)
        {
            initialSpaceIndex = 1;
        }

        // Entityからソースコードの変換
        return Utils.SoruceConverter.ToCsCode(classInstance, initialSpaceIndex, nameSpace, command.IndentSpaceCount, prefix, suffix);
    }

    /// <summary>
    /// Kotlin ソースコード生成
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <param name="command">コマンドパラメータ</param>
    /// <returns>ソースコード</returns>
    private static string GetKTCode(ClassesEntity classInstance, FileOutputCommand command)
    {
        // パッケージ名
        var packageName = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.KT_Package))
        {
            packageName = command.Params[ParamKeys.KT_Package];
        }

        // 固定プレフィックス
        var prefix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Prefix))
        {
            prefix = command.Params[ParamKeys.Prefix];
        }

        // 固定サフィックス
        var suffix = string.Empty;
        if (command.Params.ContainsKey(ParamKeys.Suffix))
        {
            suffix = command.Params[ParamKeys.Suffix];
        }

        // Entityからソースコードの変換
        return Utils.SoruceConverter.ToKtCode(classInstance,  packageName, command.IndentSpaceCount, prefix, suffix);
    }

    /// <summary>
    /// スペース単位で頭文字を大文字に設定
    /// </summary>
    /// <param name="src">対象文字列</param>
    /// <returns>頭文字を大文字にした文字列</returns>
    private static string ToFirstUppers(string src)
    {
        var result = ToFirstUpper(src);
        if(src.IndexOf("_") >= 0)
        {
            var keywords = new StringBuilder();
            foreach(var keyword in src.Split("_"))
            {
                keywords.Append(ToFirstUpper(keyword));
            }
            result = keywords.ToString();
        }
        return result;
    }

    /// <summary>
    /// 頭文字を大文字に設定
    /// </summary>
    /// <param name="src">対象文字列</param>
    /// <returns>頭文字を大文字にした文字列</returns>
    private static string ToFirstUpper(string src)
    {
        if(string.IsNullOrEmpty(src))
            return string.Empty;

        var result = new StringBuilder();
        result.Append(src.Substring(0,1).ToUpper());
        if(src.Length > 1) 
                result.Append(src.Substring(1, src.Length - 1));
        return result.ToString();
    }
}