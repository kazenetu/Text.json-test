using System.Text;
using System.Text.Json;

internal class Program
{
    /// <summary>
    /// エントリメソッド
    /// </summary>    
    private static void Main(string[] args)
    {
        var json = string.Empty;

        // 単純なJSON文字列作成
        json = @"{
            ""propString"" : ""string""
            , ""propNumber"":10
            , ""propDate"":""2022/01/01 10:11:12""
            , ""propTrue"":true
            , ""propFalse"":false
            , ""propNull"":null
            , ""propArray"":[1,2,3]
        }";

        Console.WriteLine("--単純なJSON文字列--");
        ShowJsonResult(json);
        // 出力結果：
        // --単純なJSON文字列--
        // String propString
        // Number propNumber
        // DateTime propDate
        // True propTrue
        // False propFalse
        // Null propNull
        // Array(Number) propArray

        var simpleJsonResult = JsonSerializer.Deserialize<SimpleJson>(json);
        Console.WriteLine("----Deserialize結果----");
        Console.WriteLine(simpleJsonResult?.ToString());
        Console.WriteLine("--------------------");
        //出力結果：
        // ----Deserialize結果----
        // propString:string
        // propNumber:10
        // propDate:2022/01/01 10:11:12
        // propTrue:True
        // propFalse:False
        // propNull:
        // propArray:[1,2,3]

        // Objectが含まれるJSON文字列作成
        json = @"{
            ""propObjct"" : 
            {
                ""propObjString"":""propObjString""
            }
            , ""propNumber"":10
        }";

        Console.WriteLine("--Objectが含まれるJSON文字列--");
        ShowJsonResult(json);
        Console.WriteLine("--------------------");
        // 出力結果：
        // --Objectが含まれるJSON文字列--
        // Object propObjct
        //   String propObjString
        // Number propNumber

        // Object配列が含まれるJSON文字列作成
        json = @"{
            ""propObjcts"" : 
            [
                    {
                        ""propObjString"":""propObjString1""
                    }
                    ,{
                        ""propObjString"":""propObjString2""
                    }
            ]
        }";

        Console.WriteLine("--Object配列が含まれるJSON文字列--");
        ShowJsonResult(json);
        Console.WriteLine("--------------------");
        // 出力結果：
        // --Object配列が含まれるJSON文字列--
        // Array(Object) propObjcts
        //   String propObjString

        // Objectのネストを含むJSON文字列作成
        json = @"{
            ""propObjct"" : 
            {
                ""propSubObjct"":
                {
                    ""propString"" : ""string""
                    , ""propNumber"":10
                    , ""propDate"":""2022/01/01 10:11:12""
                    , ""propTrue"":true
                    , ""propFalse"":false
                    , ""propNull"":null
                    , ""propArray"":[1,2,3]
                }
            }
        }";
        Console.WriteLine("--Objectのネストを含むJSON文字列作成--");
        ShowJsonResult(json);
        Console.WriteLine("--------------------");
        // 出力結果：
        // --Objectのネストを含むJSON文字列作成--
        // Object propObjct
        //   Object propSubObjct
        //     String propString
        //     Number propNumber
        //     DateTime propDate
        //     True propTrue
        //     False propFalse
        //     Null propNull
        //     Array(Number) propArray

    }

    private static void ShowJsonResult(string json)
    {
        var result = new JsonParser(json);
        Console.WriteLine(result.Result);
    }
}
