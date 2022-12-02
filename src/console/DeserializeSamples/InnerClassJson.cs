using System.Text;
/// <summary>
/// Objectが含まれるJSON文字列デシリアライズ用クラス
/// </summary>
public class InnerClassJson
{
    public class InnerClass
    {
        public string propObjString{set; get;} = string.Empty;

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"InnerClass");
            result.AppendLine($"  propObjString:{propObjString}");

            return result.ToString();
        }
    }

    public InnerClass? propObjct{set; get;}
    public decimal propNumber{set; get;}

    public override string ToString()
    {
        var result = new StringBuilder();

        result.Append($"propObjct:{propObjct}");
        result.AppendLine($"propNumber:{propNumber}");

        return result.ToString();
    }
}
