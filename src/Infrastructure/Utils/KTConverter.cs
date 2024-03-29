using System.Text;
using Domain.Commands;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Extensions;

namespace Infrastructure.Utils;

/// <summary>
/// Kotlinソースコード変換クラス
/// </summary>
public class KTConverter : IConverter
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
    /// 固定プレフィックス
    /// </summary>
    private readonly string Prefix = string.Empty;

    /// <summary>
    /// 固定サフィックス
    /// </summary>
    private readonly string Suffix = string.Empty;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="classInstance">集約クラス</param>
    /// <param name="param">パラメータ</param>
    /// <returns>インスタンス<returns>
    public KTConverter(ClassesEntity classInstance, Dictionary<ParamKeys, string> param)
    {
        ClassInstance = classInstance;
        Params = param;

        // 必須パラメータチェック
        if (classInstance?.RootClass is null) throw new NullReferenceException("RootClassが設定されていません");

        // 固定プレフィックスを設定
        if (Params.ContainsKey(ParamKeys.Prefix))
            Prefix = Params[ParamKeys.Prefix];

        // 固定サフィックスを設定
        if (Params.ContainsKey(ParamKeys.Suffix))
            Suffix = Params[ParamKeys.Suffix];


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

        return new KTConverter(classInstance, param);
    }

    /// <summary>
    /// コード変更処理
    /// </summary>
    /// <returns>ソースコード文字列</returns>
    public string Convert()
    {
        // パッケージ名取得
        var packageName = string.Empty;
        if (Params.ContainsKey(ParamKeys.KT_Package)) packageName = Params[ParamKeys.KT_Package];

        var result = new StringBuilder();

        if (!string.IsNullOrEmpty(packageName))
        {
            result.AppendLine($"package {packageName}");
            result.AppendLine();
        }

        // インポート追加
        result.AppendLine("import kotlinx.serialization.Serializable");
        result.AppendLine();

        // メイン データクラス生成
        result.Append(GetRootClassString());
        if (ClassInstance.InnerClasses.Any())
            result.AppendLine();

        // インナークラスに相当する データクラス生成
        foreach (var classInstance in ClassInstance.InnerClasses.Reverse())
        {
            result.AppendLine();
            result.AppendLine(GetClassString(classInstance));
        }

        return result.ToString();
    }

    /// <summary>
    /// class文字列生成して返す
    /// </summary>
    /// <returns>class文字列</returns>
    string GetRootClassString()
    {
        // ルートクラスを出力
        return GetClassString(RootClass);
    }

    /// <summary>
    /// クラスエンティティからclass文字列生成して返す
    /// </summary>
    /// <param name="classEntity">クラスエンティティインスタンス</param>
    /// <returns>class文字列</returns>
    string GetClassString(ClassEntity classEntity)
    {
        var result = new StringBuilder();

        // パラメータマルチライン設定
        var isMultiLineParams = classEntity.Properties.Count > 2;

        // アノテーション追加
        result.AppendLine("@Serializable");

        // データクラス生成
        result.Append($"data class {Prefix}{classEntity.Name}{Suffix}");
        result.Append($" (");
        if (isMultiLineParams) result.Append($" {Environment.NewLine}");

        // プロパティ文字列作成
        var propertyLastIndex = classEntity.Properties.Count - 1;
        foreach (var property in classEntity.Properties)
        {
            if (isMultiLineParams) result.Append("    ");
            result.Append(GetPropertyString(property));
            if (classEntity.Properties[index: propertyLastIndex] != property)
            {
                result.Append(',');
            }
            if (isMultiLineParams) result.Append($" {Environment.NewLine}");
        }
        result.Append(')');

        return result.ToString();
    }

    /// <summary>
    /// プロパティValueObjectからプロパティ文字列を作成して返す
    /// </summary>
    /// <param name="property">プロパティValueObject</param>
    /// <returns>プロパティ文字列</returns>
    string GetPropertyString(PropertyValueObject property)
    {
        return GetPropertyBaseString(property);
    }

    /// <summary>
    /// プロパティValueObjectからプロパティ文字列のベースを作成して返す
    /// </summary>
    /// <param name="property">プロパティValueObject</param>
    /// <returns>プロパティ文字列のベース</returns>
    private string GetPropertyBaseString(PropertyValueObject property)
    {
        // Kotlin型取得
        var typeName = property.Type switch
        {
            { Kind: PropertyType.Kinds.String } => "String",
            { Kind: PropertyType.Kinds.Decimal } => "Double",
            { Kind: PropertyType.Kinds.Bool } => "Boolean",
            { Kind: PropertyType.Kinds.Null } => "String",
            { Kind: PropertyType.Kinds.Class, IsList: true } => $"{Prefix}{property.PropertyTypeClassName}{Suffix}",
            { Kind: PropertyType.Kinds.Class, IsList: false } => $"{Prefix}{property.PropertyTypeClassName}{Suffix}",
            _ => throw new Exception($"{nameof(property)} has no type set"),
        };

        // Listの場合はNullableにする
        if (property.Type!.IsList)
        {
            typeName = $"List<{typeName}>";
        }

        // Kotlinのプロパティを設定
        var codeProprty = property.Name.ToKotlinPrppertyNaming();

        // アノテーション追加確認
        var annotation = string.Empty;
        if (codeProprty != property.Name)
        {
            annotation = $"@SerialName(\"{property.Name}\") ";
        }

        return $"{annotation}val {codeProprty}: {typeName}";
    }
}