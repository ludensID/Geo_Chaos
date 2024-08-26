using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait
{
  public class FrogWaitFeature : WaitFeature<FrogTag>
  {
    public FrogWaitFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}