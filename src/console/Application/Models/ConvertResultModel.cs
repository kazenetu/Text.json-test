namespace Appplication.Models;

/// <summary>
/// ソースコード変換結果
/// </summary>
/// <param name="success">変換成功か否か</param>
/// <param name="FileName">変換後のソースコード</param>
public record ConvertResultModel(bool success, string FileName);