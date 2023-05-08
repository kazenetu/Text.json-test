using System.Text;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Utils;

/// <summary>
/// C#ソースコード変換クラス
/// </summary>
public class CSConverter:IConverter
{
    private ClassesEntity ClassInstance;
    private Dictionary<string,string> Params;
    private ClassEntity RootClass;

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

        // 必須パラメータチェック
        if (classInstance?.RootClass is null) throw new NullReferenceException("RootClassが設定されていません");

        // Rootを取得
        RootClass = ClassInstance.RootClass;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">パラメータ</param>
    /// <returns>インスタンス<returns>
    public static IConverter Create(ClassesEntity classInstance, Dictionary<string, string> param)
    {

        return new CSConverter(classInstance, param);
    }

    /// <summary>
    /// コード変更処理
    /// </summary>
    /// <returns>ソースコード文字列</returns>
    public string Convert()
    {
        // 名前空間取得
        var namespaceName = string.Empty;
        if (Params.ContainsKey(ParamKeys.CS_NameSpace)) namespaceName = Params[ParamKeys.CS_NameSpace];

        var result = new StringBuilder();

        // インデントレベル
        var indentLevel = 0;

        if (!string.IsNullOrEmpty(namespaceName))
        {
            result.AppendLine($"namespace {namespaceName}");
            result.AppendLine("{");
            indentLevel++;
        }

        // クラス生成
        result.Append(GetRootClassString(indentLevel));

        if (!string.IsNullOrEmpty(namespaceName))
        {
            result.Append("}");
        }

        return result.ToString();
   }

    /// <summary>
    /// class文字列生成して返す
    /// </summary>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    string GetRootClassString(int indentLevel = 0)
    {
        // ルートクラスを出力
        return GetClassString(RootClass, indentLevel);
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

        if (classEntity == RootClass)
        {
            // インナークラスのクラス文字列作成
            foreach (var classInstance in ClassInstance.InnerClasses)
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