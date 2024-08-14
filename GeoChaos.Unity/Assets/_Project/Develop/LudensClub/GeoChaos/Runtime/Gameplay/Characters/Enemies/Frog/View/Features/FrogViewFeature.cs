using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.View
{
  public class FrogViewFeature : EcsFeature
  {
    public FrogViewFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SetFrogBodyDirectionByJumpSystem>());
    }
  }
}