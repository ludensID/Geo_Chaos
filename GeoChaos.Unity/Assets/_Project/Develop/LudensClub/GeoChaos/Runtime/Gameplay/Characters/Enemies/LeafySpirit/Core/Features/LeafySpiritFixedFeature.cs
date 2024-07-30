using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
{
  public class LeafySpiritFixedFeature : EcsFeature
  {
    public LeafySpiritFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritMovingFixedFeature>());
    } 
  }
}