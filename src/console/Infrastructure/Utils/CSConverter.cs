using System.Text;
using Domain.Commands;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Utils;

/// <summary>
/// C#ソースコード変換クラス
/// </summary>
public class CSConverter : IConverter
{
    /// <summary>
    /// 集合クラスインスタンス フィールド
    /// </summary>
    private readonly ClassesEntity ClassInstance;

    /// <summary>
    /// パラメータ フィールド
    /// </summary>
    private readonly Dictionary<ParamKeys, string> Params;

    /// <summary>
    /// ルートクラスインスタンス フィールド
    /// </summary>
    private readonly ClassEntity RootClass;
    
    /// <summary>
    /// インデントのスペース数
    /// </summary>
    private int IndentSpaceCount = 2;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">パラメータ</param>
    /// <returns>インスタンス<returns>
    public CSConverter(ClassesEntity classInstance, Dictionary<ParamKeys, string> param)
    {
        ClassInstance = classInstance;
        Params = param;

        // 必須パラメータチェック
        if (classInstance?.RootClass is null) throw new NullReferenceException("RootClassが設定されていません");

        // インデントパラメータを設定
        if (Params.ContainsKey(ParamKeys.IndentSpaceCount)) 
            IndentSpaceCount = int.Parse(Params[ParamKeys.IndentSpaceCount]);
        
        // Rootを取得
        RootClass = ClassInstance.RootClass;
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">パラメータ</param>
    /// <returns>インスタンス<returns>
    public static IConverter Create(ClassesEntity classInstance, Dictionary<ParamKeys, string> param)
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
            result.Append('}');
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
        var levelSpace = new string('S', indentLevel * IndentSpaceCount).Replace("S", " ");
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
        var levelSpace = new string('S', indentLevel * IndentSpaceCount).Replace("S", " ");

        // プロパティ文字列作成
        result.Append($"{levelSpace}public {GetPropertyBaseString(property)}");
        result.AppendLine();

        return result.ToString();
    }

    /// <summary>
    /// プロパティValueObjectからプロパティ文字列のベースを作成して返す
    /// </summary>
    /// <param name="property">プロパティValueObject</param>
    /// <returns>プロパティ文字列のベース</returns>
    string GetPropertyBaseString(PropertyValueObject property)
    {
        // C#型取得
        var typeName = property.Type switch
        {
            {Kind: PropertyType.Kinds.String} => "string",
            {Kind: PropertyType.Kinds.Decimal} => "decimal",
            {Kind: PropertyType.Kinds.Bool} => "bool",
            {Kind: PropertyType.Kinds.Null }=> "object",
            {Kind: PropertyType.Kinds.Class, IsList: true }=> $"{property.PropertyTypeClassName}",
            {Kind: PropertyType.Kinds.Class, IsList: false }=> $"{property.PropertyTypeClassName}?",
            _ => throw new Exception($"{nameof(property)} has no type set"),
        };

        // デフォルト値
        var defualtValue = property.Type?.Kind switch
        {
            PropertyType.Kinds.String => "string.Empty",
            PropertyType.Kinds.Null => "string.Empty",
            _ => String.Empty,
        };

        // Listの場合はNullableにする
        if(property.Type!.IsList)
        {
            typeName = $"List<{typeName}>?";
        }

        // デフォルト文字列設定
        var defualt = string.Empty;
        if (defualtValue is not "")
        {
            defualt = $" = {defualtValue};";
        }

        // C#のプロパティを設定
        return $"{typeName} {property.Name} {{ set; get; }}{defualt}";
     }
}