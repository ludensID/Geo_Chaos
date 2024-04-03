using FluentAssertions;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using LudensClub.GeoChaos.Testing;
using NUnit.Framework;

namespace LudensClub.GeoChaos.Tests.EditMode
{
  public class HeroMovementTests
  {
    [Test]
    public void
      WhenAddNextHeroMoveDirectionToQueue_AndInputValueNotEqualToZero_ThenQueueElementShouldBeEqualToInputValue()
    {
      // Arrange.
      var horizontalMovement = -1;
      IConfigProvider provider = Setup.ConfigProvider();
      using GameWorldWrapper gameWorldWrapper = new GameWorldWrapper();
      EcsWorld world = gameWorldWrapper.World;
      EcsSystems systems = new EcsSystems(world);
      IInputDataProvider inputDataProvider = Setup.InputDataProvider(horizontalMovement);
      AddNextHeroMoveDirectionToQueueSystem system =
        new AddNextHeroMoveDirectionToQueueSystem(gameWorldWrapper, inputDataProvider, provider, Create.TimerService());

      int hero = Setup.Hero(world);
      ref var queue = ref world.Get<MovementQueue>(hero); 

      // Act.
      system.Run(systems);

      // Assert.
      queue.NextMovements.Count.Should().Be(1);
      queue.NextMovements[0].Direction.Should().Be(horizontalMovement);
      
      systems.Destroy();
    }
  }
}