using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun
{
  public class FinishStunFrogSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _stunnedFrogs;

    public FinishStunFrogSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _stunnedFrogs = _game
        .Filter<FrogTag>()
        .Inc<StunTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _stunnedFrogs
        .Check<StunTimer>(x => x.TimeLeft <= 0))
      {
        frog
          .Del<StunTimer>()
          .Del<Stunned>();
      }
    }
  }
}