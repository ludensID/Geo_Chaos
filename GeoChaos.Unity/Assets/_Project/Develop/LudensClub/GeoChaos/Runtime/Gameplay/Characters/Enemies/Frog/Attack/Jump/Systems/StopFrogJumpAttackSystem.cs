using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump
{
  public class StopFrogJumpAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingFrogs;

    public StopFrogJumpAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StopAttackJumpCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _attackingFrogs)
      {
        frog
          .Del<StopAttackJumpCommand>()
          .Has<JumpAttacking>(false)
          .Add<StopJumpCycleCommand>();
      }
    }
  }
}