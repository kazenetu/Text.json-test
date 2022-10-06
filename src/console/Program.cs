using System.Text;
using System.Text.Json;

internal class Program
{
    /// <summary>
    /// エントリメソッド
    /// </summary>    
    private static void Main(string[] args)
    {
        // 単純なJSON文字列作成
        var simpleJson = @"{
            ""propString"" : ""string""
            , ""propNumber"":10
            , ""propDate"":""2022/01/01 10:11:12""
            , ""propTrue"":true
            , ""propFalse"":false
            , ""propNull"":null
            , ""propArray"":[1,2,3]
        }";

        Console.WriteLine("--単純なJSON文字列--");
        ShowJsonResult(simpleJson);
        // 出力結果：
        // --単純なJSON文字列--
        // String propString
        // Number propNumber
        // DateTime propDate
        // True propTrue
        // False propFalse
        // Null propNull
        // Array(Number) propArray

        var simpleJsonResult = JsonSerializer.Deserialize<SimpleJson>(simpleJson);
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

        Console.WriteLine("----Class解析結果----");
        Console.WriteLine(Class.JsonParse(simpleJson).ToString(0));
        Console.WriteLine("--------------------");


        // --------------------------------------------------------------
        // Objectが含まれるJSON文字列作成
        var innerClassJson = @"{
            ""propObjct"" : 
            {
                ""propObjString"":""propObjString""
            }
            , ""propNumber"":10
        }";

        Console.WriteLine("--Objectが含まれるJSON文字列--");
        ShowJsonResult(innerClassJson);
        // 出力結果：
        // --Objectが含まれるJSON文字列--
        // Object propObjct
        //   String propObjString
        // Number propNumber

        var innerClassJsonResult = JsonSerializer.Deserialize<InnerClassJson>(innerClassJson);
        Console.WriteLine("----Deserialize結果----");
        Console.WriteLine(innerClassJsonResult?.ToString());
        Console.WriteLine("--------------------");
        // 出力結果：
        // ----Deserialize結果----
        // propObjct:InnerClass
        //   propObjString:propObjString
        // propNumber:10        

        // --------------------------------------------------------------
        // Object配列が含まれるJSON文字列作成
        var arrayJson = @"{
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
        ShowJsonResult(arrayJson);
        // 出力結果：
        // --Object配列が含まれるJSON文字列--
        // Array(Object) propObjcts
        //   String propObjString

        var classArrayJson = JsonSerializer.Deserialize<ClassArrayJson>(arrayJson);
        Console.WriteLine("----Deserialize結果----");
        Console.WriteLine(classArrayJson?.ToString());
        Console.WriteLine("--------------------");
        // 出力結果：
        // ----Deserialize結果----
        // propObjcts
        //   InnerClass...  propObjString:propObjString1
        //   InnerClass...  propObjString:propObjString2

        // --------------------------------------------------------------
        // Objectのネストを含むJSON文字列作成
        var innerNestJson = @"{
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
        ShowJsonResult(innerNestJson);
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

        var innerNestClassJson = JsonSerializer.Deserialize<InnerNestClassJson>(innerNestJson);
        Console.WriteLine("----Deserialize結果----");
        Console.WriteLine(innerNestClassJson?.ToString());
        Console.WriteLine("--------------------");
        // 出力結果：
        // ----Deserialize結果----
        // propObjct:  InnerClass...
        //   propSubObjct:    propString:string
        //     propNumber:10
        //     propDate:2022/01/01 10:11:12
        //     propTrue:True
        //     propFalse:False
        //     propNull:
        //     propArray:[1,2,3]
    }

    /// <summary>
    /// 構想解析結果の表示
    /// </summary>    
    /// <param name="json">JSON文字列</param>
    private static void ShowJsonResult(string json)
    {
        var result = new JsonParser(json);
        Console.WriteLine(result.Result);
    }
}
