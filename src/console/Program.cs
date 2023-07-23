using System.Text.Json;
using Appplication;
using Domain.Interfaces;
using Infrastructure;
using TinyDIContainer;

namespace ConsoleApp;

internal class Program
{
    /// <summary>
    /// エントリメソッド
    /// </summary>    
    private static void Main(string[] args)
    {
        // DI設定
        DIContainer.Add<IFileOutputRepository, FileOutputRepository>();
        DIContainer.Add<IJsonRepository, JsonRepository>();

        // ファイル出力設定値
        var rootPath = "CSOutputs";
        var nameSpace = "Domain.Entity";

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
        FileOutputAndResutoOutput(simpleJson, nameSpace, rootPath, "SimpleJsonClass");
        Console.WriteLine("--------------------");
        //出力結果：
        // namespace Domain.Entity
        // {
        //   public class SimpleJsonClass
        //   {
        //     public string propString { set; get; } = string.Empty;
        //     public decimal propNumber { set; get; }
        //     public string propDate { set; get; } = string.Empty;
        //     public bool propTrue { set; get; }
        //     public bool propFalse { set; get; }
        //     public object propNull { set; get; } = string.Empty;
        //     public List<decimal>? propArray { set; get; }
        //   }
        // }
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

        Console.WriteLine("----Class解析結果----");
        FileOutputAndResutoOutput(innerClassJson, nameSpace, rootPath, "InnerClassJsonClass");
        Console.WriteLine("--------------------");
        // ----Class解析結果----
        // namespace Domain.Entity
        // {
        //   public class InnerClassJsonClass
        //   {
        //     public class InnerClass
        //     {
        //       public string propObjString { set; get; } = string.Empty;
        //     }
        //
        //     public InnerClass? propObjct { set; get; }
        //     public decimal propNumber { set; get; }
        //   }
        // }

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

        Console.WriteLine("----Class解析結果----");
        FileOutputAndResutoOutput(arrayJson, nameSpace, rootPath, "ArrayJsonClass");
        Console.WriteLine("--------------------");
        // ----Class解析結果----
        // namespace Domain.Entity
        // {
        //   public class ArrayJsonClass
        //   {
        //     public class InnerClass
        //     {
        //       public string propObjString { set; get; } = string.Empty;
        //     }
        //
        //     public List<InnerClass>? propObjcts { set; get; }
        //   }
        // }

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

        Console.WriteLine("----Class解析結果----");
        FileOutputAndResutoOutput(innerNestJson, nameSpace, rootPath, "InnerNestJsonClass");
        Console.WriteLine("--------------------");
        // ----Class解析結果----
        // namespace Domain.Entity
        // {
        //   public class InnerNestJsonClass
        //   {
        //     public class InnerClassA
        //     {
        //       public string propString { set; get; } = string.Empty;
        //       public decimal propNumber { set; get; }
        //       public string propDate { set; get; } = string.Empty;
        //       public bool propTrue { set; get; }
        //       public bool propFalse { set; get; }
        //       public object propNull { set; get; } = string.Empty;
        //       public List<decimal>? propArray { set; get; }
        //     }
        //
        //     public class InnerClass
        //     {
        //       public InnerClassA? propSubObjct { set; get; }
        //     }
        //
        //     public InnerClass? propObjct { set; get; }
        //   }
        // }
    }

    /// <summary>
    /// ファイル出力と結果表示
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="nameSpace">名前空間</param>
    /// <param name="rootPath">出力先</param>
    /// <param name="rootClassName">ルートパスのクラス名/param>
    private static void FileOutputAndResutoOutput(string json, string nameSpace, string rootPath, string rootClassName)
    {
        // DIでインスタンス生成
        IFileOutputRepository repository = DIContainer.CreateInstance<IFileOutputRepository>();
        IJsonRepository jsonRepository = DIContainer.CreateInstance<IJsonRepository>();

        var indentSpaceCount = 4;
        var csApplication = new ClassesApplication(jsonRepository, repository);
        var result = csApplication.ConvertJsonToCSharp(json, new Appplication.Commands.CSharpCommand(nameSpace, rootPath, rootClassName, indentSpaceCount));

        // コンソール出力
        var message = result.Success ? "成功" : "失敗";
        Console.WriteLine($"{result.FileName}...{message}");
        if(result.Success){
            Console.WriteLine($"---出力結果---");
            Console.WriteLine(result.SourceCode);
        }
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
