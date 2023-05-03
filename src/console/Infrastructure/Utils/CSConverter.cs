using Domain.Entities;

namespace Infrastructure.Utils;

/// <summary>
/// C#ソースコード変換クラス
/// </summary>
public class CSConverter:IConverter
{
    private ClassesEntity ClassInstance;
    private Dictionary<string,string> Params;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">パラメータ</param>
    /// <returns>インスタンス<returns>
    public CSConverter(ClassesEntity classInstance, Dictionary<string,string> param)
    {
        ClassInstance = classInstance;
        Params = param;
    }

    /// <summary>
    /// コード変更処理
    /// </summary>
    /// <returns>ソースコード文字列</returns>
    public string Convert()
    {
        throw new NotImplementedException();
    }
}