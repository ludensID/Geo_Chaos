using FluentAssertions;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NUnit.Framework;

namespace LudensClub.GeoChaos.Testing.EditMode
{
  public class EcsEntitiesTests
  {
    public struct A : IEcsComponent
    {
      public int Value;
    }

    public struct B : IEcsComponent
    {
      public string Value;
    }
    
    [Test]
    public void WhenSelectEntitiesWithMultipleConditionAndHasEntityThatDoesNotMatchSecondToLastConditionThenThisEntityShouldNotBeSelect()
    {
      // Arrange.
      var aValue = 1;
      var bValue = "1";
      EcsWorld game = new EcsWorld();
      EcsEntities entities = game.Filter<A>().Inc<B>().Collect();
      EcsEntity entity = game.CreateEntity();

      entity
        .Add<A>()
        .Add((ref B b) => b.Value = bValue);
      
      // Act.
      var selectedEntities = entities.Clone()
        .Where<A>(x => x.Value == aValue)
        .Where<B>(x => x.Value == bValue);
      
      // Assert.
      entity.Get<A>().Value.Should().NotBe(aValue);
      entity.Get<B>().Value.Should().Be(bValue);
      selectedEntities.ToEnumerable().Should().NotContain(x => x.Entity == entity.Entity);
    } 
  }
}