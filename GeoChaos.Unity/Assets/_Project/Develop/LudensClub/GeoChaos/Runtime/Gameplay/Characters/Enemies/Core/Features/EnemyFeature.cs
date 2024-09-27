using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class EnemyFeature : EcsFeature
  {
    public EnemyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DestroyEnemiesOnRestartSystem>());
        
      Add(systems.Create<SelectNearestDamageableEntitySystem>());
      
      Add(systems.Create<LamaFeature>());
      Add(systems.Create<LeafySpiritFeature>());
      Add(systems.Create<FrogFeature>());
      Add(systems.Create<ZombieFeature>());
      Add(systems.Create<ShroomFeature>());
        
      Add(systems.Create<UpdateEnemyHealthViewSystem>());
    }
  }
}