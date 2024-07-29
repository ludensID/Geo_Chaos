using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class EnemyFeature : EcsFeature
  {
    public EnemyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LamaFeature>());
      Add(systems.Create<LeafySpiritFeature>());
      
      Add(systems.Create<SetEnemyBodyDirectionSystem>());
      Add(systems.Create<SetHealthViewSystem>());
    }
  }
}