/// <summary>
/// コンソール出力リポジトリクラス
/// </summary>
public class ConsoleOutputRepository : IConsoleOutputRepository
{
    /// <summary>
    /// コンソール出力する
    /// </summary>
    /// <param name="classInstance">クラスエンティティ</param>
    /// <returns>出力結果</returns>
    public void Output(Class classInstance)
    {
        //必須パラメータチェック
        if(classInstance is null){
            throw new Exception("classInstanceが設定されていません");
        };

        // コンソール出力
        Console.WriteLine(ClassEntityToStringUtil.GetClassString(classInstance, 0));
    }
}
