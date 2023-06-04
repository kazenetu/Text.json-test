using Appplication.Commands;
using Appplication.Models;
using Domain.Commands;
using Domain.Interfaces;

namespace Appplication;

/// <summary>
/// クラス集約アプリケーション
/// </summary>
public class ClassesApplication
{
    /// <summary>
    /// Json解析リポジトリクラスインスタンス
    /// </summary>
    private IJsonRepository JsonRepository;

    /// <summary>
    /// ファイル出力リポジトリクラスインスタンス
    /// </summary>
    private IFileOutputRepository FileOutputRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="jsonRepository">Json解析リポジトリクラスインスタンス</param>
    /// <param name="fileOutputRepository">ファイル出力リポジトリクラスインスタンス</param>
    public ClassesApplication(IJsonRepository jsonRepository, IFileOutputRepository fileOutputRepository)
    {
        JsonRepository = jsonRepository;
        FileOutputRepository = fileOutputRepository;
    }

    /// <summary>
    /// Json文字列をC#ソースコードに変換しファイル作成する
    /// </summary>
    /// <param name="json">Json文字列</param>
    /// <param name="command">C#変換コマンド</param>
    /// <returns>処理結果</returns>
    public ConvertResutModel ConvertJsonToCSharp(string json, CSharpCommand command)
    {
        // パラメータチェック
        if (string.IsNullOrEmpty(json)) throw new Exception($"{nameof(json)} is null or Empty");
        if (command is null) throw new Exception($"{nameof(command)} is null");
        if (string.IsNullOrEmpty(command?.RootClassName)) throw new Exception($"{nameof(command.RootClassName)} is null");

        // Json文字列読み込み
        var classesEntity = JsonRepository.CreateClassEntityFromString(json, command.RootClassName);

        // ファイル名
        var fileName = $"{command.RootClassName}.cs";

        // ファイル出力
        if (FileOutputRepository.Output(classesEntity, new FileOutputCommand(command.RootPath, command.NameSpace)))
        {
            return new ConvertResutModel(true, fileName);
        }

        // 変換失敗
        return new ConvertResutModel(false, fileName);
    }
}
