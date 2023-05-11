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
        // パラメータ設定
        var param = new Dictionary<string,string>();
        if(!string.IsNullOrEmpty(namespaceName))
        {
            param.Add(ParamKeys.CS_NameSpace, namespaceName);
        }

        var instance = CSConverter.Create(classInstance, param);
        return instance.Convert();
    }
}