using System.Text;

namespace Infrastructure.Extensions;

/// <summary>
/// 拡張クラス ネーミングルール
/// </summary>
public static class NamingRules
{
    /// <summary>
    /// C#規約に変換する
    /// </summary>
    /// <param name="src">対象文字列</param>
    /// <returns>UpperCaseの文字列</returns>
    public static string ToCSharpNaming(this string src)
    {
        var result = ToUpperCase(src);
        if(src.IndexOf("_") >= 0)
        {
            var keywords = new StringBuilder();
            foreach(var keyword in src.Split("_"))
            {
                keywords.Append(ToUpperCase(keyword));
            }
            result = keywords.ToString();
        }
        return result;
    }

    /// <summary>
    /// UpperCaseに変換する
    /// </summary>
    /// <param name="src">対象文字列</param>
    /// <returns>頭文字を大文字にした文字列</returns>
    private static string ToUpperCase(string src)
    {
        if(string.IsNullOrEmpty(src))
            return string.Empty;

        var result = new StringBuilder();
        result.Append(src.Substring(0,1).ToUpper());
        if(src.Length > 1) 
                result.Append(src.Substring(1, src.Length - 1));
        return result.ToString();
    }
}