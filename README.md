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
      * CSharpCommand.cs
    * Models
      * ConvertResultModel.cs
    * ApplicationBase.cs
    * ClassesApplication.cs
  * Domain  
    ValueObject・EntityとRepositoryのインターフェイス
    * ClassesEntity.cs  
      集約クラス  
      * ClassEntityリスト：インナークラスリスト
      * ルートクラス：メインクラス

    * ClassEntity.cs  
      クラスエンティティ
      * プロパティリスト：クラス内のプロパティリスト

    * PropertyValueObject.cs  
      プロパティValueObject
      * プロパティ型：プロパティの型

    * PropertyType.cs  
      プロパティ型ValueObject

    * IJsonRepository.cs  
      JSON読み込みリポジトリインターフェース

    * IConsoleOutputRepository.cs  
      コンソール出力リポジトリインターフェース

    * IFileOutputRepository.cs  
      ファイル出力リポジトリインターフェース

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
      集約クラスからC#ソースコードを出力するためのクラス
      
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
        C#の配列型に変換する(配列要素は再起に取得する)

      * JsonPropertyFalse.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.False用  
        C#のboolに変換する
        
      * JsonPropertyTrue.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.True用  
        C#のboolに変換する
        
      * JsonPropertyNull.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Null用  
        C#のNullableに変換する
        
      * JsonPropertyNumber.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Number用  
        C#のdecimalに変換する
        
      * JsonPropertyObject.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.Object用  
        C#のインナークラスに変換する
        
      * JsonPropertyString.cs  
        IJsonPropertyの実装クラス  
        JsonValueKind.String用  
        C#のstringに変換する

    * Utils  
      ソースコード変換ユーティリティ
      * IConverter.cs  
        ソース変換用インターフェース

      * ParamKeys.cs  
        パラメータキー定数クラス  
        連想配列のキーの定数

      * SoruceConverter.cs  
        ソース変換ユーティリティのエントリクラス  
        各言語用ソースコード変換変換メソッドを実装する予定  
        現時点はC#用ソースコードのみ

      * CSConverter.cs  
        C#ソースコード変換クラス  

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
