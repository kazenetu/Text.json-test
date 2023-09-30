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
            ""prop_string"" : ""string""
            , ""propNumber"":10
            , ""prop_Date"":""2022/01/01 10:11:12""
            , ""PropTrue"":true
            , ""propFalse"":false
            , ""propNull"":null
            , ""propArray"":[1,2,3]
        }";

        Console.WriteLine("--単純なJSON文字列--");
        ShowJsonResult(simpleJson);
        // 出力結果：
        // String prop_string
        // Number propNumber
        // DateTime prop_Date
        // True PropTrue
        // False propFalse
        // Null propNull
        // Array(Number) propArray

        var simpleJsonResult = JsonSerializer.Deserialize<SimpleJson>(simpleJson);
        Console.WriteLine("----Deserialize結果----");
        Console.WriteLine(simpleJsonResult?.ToString());
        Console.WriteLine("--------------------");
        //出力結果：
        // ----Deserialize結果----
        // propString:
        // propNumber:10
        // propDate:
        // propTrue:False
        // propFalse:False
        // propNull:
        // propArray:[1,2,3]

        Console.WriteLine("----Class解析結果----");
        FileOutputAndResutoOutput(simpleJson, nameSpace, rootPath, "SimpleJsonClass");
        Console.WriteLine("--------------------");
        //出力結果：
        // namespace Domain.Entity
        // {
        //     public class SimpleJsonClass
        //     {
        //         public string prop_string { set; get; } = string.Empty;
        //         public decimal propNumber { set; get; }
        //         public string prop_Date { set; get; } = string.Empty;
        //         public bool PropTrue { set; get; }
        //         public bool propFalse { set; get; }
        //         public object propNull { set; get; } = string.Empty;
        //         public List<decimal>? propArray { set; get; }
        //     }
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

        // Kotlinへの変換
        var package = "Kotlin.example";
        var rootPathKotlin = "KTOutputs";
        FileOutputAndResutoOutputKotlin(simpleJson, package, rootPathKotlin, "SimpleJsonClass");
        // KTOutputs/SimpleJsonClass.kt...成功
        // ---出力結果---
        // package Kotlin.example
        //
        // import kotlinx.serialization.Serializable
        //
        // @Serializable
        // data class SimpleJsonClass(var prop_string: String, var propNumber: Double, var prop_Date: String, var PropTrue: Boolean, var propFalse: Boolean, var propNull: String, var propArray: List<Double>)

        FileOutputAndResutoOutputKotlin(innerClassJson, package, rootPathKotlin, "InnerClassJsonClass");
        // KTOutputs/InnerClassJsonClass.kt...成功
        // ---出力結果---
        // import kotlinx.serialization.Serializable
        //
        // @Serializable
        // data class InnerClassJsonClass(var propObjct: InnerClass, var propNumber: Double)
        //
        // @Serializable
        // data class InnerClass(var propObjString: String)

        FileOutputAndResutoOutputKotlin(arrayJson, package, rootPathKotlin, "ArrayJsonClass");
        // KTOutputs/ArrayJsonClass.kt...成功
        // ---出力結果---
        // import kotlinx.serialization.Serializable
        //
        // @Serializable
        // data class ArrayJsonClass(var propObjcts: Array<InnerClass>)
        //
        // @Serializable
        // data class InnerClass(var propObjString: String)

        FileOutputAndResutoOutputKotlin(innerNestJson, package, rootPathKotlin, "InnerNestJsonClass");
        // KTOutputs/InnerNestJsonClass.kt...成功
        // ---出力結果---
        // import kotlinx.serialization.Serializable
        //
        // @Serializable
        // data class InnerNestJsonClass(var propObjct: InnerClass)
        //
        // @Serializable
        // data class InnerClass(var propSubObjct: InnerClassA)
        //
        // @Serializable
        // data class InnerClassA(var propString: String, var propNumber: Double, var propDate: String, var propTrue: Boolean, var propFalse: Boolean, var propNull: String, var propArray: Array<Double>)

        Console.WriteLine("--プレフィックス、サフィックス追加--");
        FileOutputAndResutoOutput(innerNestJson, nameSpace, rootPath, "InnerNestJsonClass", "Prefix_", "_Suffix");
        // namespace Domain.Entity
        // {
        //     public class Prefix_InnerNestJsonClass_Suffix
        //     {
        //         public class Prefix_InnerClassA_Suffix
        //         {
        //             public string propString { set; get; } = string.Empty;
        //             public decimal propNumber { set; get; }
        //             public string propDate { set; get; } = string.Empty;
        //             public bool propTrue { set; get; }
        //             public bool propFalse { set; get; }
        //             public object propNull { set; get; } = string.Empty;
        //             public List<decimal>? propArray { set; get; }
        //         }
        //
        //         public class Prefix_InnerClass_Suffix
        //         {
        //             public Prefix_InnerClassA_Suffix? propSubObjct { set; get; }
        //         }
        //
        //         public Prefix_InnerClass_Suffix? propObjct { set; get; }
        //     }
        // }

        FileOutputAndResutoOutputKotlin(innerNestJson, package, rootPathKotlin, "InnerNestJsonClass","Prefix_","_Suffix");
        // import kotlinx.serialization.Serializable
        //
        // @Serializable
        // data class Prefix_InnerNestJsonClass_Suffix(var propObjct: Prefix_InnerClass_Suffix)
        //
        // @Serializable
        // data class Prefix_InnerClass_Suffix(var propSubObjct: Prefix_InnerClassA_Suffix)
        //
        // @Serializable
        // data class Prefix_InnerClassA_Suffix(var propString: String, var propNumber: Double, var propDate: String, var propTrue: Boolean, var propFalse: Boolean, var propNull: String, var propArray: Array<Double>)

    }

    /// <summary>
    /// ファイル出力と結果表示
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="nameSpace">名前空間</param>
    /// <param name="rootPath">出力先</param>
    /// <param name="rootClassName">ルートパスのクラス名/param>
    /// <param name="prefix">固定プレフィックス</param>
    /// <param name="suffix">固定サフィックス</param>
    private static void FileOutputAndResutoOutput(string json, string nameSpace, string rootPath, string rootClassName, string prefix = "", string suffix = "")
    {
        var indentSpaceCount = 4;
        var csApplication = new ClassesApplication();
        var result = csApplication.ConvertJsonToCSharp(json, new Appplication.Commands.CSharpCommand(nameSpace, rootPath, rootClassName, indentSpaceCount,
                                               prefix, suffix));

        // コンソール出力
        var message = result.Success ? "成功" : "失敗";
        Console.WriteLine($"{result.FileName}...{message}");
        if(result.Success){
            Console.WriteLine($"---出力結果---");
            Console.WriteLine(result.SourceCode);
        }
    }

    /// <summary>
    /// ファイル出力と結果表示
    /// </summary>
    /// <param name="json">JSON文字列</param>
    /// <param name="packageName">パッケージ名</param>
    /// <param name="rootPath">出力先</param>
    /// <param name="rootClassName">ルートパスのクラス名/param>
    /// <param name="prefix">固定プレフィックス</param>
    /// <param name="suffix">固定サフィックス</param>
    private static void FileOutputAndResutoOutputKotlin(string json, string packageName, string rootPath, string rootClassName, string prefix = "", string suffix = "")
    {
        var csApplication = new ClassesApplication();
        var result = csApplication.ConvertJsonToKotlin(json, new Appplication.Commands.KotlinCommand(packageName, rootPath, rootClassName,
                                                prefix, suffix));

        // コンソール出力
        var message = result.Success ? "成功" : "失敗";
        Console.WriteLine($"{result.FileName}...{message}");
        if (result.Success)
        {
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
