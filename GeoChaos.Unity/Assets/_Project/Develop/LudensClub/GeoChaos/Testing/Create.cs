using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using NSubstitute;

namespace LudensClub.GeoChaos.Testing
{
  public static class Create
  {
    public static IConfigProvider ConfigProvider()
    {
      return Substitute.For<IConfigProvider>();
    }

    public static IInputDataProvider InputDataProvider()
    {
      return Substitute.For<IInputDataProvider>();
    }

    public static ITimerService TimerService()
    {
      return Substitute.For<ITimerService>();
    }

    public static int Hero(EcsWorld world)
    {
      var hero = world.NewEntity();
      world.Add<Hero>(hero);
      return hero;
    }
  }
}