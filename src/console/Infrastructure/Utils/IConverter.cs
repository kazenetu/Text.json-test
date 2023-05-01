using Domain.Entities;

namespace Infrastructure.Utils;

/// <summary>
/// ソース変換インターフェース
/// </summary>
interface IConverter
{
    /// <summary>
    /// コード変更処理
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">オプションリスト</param>
    /// <returns>ソースコード文字列</returns>
    string Convert(ClassesEntity classInstance, Dictionary<string, string> param);
}