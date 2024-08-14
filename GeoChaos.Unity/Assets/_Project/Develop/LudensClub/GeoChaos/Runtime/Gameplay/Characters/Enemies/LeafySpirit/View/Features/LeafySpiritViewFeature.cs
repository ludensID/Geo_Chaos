using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.View
{
  public class LeafySpiritViewFeature : EcsFeature
  {
    public LeafySpiritViewFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<EnableLeafySpiritBodyColliderSystem>());
      Add(systems.Create<ActivateLeafySpiritBodySystem>());
      Add(systems.Create<SetLeafySpiritBodyDirectionWhenRisingSystem>());
      Add(systems.Create<SetLeafySpiritBodyDirectionByMovementSystem>());
    }
  }
}