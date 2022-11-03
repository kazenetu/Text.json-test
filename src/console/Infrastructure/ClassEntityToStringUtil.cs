using System.Text;

public static class ClassEntityToStringUtil
{
    public static string GetClassString(Class classEntity, int index = 0)
    {
        var result = new StringBuilder();

        var levelSpace = new string('S', index).Replace("S", "  ");
        result.AppendLine($"{levelSpace}public class {classEntity.Name} {{");

        // クラス
        foreach (var classInstance in classEntity.InnerClass)
        {
            result.AppendLine($"{levelSpace}{GetClassString(classInstance,index + 1)}");
        }

        // プロパティ
        foreach (var property in classEntity.Properties)
        {
            result.Append($"{levelSpace}{GetPropertyString(property,index + 1)}");
        }

        result.AppendLine($"{levelSpace}}}");

        return result.ToString();
    }

    private static string GetPropertyString(Property propertyEntity, int index)
    {
        var result = new StringBuilder();

        var defaultValue = string.Empty;
        if (propertyEntity.TypeName == "string" || propertyEntity.TypeName == "object")
        {
            defaultValue = "string.Empty";
        }

        var levelSpace = new string('S', index).Replace("S", "  ");
        result.Append($"{levelSpace}public {propertyEntity.TypeName} {propertyEntity.Name}{{set; get;}}");
        if (!string.IsNullOrEmpty(defaultValue))
        {
            result.Append($" = {defaultValue};");
        }
        result.AppendLine();

        return result.ToString();
    }
}