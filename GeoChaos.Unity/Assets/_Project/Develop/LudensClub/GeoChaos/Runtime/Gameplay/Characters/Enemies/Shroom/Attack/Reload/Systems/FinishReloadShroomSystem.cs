using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload
{
  public class FinishReloadShroomSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _reloadingShrooms;

    public FinishReloadShroomSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _reloadingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<ReloadingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _reloadingShrooms
        .Check<ReloadingTimer>(x => x.TimeLeft <= 0))
      {
        shroom
          .Del<ReloadingTimer>()
          .Del<Reloading>()
          .Add<OnReloadingFinished>();
      }
    }
  }
}