using System.Text;
using System.Text.Json.Serialization;

/// <summary>
/// 単純なJSON文字列デシリアライズ用クラス
/// </summary>
public class SimpleJson
{
    [JsonPropertyName("prop_string")]
    public string propString { set; get; } = string.Empty;
    public decimal propNumber { set; get; }
    [JsonPropertyName("prop_Date")]
    public string propDate { set; get; } = string.Empty;
    public bool propTrue { set; get; }
    public bool propFalse { set; get; }
    public object propNull { set; get; } = string.Empty;
    public List<decimal>? propArray { set; get; }

    public override string ToString()
    {
        var result = new StringBuilder();

        result.AppendLine($"propString:{propString}");
        result.AppendLine($"propNumber:{propNumber}");
        result.AppendLine($"propDate:{propDate}");
        result.AppendLine($"propTrue:{propTrue}");
        result.AppendLine($"propFalse:{propFalse}");
        result.AppendLine($"propNull:{propNull}");
        if (propArray is null)
        {
            result.AppendLine("propArray:none");
        }
        else
        {
            result.AppendLine($"propArray:[{string.Join(",", propArray)}]");
        }

        return result.ToString();
    }
}