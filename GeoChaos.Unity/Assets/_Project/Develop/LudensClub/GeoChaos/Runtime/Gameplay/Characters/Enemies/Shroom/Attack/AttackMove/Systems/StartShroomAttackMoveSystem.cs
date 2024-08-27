using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class StartShroomAttackMoveSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingShrooms;

    public StartShroomAttackMoveSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<OnReloadingFinished>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _movingShrooms)
      {
        shroom
          .Add<AttackMoveCommand>();
      }
    }
  }
}