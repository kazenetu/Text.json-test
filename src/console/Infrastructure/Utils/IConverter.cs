using Domain.Entities;

namespace Infrastructure.Utils;

/// <summary>
/// ソース変換インターフェース
/// </summary>
public interface IConverter
{
    /// <summary>
    /// コード変更処理
    /// </summary>
    /// <returns>ソースコード文字列</returns>
    string Convert();
}