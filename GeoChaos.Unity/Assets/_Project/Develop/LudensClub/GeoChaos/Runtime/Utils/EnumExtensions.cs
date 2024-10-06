using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Windows;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EnumExtensions
  {
    public static bool IsEnemy(this EntityType type)
    {
      return type is >= EntityType.Enemy and < EntityType.Ring;
    }

    public static bool IsReactiveOnPause(this WindowType type)
    {
      return type is WindowType.Pause or >= WindowType.NothingHappens;
    }
  }
}