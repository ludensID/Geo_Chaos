using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class StopFrogBiteSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bitingFrogs;

    public StopFrogBiteSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StopBiteCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _bitingFrogs)
      {
        frog
          .Del<StopBiteCommand>()
          .Has<Biting>(false)
          .Has<BiteTimer>(false)
          .Add<OnBiteStopped>();
      }
    }
  }
}