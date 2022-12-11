# Text.json-test
System.Text.Jsonを使った解析機能のテスト

# 実行環境
* .NET6

# 実行方法
```sh
dotnet run --project src/console/console.csproj
```

# フォルダ構成
* src/console
  * Common  
    * ClassesEntity.cs
    * Class.cs
    * Property.cs
    * IJsonRepository.cs
    * IConsoleOutputRepository.cs
    * IFileOutputRepository.cs
    * FileOutputCommand.cs
  * Infrastructure
    * ConsoleOutputRepository.cs
    * ConsoleOutputRepository.cs
    * FileOutputRepository.cs
    * JsonRepository.cs
  * DeserializeSamples  
    サンプルJson文字列デシアライズクラス
    * SimpleJson.cs
    * InnerClassJson.cs
    * ClassArrayJson.cs
    * InnerNestClassJson.cs

  * JsonParser.cs  
    JSON文字列をパースする処理のプロトタイプ版  
    JsonDocumentを利用してJson構造を解析するクラス  
    スペースでクラスやプロパティを簡易的に表す文字列を返す  

  * Program.cs  
    Json文字列とJson解析を実施するエントリクラス
