using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Wait
{
  public class LamaWaitFeature : WaitFeature<LamaTag>
  {
    public LamaWaitFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}