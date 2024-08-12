using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class DeleteFrogBiteStartedEventSystem : Delete<OnBiteStarted>
  {
    protected DeleteFrogBiteStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}