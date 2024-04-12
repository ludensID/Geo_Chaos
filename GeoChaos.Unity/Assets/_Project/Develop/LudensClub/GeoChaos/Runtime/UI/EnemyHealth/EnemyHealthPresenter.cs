using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class EnemyHealthPresenter : ITickable
  {
    private readonly EnemyHealthView _view;
    private readonly EcsWorld _game;
    private readonly EcsFilter _enemies;

    public EnemyHealthPresenter(EnemyHealthView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<Health>()
        .End();
    }

    public void Tick()
    {
      foreach (int enemy in _enemies)
      {
        ref Health health = ref _game.Get<Health>(enemy);
        _view.SetText(health.Value.ToString("####"));
      }
    }
  }
}