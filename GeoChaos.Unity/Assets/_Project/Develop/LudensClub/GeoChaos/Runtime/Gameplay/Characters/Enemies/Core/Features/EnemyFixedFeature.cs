using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class EnemyFixedFeature : EcsFeature
  {
    public EnemyFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LamaFixedFeature>());
      Add(systems.Create<LeafySpiritFixedFeature>());
        
      Add(systems.Create<KeepInBoundsSystem>());
    }
  }
}