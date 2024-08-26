using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait
{
  public class LeafySpiritWaitFeature : WaitFeature<LeafySpiritTag>
  {
    public LeafySpiritWaitFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}