using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NSubstitute;
using UnityEngine;

namespace LudensClub.GeoChaos.Testing
{
  public static class Setup
  {
    public static void Movable(EcsWorld world, int hero, bool canMove = true)
    {
      ref var movable = ref world.Add<Movable>(hero);
    }

    public static IConfigProvider ConfigProvider()
    {
      var provider = Create.ConfigProvider();
      provider.Get<HeroConfig>().Returns(ScriptableObject.CreateInstance<HeroConfig>());
      return provider;
    }
    

    public static InputData InputDataProvider(int horizontalMovement)
    {
      return new InputData { HorizontalMovement = horizontalMovement };
    }

    public static int Hero(EcsWorld world)
    {
      var hero = Create.Hero(world);
      Movable(world, hero);
      return hero;
    }
  }
}