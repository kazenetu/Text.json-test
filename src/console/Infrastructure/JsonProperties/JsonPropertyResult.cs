using Domain.ValueObjects;

namespace Infrastructure.JsonProperties;

/// <summary>
/// JsonPropertyの戻り値
/// </summary>
/// <param name="PropertyValueObject">プロパティ値オブジェクト</param>
/// <param name="InnerClass">インナークラスか否か</param>
/// <returns></returns>
public record JsonPropertyResult(PropertyValueObject PropertyValueObject, bool InnerClass);