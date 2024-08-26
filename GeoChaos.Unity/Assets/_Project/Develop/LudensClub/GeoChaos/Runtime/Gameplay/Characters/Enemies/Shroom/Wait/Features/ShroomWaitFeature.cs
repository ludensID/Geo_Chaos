using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Wait
{
  public class ShroomWaitFeature : WaitFeature<ShroomTag>
  {
    public ShroomWaitFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}