using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class DeleteFrogBiteStoppedEventSystem : Delete<OnBiteStopped>
  {
    protected DeleteFrogBiteStoppedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}