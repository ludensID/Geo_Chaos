using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using NSubstitute;
using UnityEngine;

namespace LudensClub.GeoChaos.Testing
{
  public static class Setup
  {
    public static void Movable(EcsWorld world, int hero, bool canMove = true)
    {
      ref var movable = ref world.Add<Movable>(hero);
      movable.CanMove = canMove;
    }

    public static IConfigProvider ConfigProvider()
    {
      var provider = Create.ConfigProvider();
      provider.Get<HeroConfig>().Returns(ScriptableObject.CreateInstance<HeroConfig>());
      return provider;
    }

    public static IInputDataProvider InputDataProvider(int horizontalMovement)
    {
      var inputDataProvider = Create.InputDataProvider();
      inputDataProvider.Data.Returns(new InputData { HorizontalMovement = horizontalMovement });
      return inputDataProvider;
    }

    public static int Hero(EcsWorld world)
    {
      var hero = Create.Hero(world);
      Movable(world, hero);
      return hero;
    }
  }
}