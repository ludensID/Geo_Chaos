using System;
using Cysharp.Text;

namespace LudensClub.GeoChaos.Editor.General
{
  public static class TypeUtils
  {
    public static string GetCleanGenericTypeName(Type type)
    {
      if (!type.IsGenericType)
        return type.Name;

      using Utf16ValueStringBuilder builder = ZString.CreateStringBuilder();
      {
        using Utf16ValueStringBuilder constraintBuilder = ZString.CreateStringBuilder();
        {
          foreach (Type constraint in type.GetGenericArguments())
          {
            if (constraintBuilder.Length > 0)
              constraintBuilder.Append(", ");

            constraintBuilder.Append(GetCleanGenericTypeName(constraint));
          }

          int genericIndex = type.Name.LastIndexOf("`", StringComparison.Ordinal);
          builder.Append(genericIndex == -1 ? type.Name : type.Name[..genericIndex]);
          builder.Append("<");
          builder.Append(constraintBuilder.ToString());
          builder.Append(">");
          return builder.ToString();
        }
      }
    }
  }
}