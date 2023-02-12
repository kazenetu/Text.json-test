using System.Text;

/// <summary>
/// Objectのネストを含むJSONデシリアライズ用クラス
/// </summary>
public class InnerNestClassJson
{
    public class InnerClassA
    {
        public string propString { set; get; } = string.Empty;
        public decimal propNumber { set; get; }
        public string propDate { set; get; } = string.Empty;
        public bool propTrue { set; get; }
        public bool propFalse { set; get; }
        public object propNull { set; get; } = string.Empty;
        public List<decimal>? propArray { set; get; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"    propString:{propString}");
            result.AppendLine($"    propNumber:{propNumber}");
            result.AppendLine($"    propDate:{propDate}");
            result.AppendLine($"    propTrue:{propTrue}");
            result.AppendLine($"    propFalse:{propFalse}");
            result.AppendLine($"    propNull:{propNull}");
            if (propArray is null)
            {
                result.AppendLine($"    propArray:none...");
            }
            else
            {
                result.AppendLine($"    propArray:[{string.Join(",", propArray)}]");
            }

            return result.ToString();
        }
    }

    public class InnerClass
    {
        public InnerClassA? propSubObjct { set; get; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("  InnerClass...");
            result.AppendLine($"  propSubObjct:{propSubObjct}");

            return result.ToString();
        }
    }

    public InnerClass? propObjct { set; get; }

    public override string ToString()
    {
        var result = new StringBuilder();

        if (propObjct is null)
        {
            result.AppendLine($"propObjct is null");
            return result.ToString();
        }
        else
        {
            result.Append($"propObjct:{propObjct}");
        }

        return result.ToString();
    }
}