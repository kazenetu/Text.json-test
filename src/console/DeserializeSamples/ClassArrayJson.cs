using System.Text;

/// <summary>
/// Object配列が含まれるJSONデシリアライズ用クラス
/// </summary>
public class ClassArrayJson
{
    public class InnerClass
    {
        public string propObjString{set; get;} = string.Empty;

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append("  InnerClass...");
            result.AppendLine($"  propObjString:{propObjString}");

            return result.ToString();
        }
    }

    public List<InnerClass>? propObjcts{set; get;}

    public override string ToString()
    {
        var result = new StringBuilder();

        if (propObjcts is null)
        {
            result.AppendLine($"propObjcts is null");
            return result.ToString();
        }

        result.AppendLine($"propObjcts");
        foreach (var item in propObjcts)
        {
            result.Append(item);
        }

        return result.ToString();
    }
}