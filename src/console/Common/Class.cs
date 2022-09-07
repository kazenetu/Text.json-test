using System.Collections.ObjectModel;

  /// <summary>
  /// クラス情報
  /// </summary>
  public class Class
  {
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; init; }

    /// <summary>
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; init; }

    /// <summary>
    /// プロパティリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public List<Property> Properties { get; init; }

    /// <summary>
    /// インナークラスリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public List<Class> InnerClass { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private Class()
    {
        Name = string.Empty;
        Comment = string.Empty;
        Properties = new List<Property>();
        InnerClass = new List<Class>();
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <param name="properties">プロパティリスト</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static Class Create(string name, string comment, ReadOnlyCollection<Property> properties)
    {
      var result = new Class()
      {
        Name = name,
        Comment = comment
      };
      result.Properties.AddRange(properties);

      return result;
    }
  }