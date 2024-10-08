using System.Collections.Generic;

namespace LudensClub.GeoChaos.Editor.Monitoring.World
{
  public static class EntityDictionary
  {
    private const string INT_FORMAT = "D8";
      
    private static readonly Dictionary<(int, EntityMethodType), string> strings =
      new Dictionary<(int, EntityMethodType), string>();

    private static readonly Dictionary<EntityMethodType, string> _methodTypes = new Dictionary<EntityMethodType, string>
    {
      { EntityMethodType.UpdateView, ".UpdateView()" },
      { EntityMethodType.Tick, ".Tick()" }
    };
    
    public static string GetString(int entity, EntityMethodType methodType)
    {
      if (!strings.TryGetValue((entity, methodType), out string value))
      {
        value = entity.ToString(INT_FORMAT) + _methodTypes[methodType];
        strings[(entity, methodType)] = value;
      }

      return value;
    }
  }

  public enum EntityMethodType
  {
    UpdateView = 0,
    Tick = 1
  }
}