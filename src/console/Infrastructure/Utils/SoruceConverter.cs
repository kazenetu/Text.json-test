using System.Text;
using Domain.Entities;
using Domain.ValueObjects;

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
        // 必須パラメータチェック
        if (classInstance?.RootClass is null) throw new NullReferenceException("RootClassが設定されていません"); ;

        var rootClass = classInstance.RootClass;

        var result = new StringBuilder();

        if (!string.IsNullOrEmpty(namespaceName))
        {
            result.AppendLine($"namespace {namespaceName}");
            result.AppendLine("{");
        }

        // クラス生成
        result.Append(GetRootClassString(indentLevel));

        if (!string.IsNullOrEmpty(namespaceName))
        {
            result.Append("}");
        }

        return result.ToString();

        /// <summary>
        /// class文字列生成して返す
        /// </summary>
        /// <param name="indentLevel">インデントレベル</param>
        /// <returns>class文字列</returns>
        string GetRootClassString(int indentLevel = 0)
        {
            // ルートクラスを出力
            return GetClassString(rootClass, indentLevel);
        }

        /// <summary>
        /// クラスエンティティからclass文字列生成して返す
        /// </summary>
        /// <param name="classEntity">クラスエンティティインスタンス</param>
        /// <param name="indentLevel">インデントレベル</param>
        /// <returns>class文字列</returns>
        string GetClassString(ClassEntity classEntity, int indentLevel = 0)
        {
            var result = new StringBuilder();

            // インデント設定
            var levelSpace = new string('S', indentLevel).Replace("S", "  ");
            result.AppendLine($"{levelSpace}public class {classEntity.Name}");
            result.AppendLine($"{levelSpace}{{");

            if (classEntity == rootClass)
            {
                // インナークラスのクラス文字列作成
                foreach (var classInstance in classInstance.InnerClasses)
                {
                    result.AppendLine($"{GetClassString(classInstance, indentLevel + 1)}");
                }
            }

            // プロパティ文字列作成
            foreach (var property in classEntity.Properties)
            {
                result.Append($"{GetPropertyString(property, indentLevel + 1)}");
            }

            result.AppendLine($"{levelSpace}}}");

            return result.ToString();
        }

        /// <summary>
        /// プロパティValueObjectからプロパティ文字列を作成して返す
        /// </summary>
        /// <param name="property">プロパティValueObject</param>
        /// <param name="indentLevel">インデントレベル</param>
        /// <returns>プロパティ文字列</returns>
        string GetPropertyString(PropertyValueObject property, int indentLevel)
        {
            var result = new StringBuilder();

            // インデント設定
            var levelSpace = new string('S', indentLevel).Replace("S", "  ");

            // プロパティ文字列作成
            result.Append($"{levelSpace}public {property}");
            result.AppendLine();

            return result.ToString();
        }
    }
}