namespace Infrastructure;

/// <summary>
/// コンソール出力リポジトリクラス
/// </summary>
public class ConsoleOutputRepository : IConsoleOutputRepository
{
    /// <summary>
    /// コンソール出力する
    /// </summary>
    /// <param name="classInstance">集約エンティティ インスタンス</param>
    /// <returns>出力結果</returns>
    public void Output(ClassesEntity classInstance)
    {
        //必須パラメータチェック
        if (classInstance is null)
        {
            throw new Exception("classInstanceが設定されていません");
        };

        // コンソール出力
        Console.WriteLine(classInstance.GetClassString());
    }
}
