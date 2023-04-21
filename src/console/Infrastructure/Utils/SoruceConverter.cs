using System.Text;
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
    /// <param name="indentLevel">インデントレベル</param>
    /// <param name="namespaceName">名前空間</param>
    /// <returns>C#コード</returns>
    public static string ToCsCode(ClassesEntity classInstance, int indentLevel = 0, string? namespaceName = null)
    {
        var result = new StringBuilder();

        if (!string.IsNullOrEmpty(namespaceName))
        {
            indentLevel++;
            result.AppendLine($"namespace {namespaceName}");
            result.AppendLine("{");
        }

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");

        // TODO クラス生成

        if (!string.IsNullOrEmpty(namespaceName))
        {
            result.AppendLine("}");
        }

        return result.ToString();
    }
}