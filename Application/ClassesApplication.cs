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
}
