using Domain.Entities;

namespace Infrastructure.Utils;

/// <summary>
/// ソースコンバーター
/// </summary>
internal static class SoruceConverter
{
    /// <summary>
    /// C#コードを作成する
    /// </summary>
    /// <param name="classInstance">Class集約エンティティ</param>
    /// <param name="namespaceName">名前空間</param>
    /// <returns>C#コード</returns>
    public static string ToCsCode(ClassesEntity classInstance, string? namespaceName = null)
    {
        // TODO コード作成
        return string.Empty;
    }
}