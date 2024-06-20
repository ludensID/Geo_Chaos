using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EntityTypeExtensions
  {
    public static bool IsEnemy(this EntityType type)
    {
      return type is >= EntityType.Enemy and < EntityType.Ring;
    }
  }
}