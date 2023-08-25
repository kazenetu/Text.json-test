using Appplication.Commands;
using Appplication.Models;
using Domain.Commands;
using Domain.Interfaces;

namespace Appplication;

/// <summary>
/// クラス集約アプリケーション
/// </summary>
public class ClassesApplication : ApplicationBase
{
    /// <summary>
    /// Json解析リポジトリクラスインスタンス
    /// </summary>
    private IJsonRepository? JsonRepository;

    /// <summary>
    /// ファイル出力リポジトリクラスインスタンス
    /// </summary>
    private IFileOutputRepository? FileOutputRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="jsonRepository">Json解析リポジトリクラスインスタンス</param>
    /// <param name="fileOutputRepository">ファイル出力リポジトリクラスインスタンス</param>
    public ClassesApplication(IJsonRepository jsonRepository, IFileOutputRepository fileOutputRepository) : base()
    {
        JsonRepository = jsonRepository;
        FileOutputRepository = fileOutputRepository;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="jsonRepository">Json解析リポジトリクラスインスタンス</param>
    /// <param name="fileOutputRepository">ファイル出力リポジトリクラスインスタンス</param>
    public ClassesApplication() : base()
    {
    }

    /// <summary>
    /// Json文字列をC#ソースコードに変換しファイル作成する
    /// </summary>
    /// <param name="json">Json文字列</param>
    /// <param name="command">C#変換コマンド</param>
    /// <returns>処理結果</returns>
    public ConvertResultModel ConvertJsonToCSharp(string json, CSharpCommand command)
    {
        // パラメータチェック
        if (string.IsNullOrEmpty(json)) throw new Exception($"{nameof(json)} is null or Empty");
        if (command is null) throw new Exception($"{nameof(command)} is null");
        if (string.IsNullOrEmpty(command?.RootClassName)) throw new Exception($"{nameof(command.RootClassName)} is null");

        // インターフェイスのnullチェック
        if(JsonRepository is null) throw new Exception($"{nameof(JsonRepository)} is null");
        if(FileOutputRepository is null) throw new Exception($"{nameof(FileOutputRepository)} is null");

        // Json文字列読み込み
        var classesEntity = JsonRepository.CreateClassEntityFromString(json, command.RootClassName);

        // ファイル出力
        var CommandParams = new Dictionary<ParamKeys, string>
        {
            {ParamKeys.CS_NameSpace, command.NameSpace},
        };
        var fileCommand = new FileOutputCommand(command.RootPath,OutputLanguageType.CS,command.IndentSpaceCount, CommandParams);
        var result = FileOutputRepository.OutputResult(classesEntity, fileCommand);

        if (result.Success)
        {
            // 変換成功
            return new ConvertResultModel(true, result.FileName, result.SourceCode);
        }

        // 変換失敗
        return new ConvertResultModel(false, result.FileName);
    }
}
