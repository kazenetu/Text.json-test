using System.Text;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// クラス集約エンティティ
/// </summary>
public class ClassesEntity
{
    /// <summary>
    /// 非公開インナークラスリスト
    /// </summary>
    /// <returns>非公開インナークラスリスト</returns>
    public List<ClassEntity> innerClasses = new();

    /// <summary>
    /// 読み取り用インナークラスリスト
    /// </summary>
    /// <returns>読み取り用インナークラスリスト</returns>
    public IReadOnlyList<ClassEntity> InnerClasses
    { 
        get => innerClasses;  
    }

    /// <summary>
    /// ルートクラス
    /// </summary>
    /// <returns>ルートクラス</returns>
    private ClassEntity? rootClass = null;

    /// <summary>
    /// 読み取り用ルートクラス
    /// </summary>
    /// <returns>読み取り用ルートクラス</returns>
    public ClassEntity RootClass 
    {
        get {
            var newInstance = ClassEntity.Create(rootClass is null ? string.Empty: rootClass.Name);
            foreach(var prop in rootClass!.Properties)
            {
                newInstance.AddProperty(prop);
            }
            return newInstance;
        }
    }

    /// <summary>
    /// ルートクラスのクラス名を返す
    /// </summary>
    /// <returns>ルートクラス</returns>
    public string Name
    {
        get => rootClass?.Name ?? "RootClass";
    }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private ClassesEntity()
    {
    }

    /// <summary>
    /// ルートクラスのプロパティ追加
    /// </summary>
    /// <param name="Property">追加対象</param>
    public void AddRootProperty(PropertyValueObject Property)
    {
        // ルートクラス存在チェック
        if (rootClass is null) throw new Exception($"{nameof(rootClass)} is null");

        // プロパティ追加
        rootClass?.AddProperty(Property);
    }

    /// <summary>
    /// インナークラスの追加
    /// </summary>
    /// <param name="innerClass">追加対象</param>
    public void AddInnerClass(ClassEntity innerClass)
    {
        // 入力チェック
        if (innerClass is null) throw new ArgumentException($"{nameof(innerClass)} is null");

        // インナークラスリストに追加
        innerClasses.Add(innerClass!);
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="rootClassName">ルートクラス名</param>
    /// <returns>クラス集約エンティティ インスタンス</returns>
    public static ClassesEntity Create(string rootClassName)
    {
        // 入力チェック
        if (rootClassName is null) throw new ArgumentException($"{nameof(rootClassName)} is null");

        // インスタンスを返す
        var result = new ClassesEntity()
        {
            rootClass = ClassEntity.Create(rootClassName!)
        };

        return result;
    }

    #region class文字列作成

    /// <summary>
    /// class文字列生成して返す
    /// </summary>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    [Obsolete()]
    public string GetClassString(int indentLevel = 0)
    {
        // 必須パラメータチェック
        if (rootClass is null) throw new NullReferenceException("RootClassが設定されていません"); ;

        var result = string.Empty;

        // ルートクラスを出力
        result += GetClassString(rootClass, indentLevel);


        return result;
    }

    /// <summary>
    /// クラスエンティティからclass文字列生成して返す
    /// </summary>
    /// <param name="classEntity">クラスエンティティインスタンス</param>
    /// <param name="indentLevel">インデントレベル</param>
    /// <returns>class文字列</returns>
    [Obsolete()]
    private string GetClassString(ClassEntity classEntity, int indentLevel = 0)
    {
        var result = new StringBuilder();

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");
        result.AppendLine($"{levelSpace}public class {classEntity.Name}");
        result.AppendLine($"{levelSpace}{{");

        if (classEntity == rootClass)
        {
            // インナークラスのクラス文字列作成
            foreach (var classInstance in innerClasses)
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
    [Obsolete()]
    private string GetPropertyString(PropertyValueObject property, int indentLevel)
    {
        var result = new StringBuilder();

        // インデント設定
        var levelSpace = new string('S', indentLevel).Replace("S", "  ");

        // プロパティ文字列作成
        result.Append($"{levelSpace}public {property}");
        result.AppendLine();

        return result.ToString();
    }
    #endregion

}