# Text.json-test
## 目的
* System.Text.Jsonを使った解析機能のテスト実装  
* DDDを使った実装テスト

## 実行環境
* .NET6

## 実行方法
```sh
dotnet run --project src/console/console.csproj
```

## フォルダ構成
* src/console
  * Appplication
    * Commands  
      言語変換コマンドクラス
      * CSharpCommand.cs
      * KotlinCommand.cs ※言語ソースコード出力の追加例
    * Models  
      ソースコード変換結果モデルクラス
      * ConvertResultModel.cs
    * ApplicationBase.cs  
      アプリケーションサービスクラスのスーパークラス  
      DIコンテナ経由でインターフェイスにインスタンスを設定
    * ClassesApplication.cs  
      ソース変換アプリケーションサービスクラス

  * Domain  
    ValueObject・EntityとRepositoryのインターフェイス
    * Entities
      * ClassesEntity.cs  
        集約クラス  
        * ClassEntityリスト：インナークラスリスト
        * ルートクラス：メインクラス

      * ClassEntity.cs  
        クラスエンティティ
        * プロパティリスト：クラス内のプロパティリスト

    * ValueObjects
      * PropertyValueObject.cs  
        プロパティValueObject
        * プロパティ型：プロパティの型

      * PropertyType.cs  
        プロパティ型ValueObject

    * Interfaces
      * IJsonRepository.cs  
        JSON読み込みリポジトリインターフェース

      * IFileOutputRepository.cs  
        ファイル出力リポジトリインターフェース
    * Results
      * FileOutputCommand.cs  
        ファイル出力コマンドクラス  
        名前空間や出力先などを設定するクラス

  * Infrastructure  
    インフラ層  
    * ConsoleOutputRepository.cs  
      コンソール出力リポジトリ  
      集約クラスの内容をコンソール出力するためのクラス

    * FileOutputRepository.cs  
      ファイル出力リポジトリ  
      集約クラスからソースコードを出力するためのクラス
      
    * JsonRepository.cs  
      JSON読み込みリポジトリ  
      JSON文字列を読み込んでドメイン集約クラス返す
      * JsonProperties：プライベートプロパティ  
        IJsonPropertyのリスト  
        System.Linq.Whereを使って対象のJsonPropertyを選択する

    * JsonProperties  
      ストラテジパターンで実装されており、JsonRepositoryから呼ばれる。
      JsonValueKindを元にJsonPropertyResultを返す

      * JsonPropertyResult.cs  
        ドメイン層のPropertyValueObjectとサブクラス情報を内包したクラス

      * IJsonProperty.cs  
        Jsonプロパティインターフェイス  

      * JsonPropertyArray.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Array用  
        配列型に変換する(配列要素は再起的に取得する)

      * JsonPropertyFalse.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.False用  
        boolに変換する
        
      * JsonPropertyTrue.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.True用  
        boolに変換する
        
      * JsonPropertyNull.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Null用  
        Nullableに変換する
        
      * JsonPropertyNumber.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Number用  
        decimalに変換する
        
      * JsonPropertyObject.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Object用  
        インナークラスに変換する
        
      * JsonPropertyString.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.String用  
        stringに変換する

    * Utils  
      ソースコード変換ユーティリティ
      * IConverter.cs  
        ソース変換用インターフェース

      * SoruceConverter.cs  
        ソース変換ユーティリティのエントリクラス  
        各言語用ソースコード変換変換メソッドを実装する  

      * CSConverter.cs  
        C#ソースコード変換クラス  

      * KTConverter.cs ※言語ソースコード出力の追加例  
        Kotlinソースコード変換クラス  

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

* Lib/TinyDIContainer
  * DIContainer.cs  
    拙作「[TinyDIContainer](https://github.com/kazenetu/DIContainer)」を改良した簡易DIコンテナ

## 言語生成手順
言語生成機能を追加することができる。  
(また、各種DBのDDL文も作成可能)
1. **Domain.Commands.OutputLanguageType**に```出力言語タイプ```を追記する。  
  追記場所：Domain/Commands/FileOutputCommand.cs

1. **Domain.Commands.ParamKeys**に```追加パラメータ```を追記する。  
  追記場所：Domain/Commands/FileOutputCommand.cs

1. **Infrastructure.Utils.*Converter**を追加する。  
  例) Infrastructure/Utils/KTConverter.cs

1. **Infrastructure.FileOutputRepository**に専用メソッドを追記する。  
  追記場所：Appplication/ClassesApplication.cs  
  例) private static string GetKTCode(ClassesEntity classInstance, FileOutputCommand command)

1. **Appplication.Commands.*Command**を追加する。  
  例) Application/Commands/KotlinCommand.cs

1. **Appplication.ClassesApplication**にメソッドを追記する。  
  追記場所：Appplication/ClassesApplication.cs  
  例) public ConvertResultModel ConvertJsonToKotlin(string json, KotlinCommand command)


