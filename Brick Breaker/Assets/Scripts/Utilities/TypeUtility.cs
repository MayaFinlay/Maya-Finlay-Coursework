using UnityEngine;

class TypeUtility
{
    public static void PrintType(object x)
    {
        string internalName = x.GetType().Name;
        string properName = ConvertInternalName(internalName);

        Debug.Log(properName);
    }

    static string ConvertInternalName(string internalName)
    {
        switch (internalName)
        {
            case "Boolean": return "bool";
            case "Byte":    return "byte";
            case "SByte":   return "sbyte";
            case "Char":    return "char";
            case "Decimal": return "decimal";
            case "Double":  return "double";
            case "Single":  return "float";
            case "Int32":   return "int";
            case "UInt32":  return "uint";
            case "Int64":   return "long";
            case "UInt64":  return "ulong";
            case "Object":  return "object";
            case "Int16":   return "short";
            case "UInt16":  return "ushort";
            case "String":  return "string";
            default:        return internalName;
        }
    }
}
