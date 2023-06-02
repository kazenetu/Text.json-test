using Appplication.Commands;
using Appplication.Models;

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
        // TODO 実装
        throw new System.Exception("未実装");
    }
}
