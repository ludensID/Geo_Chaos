using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  public class FrogTongueAttackLateFeature : EcsFeature
  {
    public FrogTongueAttackLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DrawFrogTongueSystem>());
    }
  }
}