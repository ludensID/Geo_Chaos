using System;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public static class EcsMonitoring
  {
    public static string GetCleanGenericTypeName(Type type)
    {
      if (!type.IsGenericType)
        return type.Name;

      var constraints = "";
      foreach (Type constraint in type.GetGenericArguments())
        constraints += constraints.Length > 0 ? $", {GetCleanGenericTypeName(constraint)}" : constraint.Name;

      int genericIndex = type.Name.LastIndexOf("`", StringComparison.Ordinal);
      string typeName = genericIndex == -1
        ? type.Name
        : type.Name[..genericIndex];
      return $"{typeName}<{constraints}>";
    }
  }
}