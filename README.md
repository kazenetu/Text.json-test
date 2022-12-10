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
    JsonDocumentを利用してJson構造を解析するクラス  
    現在の実装では解析結果を出力用文字列を出力する  
    将来的にはCommonのクラス群を作成する予定

  * Program.cs  
    Json文字列とJson解析を実施するエントリクラス
